using OPMS.Services.Finance;
using OPMS.Services.RequestProvider;
using OPMS.ViewModels.Base;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPMS.ViewModels
{
    public class NonPatientIncomeViewModel : GenericViewModel<OtherIncome>
    {

        private readonly IFinanceService financeService;

        public NonPatientIncomeViewModel(IFinanceService financeService)
        {
            this.financeService = financeService;

            GetModelItems();
        }


        private List<AccountHead> accountHeads;
        public List<AccountHead> AccountHeads
        {
            get => accountHeads;
            set
            {
                accountHeads = value;
                RaisePropertyChanged(nameof(AccountHeads));
            }
        }

        private AccountHead selectedAccountHead;
        public AccountHead SelectedAccountHead
        {
            get => selectedAccountHead;
            set
            {
                selectedAccountHead = value;
                RaisePropertyChanged(nameof(SelectedAccountHead));
                if(selectedAccountHead != null && SelectedModel != null)
                {
                    SelectedModel.AccountHeadId = selectedAccountHead.Id;
                }
            }
        }

        #region API Calls

        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await financeService.GetOtherIncomes();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                AccountHeads = response.AccountHeads;
                if (response.OtherIncomes != null && response.OtherIncomes.Count > 0)
                {
                    ModelDtos = response.OtherIncomes;
                    var firstOption = response.OtherIncomes.First();
                    SelectedAccountHead = AccountHeads?.Find(x => x.Id == firstOption.AccountHeadId);
                    SelectedModel = firstOption;
                    EditButtonActiveStateModel.Edit = true;
                }
                else
                {
                    ModelDtos = null;
                    EditButtonActiveStateModel.Edit = false;
                }
            }
        }

        protected async override void AddNewModelItem(OtherIncome model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = "tester";
            model.ModifiedDate = DateTime.UtcNow;
            var response = await financeService.InsertOtherIncome(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new other income record", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(OtherIncome model)
        {
            IsBusy = Visibility.Visible;
            var response = await financeService.UpdateOtherIncome(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the other income record", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(OtherIncome model)
        {
            IsBusy = Visibility.Visible;
            var response = await financeService.DeleteOtherIncome(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the other income record", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(OtherIncome selectedModel)
        {
            if(selectedModel != null)
            {
                SelectedAccountHead = selectedModel.AccountHead;
            }
        }

        public override void OnAddButtonClicked()
        {
            base.OnAddButtonClicked();
            SelectedModel.CashMode = true;
            SelectedModel.ChequeMode = false;
            SelectedModel.CardMode = false;
            RaisePropertyChanged(nameof(SelectedModel));

            SelectedAccountHead = AccountHeads?.First();
        }
        #endregion

    }
}
