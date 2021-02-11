using OPMS.Services.Reagent;
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
    public class DealerViewModel : GenericViewModel<Dealers>
    {
        private readonly IReagentService _reagentService;

        public DealerViewModel(IReagentService reagentService)
        {
            _reagentService = reagentService;

            GetModelItems();
        }

        #region API Calls

        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await _reagentService.GetDealers();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                if (response.Dealers != null && response.Dealers.Count > 0)
                {
                    ModelDtos = response.Dealers;

                    SelectedModel = ModelDtos.First();
                    EditButtonActiveStateModel.Edit = true;
                }
                else
                {
                    ModelDtos = null;
                    EditButtonActiveStateModel.Edit = false;
                }
            }
        }

        protected async override void AddNewModelItem(Dealers model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = "tester";
            model.ModifiedDate = DateTime.UtcNow;
            var response = await _reagentService.AddNewDealer(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new dealer", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(Dealers model)
        {
            IsBusy = Visibility.Visible;
            var response = await _reagentService.UpdateDealer(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the dealer", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(Dealers model)
        {
            IsBusy = Visibility.Visible;
            var response = await _reagentService.DeleteDealer(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the dealer", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(Dealers selectedModel)
        {
            //Do nothing
        }
        #endregion

    }
}
