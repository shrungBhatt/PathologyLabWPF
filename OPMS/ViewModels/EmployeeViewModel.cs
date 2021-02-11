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
    public class EmployeeViewModel : GenericViewModel<Employees>
    {
        readonly IMaintenanceService maintenanceService;

        public EmployeeViewModel(IMaintenanceService maintenanceService)
        {
            this.maintenanceService = maintenanceService;

            GetModelItems();
        }

        #region Fields & Properties
        private List<EmployeeCategories> categories;
        public List<EmployeeCategories> Categories
        {
            get => categories;
            set
            {
                categories = value;
                RaisePropertyChanged(nameof(Categories));
            }
        }

        private EmployeeCategories selectedCategory;
        public EmployeeCategories SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                RaisePropertyChanged(nameof(SelectedCategory));
                if(selectedCategory != null && SelectedModel != null)
                {
                    SelectedModel.EmpCategoryId = selectedCategory.Id;
                }
            }
        }

        private List<Genders> genders;
        public List<Genders> Genders
        {
            get => genders;
            set
            {
                genders = value;
                RaisePropertyChanged(nameof(Genders));
            }
        }

        private Genders selectedGender;
        public Genders SelectedGender
        {
            get => selectedGender;
            set
            {
                selectedGender = value;
                RaisePropertyChanged(nameof(SelectedGender));
                if(selectedGender != null && SelectedModel != null)
                {
                    SelectedModel.GenderId = selectedGender.Id;
                }
            }
        }

        private List<EmployeeRoles> employeeRoles;
        public List<EmployeeRoles> EmployeeRoles
        {
            get => employeeRoles;
            set
            {
                employeeRoles = value;
                RaisePropertyChanged(nameof(EmployeeRoles));
            }
        }

        private EmployeeRoles selectedEmployeeRole;
        public EmployeeRoles SelectedEmployeeRole
        {
            get => selectedEmployeeRole;
            set
            {
                selectedEmployeeRole = value;
                RaisePropertyChanged(nameof(SelectedEmployeeRole));
                if(SelectedEmployeeRole != null && SelectedModel != null)
                {
                    SelectedModel.EmployeeRoleId = selectedEmployeeRole.Id;
                }
            }
        }

        private List<FieldOptions> civilStatuses;
        public List<FieldOptions> CivilStatuses
        {
            get => civilStatuses;
            set
            {
                civilStatuses = value;
                RaisePropertyChanged(nameof(CivilStatuses));
            }
        }

        private FieldOptions selectedCivilStatus;
        public FieldOptions SelectedCivilStatus
        {
            get => selectedCivilStatus;
            set
            {
                selectedCivilStatus = value;
                RaisePropertyChanged(nameof(SelectedCivilStatus));
                if (SelectedCivilStatus != null && SelectedModel != null)
                {
                    SelectedModel.FieldOptionsId = selectedCivilStatus.Id;
                }
            }
        }
        #endregion

        #region API Calls
        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.GetEmployees();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                Genders = response.Genders;
                Categories = response.EmployeeCategories;
                EmployeeRoles = response.EmployeeRoles;
                CivilStatuses = response.CivilStatuses;

                if (response.Employees != null && response.Employees.Count > 0)
                {
                    ModelDtos = response.Employees;
                    SelectedModel = ModelDtos.First();
                }
                else
                {
                    ModelDtos = null;
                    App.Logger.Error("No employees returned");
                }
            }
        }

        protected async override void AddNewModelItem(Employees model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = "tester";
            model.ModifiedDate = DateTime.UtcNow;
            var response = await maintenanceService.AddNewEmployee(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new employee", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(Employees model)
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.UpdateEmployee(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the employee", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(Employees model)
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.DeleteEmployee(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the employee", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(Employees selectedModel)
        {
            if(SelectedModel != null)
            {
                SelectedCivilStatus = CivilStatuses?.Find(x => x.Id == selectedModel.FieldOptionsId);
                SelectedGender = Genders?.Find(x => x.Id == selectedModel.GenderId);
                SelectedCategory = Categories?.Find(x => x.Id == selectedModel.EmpCategoryId);
                SelectedEmployeeRole = EmployeeRoles?.Find(x => x.Id == selectedModel.EmployeeRoleId);
            }

        }
        #endregion

    }
}
