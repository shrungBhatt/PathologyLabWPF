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
    public class ReagentViewModel : GenericViewModel<Reagents>
    {

        private readonly IReagentService _reagentService;

        public ReagentViewModel(IReagentService reagentService)
        {
            _reagentService = reagentService;

            GetModelItems();
        }

        #region API Calls

        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await _reagentService.GetReagents();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                if (response.Reagents != null && response.Reagents.Count > 0)
                {
                    ModelDtos = response.Reagents;

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

        protected async override void AddNewModelItem(Reagents model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = "tester";
            model.ModifiedDate = DateTime.UtcNow;
            var response = await _reagentService.AddNewReagent(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new reagent", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(Reagents model)
        {
            IsBusy = Visibility.Visible;
            var response = await _reagentService.UpdateReagent(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the reagent", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(Reagents model)
        {
            IsBusy = Visibility.Visible;
            var response = await _reagentService.DeleteReagent(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the reagent", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(Reagents selectedModel)
        {
            //Do nothing
        }
        #endregion
    }
}
