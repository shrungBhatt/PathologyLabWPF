using GalaSoft.MvvmLight.Command;
using OPMS.Models;
using OPMS.Services.Finance;
using OPMS.Services.WindowManager;
using OPMS.ViewModels.Base;
using OPMS.Windows;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPMS.ViewModels
{
    public class SalaryPaymentViewModel : ViewModelBase
    {
        #region Fields and Properties
        public List<string> Months { get; set; } = new List<string>();

        private string selectedMonth;
        public string SelectedMonth
        {
            get => selectedMonth;
            set
            {
                selectedMonth = value;
                RaisePropertyChanged(nameof(SelectedMonth));
                if(selectedMonth != null)
                {
                    OnMonthSelected(selectedMonth);
                }
            }
        }

        private string selectedYear;
        public string SelectedYear
        {
            get => selectedYear;
            set
            {
                selectedYear = value;
                RaisePropertyChanged(nameof(SelectedYear));
            }
        }

        private SalaryPayment selectedSalaryPayment;
        public SalaryPayment SelectedSalaryPayment
        {
            get => selectedSalaryPayment;
            set
            {
                selectedSalaryPayment = value;
                RaisePropertyChanged(nameof(SelectedSalaryPayment));
            }
        }

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

        public List<SalaryPayment> SalaryPayments { get; set; }
        public List<Employees> Employees { get; set; }
        public ICommand SelectEmployeeCommand { get; set; }

        #endregion


        readonly IFinanceService financeService;
        readonly IWindowManager _windowManager;
        public SalaryPaymentViewModel(IFinanceService financeService,
            IWindowManager windowManager)
        {
            this.financeService = financeService;
            _windowManager = windowManager;
            EditToolbarButtonVisibilityState.FlavourOne();
            Months.AddRange(DateTimeFormatInfo.CurrentInfo.MonthNames);
            CreateCommands();
            GetData();
            SelectTheCurrentMonthAndYear();
        }

        private void SelectTheCurrentMonthAndYear()
        {
            var currentDate = DateTime.Now;
            SelectedYear = currentDate.Year.ToString();
            SelectedMonth = DateTimeFormatInfo.CurrentInfo.GetMonthName(currentDate.Month);
        }

        private void CreateCommands()
        {
            SelectEmployeeCommand = new RelayCommand(SelecteEmployeeCommand_Invoked);
        }

        private void SelecteEmployeeCommand_Invoked()
        {
            var dataModel = new SelectWindowDataModel<Employees> { ItemList = Employees, WindowTitle = "Select Employee" };
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
                    OnEmployeeSelected((Employees)args.Data);
                }
                
            }
        }

        private void OnEmployeeSelected(Employees data)
        {
            SelectedEmployee = data;
            SelectedSalaryPayment = SalaryPayments?.FindAll(x => x.EmpId == data.Id)?.First();
            if(SelectedSalaryPayment == null)
            {
                //Create a new Salary payment for this employee.
            }
            else
            {
                SelectedMonth = SelectedSalaryPayment.MonthName;
                SelectedYear = SelectedSalaryPayment.YearName.ToString();
            }
        }

        private async void GetData()
        {
            IsBusy = Visibility.Visible;
            var response = await financeService.GetSalaryPayment();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                if (response.SalaryPayments != null && response.SalaryPayments.Count > 0)
                {
                    this.SalaryPayments = response.SalaryPayments;
                }

                if(response.Employees != null && response.Employees.Count > 0)
                {
                    this.Employees = response.Employees;
                }

            }
        }
        
        void OnMonthSelected(string month)
        {
            if(SelectedEmployee != null)
            {
                SelectedSalaryPayment = SalaryPayments?.FindAll(x => x.EmpId == SelectedEmployee.Id && x.MonthName.Equals(month))?.First();
                if (SelectedSalaryPayment == null)
                {
                    //Create a new Salary payment for this employee.
                }
            }
            
        }
    }
}
