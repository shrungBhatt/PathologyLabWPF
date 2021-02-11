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
    public class InventoryViewModel : GenericViewModel<Inventories>
    {

        readonly IMaintenanceService maintenanceService;

        public InventoryViewModel(IMaintenanceService maintenanceService)
        {
            this.maintenanceService = maintenanceService;

            GetModelItems();
        }

        #region API Calls
        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.GetInventories();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                if (response.Inventories != null && response.Inventories.Count > 0)
                {
                    ModelDtos = response.Inventories;
                    SelectedModel = ModelDtos.First();
                }
            }
        }

        protected async override void AddNewModelItem(Inventories model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = "tester";
            model.ModifiedDate = DateTime.UtcNow;
            var response = await maintenanceService.AddNewInventory(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new inventory", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(Inventories model)
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.UpdateInventory(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the inventory", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(Inventories model)
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.DeleteInventory(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the inventory", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(Inventories selectedModel)
        {
            
        }
        #endregion
    }
}
