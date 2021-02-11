using GalaSoft.MvvmLight.Command;
using OPMS.Models;
using OPMS.Services.Maintenance;
using OPMS.Services.RequestProvider;
using OPMS.Services.WindowManager;
using OPMS.ViewModels.Base;
using OPMS.Windows;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPMS.ViewModels
{
    public class SalaryViewModel : GenericViewModel<Salary>
    {

        private Employees selectedEmployee;
        public Employees SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                selectedEmployee = value;
                RaisePropertyChanged(nameof(SelectedEmployee));
            }
        }

        public ICommand SelectEmployeeCommand { get; set; }

        private List<Employees> _employees;

        private readonly IMaintenanceService maintenanceService;
        private readonly IWindowManager _windowManager;
        public SalaryViewModel(IMaintenanceService maintenanceService, IWindowManager windowManager)
        {
            this.maintenanceService = maintenanceService;
            _windowManager = windowManager;
            CreateCommands();
            GetModelItems();
        }

        private void CreateCommands()
        {
            SelectEmployeeCommand = new RelayCommand(SelectEmployeeCommand_Invoked);

        }

        private void SelectEmployeeCommand_Invoked()
        {
            var dataModel = new SelectWindowDataModel<Employees> { ItemList = _employees, WindowTitle = "Select employee" };
            var selectWindow = _windowManager.Show<SelectWindow>(dataModel);
            if (selectWindow != null)
            {
                selectWindow.OnOkClicked += SelectWindow_OnOkClicked;
            }
        }

        private void SelectWindow_OnOkClicked(object sender, EventArgs e)
        {
            if (e is SelectWindowEventArgs)
            {
                var args = e as SelectWindowEventArgs;
                if (args.Data is Employees)
                {
                    UpdateEmployee(args.Data as Employees);
                }
                
            }
        }

        private void UpdateEmployee(Employees employees)
        {
            SelectedEmployee = employees;
            SelectedModel.EmployeeId = employees.Id;
        }


        #region API Calls

        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.GetSalaries();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                _employees = response.Employees;
                if (response.Salaries != null && response.Salaries.Count > 0)
                {
                    ModelDtos = response.Salaries;

                    SelectedModel = ModelDtos.First();
                }
                else
                {
                    ModelDtos = null;
                }
            }
        }

        protected async override void AddNewModelItem(Salary model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = "tester";
            model.ModifiedDate = DateTime.UtcNow;
            var response = await maintenanceService.AddNewSalary(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new salary", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(Salary model)
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.UpdateSalary(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the salary", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(Salary model)
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.DeleteSalary(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the salary", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(Salary selectedModel)
        {
            if (selectedModel != null)
            {
                SelectedEmployee = selectedModel.Employee;
            }
        }
        #endregion

    }
}
