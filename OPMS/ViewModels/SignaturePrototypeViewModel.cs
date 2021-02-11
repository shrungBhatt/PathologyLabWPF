using OPMS.Services.Maintenance;
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
    public class SignaturePrototypeViewModel : GenericViewModel<SignaturePrototypes>
    {
        readonly IMaintenanceService maintenanceService;

        public SignaturePrototypeViewModel(IMaintenanceService maintenanceService)
        {
            this.maintenanceService = maintenanceService;
            GetModelItems();
        }

        #region API Calls

        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.GetSignatures();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                if (response.Signatures != null && response.Signatures.Count > 0)
                {
                    ModelDtos = response.Signatures;

                    SelectedModel = ModelDtos.First();
                }
                else
                {
                    ModelDtos = null;
                }
            }
        }

        protected async override void AddNewModelItem(SignaturePrototypes model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = "tester";
            model.ModifiedDate = DateTime.UtcNow;
            var response = await maintenanceService.AddNewSignature(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new signature", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(SignaturePrototypes model)
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.UpdateSignature(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the signature", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(SignaturePrototypes model)
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.DeleteSignature(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the signature", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(SignaturePrototypes selectedModel)
        {

        }
        #endregion

    }
}
