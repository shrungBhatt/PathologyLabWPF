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
    public class DrLabHospitalRegistrationViewModel : GenericViewModel<HdlRegistration>
    {
        readonly IMaintenanceService maintenanceService;

        public DrLabHospitalRegistrationViewModel(IMaintenanceService maintenanceService)
        {
            this.maintenanceService = maintenanceService;


            GetModelItems();
        }

        #region Fields & Properties
        private List<RegistrationTypes> registrationTypes { get; set; }
        public List<RegistrationTypes> RegistrationTypes
        {
            get => registrationTypes;
            set
            {
                registrationTypes = value;
                RaisePropertyChanged(nameof(RegistrationTypes));
            }
        }
        private List<RegistrationCategories> registrationCategories { get; set; }
        public List<RegistrationCategories> RegistrationCategories
        {
            get => registrationCategories;
            set
            {
                registrationCategories = value;
                RaisePropertyChanged(nameof(RegistrationCategories));
            }
        }

        private RegistrationTypes selectedRegistrationType;
        public RegistrationTypes SelectedRegistrationType
        {
            get => selectedRegistrationType;
            set
            {
                selectedRegistrationType = value;
                RaisePropertyChanged(nameof(SelectedRegistrationType));
                if(selectedRegistrationType != null && SelectedModel != null)
                {
                    SelectedModel.RegistrationType = selectedRegistrationType;
                    SelectedModel.RegistrationTypeId = selectedRegistrationType.Id;
                }
            }
        }

        private RegistrationCategories selectedRegistrationCategory;
        public RegistrationCategories SelectedRegistrationCategory
        {
            get => selectedRegistrationCategory;
            set
            {
                selectedRegistrationCategory = value;
                RaisePropertyChanged(nameof(SelectedRegistrationCategory));
                if (selectedRegistrationCategory != null && SelectedModel != null)
                {
                    SelectedModel.RegistrationCategory = selectedRegistrationCategory;
                    SelectedModel.RegistrationCategoryId = selectedRegistrationCategory.Id;
                }
            }
        }
        #endregion

        #region API Calls

        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.GetDhlRegistrations();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                RegistrationCategories = response.Categories;
                RegistrationTypes = response.RegistrationTypes;
                if (response.HdlRegistrations != null && response.HdlRegistrations.Count > 0)
                {
                    ModelDtos = response.HdlRegistrations;
                    SelectedModel = ModelDtos.First();
                }

            }
        }

        protected async override void AddNewModelItem(HdlRegistration model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = DIContainer.Resolve<Repository.UserRepository>().UserName;
            model.ModifiedDate = DateTime.Now;
            model.RegistrationCategory = null;
            model.RegistrationType = null;
            var response = await maintenanceService.AddNewDhlRegistration(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new registration", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(HdlRegistration model)
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.UpdateDhlRegistration(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the registration", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(HdlRegistration model)
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.DeleteDhlRegistration(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the registration", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(HdlRegistration selectedModel)
        {
            if(selectedModel != null)
            {
                SelectedRegistrationCategory = RegistrationCategories?.Find(x => x.Id == selectedModel.RegistrationCategoryId);
                SelectedRegistrationType = RegistrationTypes?.Find(x => x.Id == selectedModel.RegistrationTypeId);
            }
        }
        #endregion
    }
}
