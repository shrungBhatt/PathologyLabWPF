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
    public class ExpensesViewModel : GenericViewModel<Expenses>
    {
        private readonly IFinanceService financeService;

        public ExpensesViewModel(IFinanceService financeService)
        {
            this.financeService = financeService;

            GetModelItems();
        }


        private List<ExpensesAccountHead> accountHeads;
        public List<ExpensesAccountHead> AccountHeads
        {
            get => accountHeads;
            set
            {
                accountHeads = value;
                RaisePropertyChanged(nameof(AccountHeads));
            }
        }

        private ExpensesAccountHead selectedAccountHead;
        public ExpensesAccountHead SelectedAccountHead
        {
            get => selectedAccountHead;
            set
            {
                selectedAccountHead = value;
                RaisePropertyChanged(nameof(SelectedAccountHead));
                if (selectedAccountHead != null && SelectedModel != null)
                {
                    SelectedModel.AccountHeadId = selectedAccountHead.Id;
                }
            }
        }

        #region API Calls

        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await financeService.GetExpenses();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                AccountHeads = response.ExpensesAccountHeads;
                if (response.Expenses != null && response.Expenses.Count > 0)
                {
                    ModelDtos = response.Expenses;
                    var firstOption = response.Expenses.First();
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

        protected async override void AddNewModelItem(Expenses model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = "tester";
            model.ModifiedDate = DateTime.UtcNow;
            var response = await financeService.InsertExpense(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new expense record", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(Expenses model)
        {
            IsBusy = Visibility.Visible;
            var response = await financeService.UpdateExpense(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the new expense record", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(Expenses model)
        {
            IsBusy = Visibility.Visible;
            var response = await financeService.DeleteExpense(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the expense record", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(Expenses selectedModel)
        {
            if (selectedModel != null)
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
            SelectedModel.OrderDate = DateTime.Now;
            SelectedModel.DeliveryDate = DateTime.Now;
            RaisePropertyChanged(nameof(SelectedModel));

            SelectedAccountHead = AccountHeads?.First();
        }
        #endregion

    }
}
