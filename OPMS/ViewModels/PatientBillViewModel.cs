using GalaSoft.MvvmLight.Command;
using OPMS.Models;
using OPMS.Models.Patient;
using OPMS.Models.Report;
using OPMS.Services.Finance;
using OPMS.Services.Patient;
using OPMS.Services.RequestProvider;
using OPMS.Services.WindowManager;
using OPMS.Utility;
using OPMS.ViewModels.Base;
using OPMS.Views;
using OPMS.Windows;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace OPMS.ViewModels
{
    public class PatientBillViewModel : GenericViewModel<PatientBill>
    {

        readonly IFinanceService financeService;
        readonly IPatientService patientService;
        readonly IWindowManager _windowManager;
        Patient cachedPatient;
        public PatientBillViewModel(IFinanceService financeService,
            IWindowManager windowManager,
            IPatientService patientService)
        {
            this.financeService = financeService;
            this.patientService = patientService;
            _windowManager = windowManager;
            EditToolbarButtonVisibilityState.FlavourTwo();
            CreateCommands();
            GetModelItems();
            SelectedPatient = null;
        }

        #region Field and properties
        private List<Patient> patients;
        public List<Patient> Patients
        {
            get => patients;
            set
            {
                patients = value;
                RaisePropertyChanged(nameof(Patients));
            }
        }

        private Patient selectedPatient;
        public Patient SelectedPatient
        {
            get => selectedPatient;
            set
            {
                selectedPatient = value;
                RaisePropertyChanged(nameof(SelectedPatient));
                if (SelectedPatient == null)
                {
                    EditButtonActiveStateModel.Edit = false;
                    EditButtonActiveStateModel.Add = false;
                }
                else
                {
                    EditButtonActiveStateModel.Edit = true;
                    EditButtonActiveStateModel.Add = true;
                }
            }
        }

        private List<TestModel> tests;
        public List<TestModel> Tests
        {
            get => tests;
            set
            {
                tests = value;
                RaisePropertyChanged(nameof(Tests));
            }
        }

        #endregion

        public ICommand SelectPatientCommand { get; set; }
        public ICommand ResetBillNoCommand { get; set; }
        public ICommand PrintBillCommand { get; set; }
        public ICommand GoToPaymentCommand { get; set; }

        private void CreateCommands()
        {
            SelectPatientCommand = new RelayCommand(SelectePatientCommand_Invoked);
            ResetBillNoCommand = new RelayCommand(ResetBillNoCommand_Invoked);
            PrintBillCommand = new RelayCommand(PrintBillCommand_Invoked);
            GoToPaymentCommand = new RelayCommand(GoToPaymentCommand_Invoked);
        }

        private void GoToPaymentCommand_Invoked()
        {
            var paymentView = new PatientBillPaymentView();
            NavigationServiceSingleton.Instance.OPSMWindowFrameNavigationService.Navigate(paymentView, this.SelectedPatient);
        }

        private void PrintBillCommand_Invoked()
        {
            //Show the report here
            var reportModel = new PatientBillReportModel
            {
                Patient = new List<Patient> { SelectedPatient },
                PatientBill = new List<PatientBill> { SelectedModel },
                TestTitles = SelectedPatient.TestTitles,
                ReportType = BaseReportModel.REPORT_TYPE_PATIENT_BILL,
                ReportResourcePath = ReportResourceFilePath.PATIENT_BILL
            };
            _windowManager.Show<ReportWindow>(reportModel);
        }

        private void ResetBillNoCommand_Invoked()
        {
            Properties.Numbers.Default.patient_bill_no = 1;
            Properties.Numbers.Default.Save();
            if (SelectedModel != null)
            {
                SelectedModel.BillNo = Properties.Numbers.Default.patient_bill_no;
            }
        }

        private void SelectePatientCommand_Invoked()
        {
            var dataModel = new SelectWindowDataModel<Patient> { ItemList = Patients, WindowTitle = "Select Patient" };
            var selectWindow = _windowManager.Show<SelectPatientWindow>(dataModel);
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
                if (args.Data is Patient)
                {
                    OnPatientSelected((Patient)args.Data);
                }

            }
        }

        internal void SaveCachedPatient(Patient patient)
        {
            cachedPatient = patient;
        }

        internal void OnPatientSelected(Patient patient)
        {
            if (patient != null)
            {
                SelectedPatient = patient;
                Tests = TestModel.GetTests(patient.TestTitles);
                SelectedModel = patient.Bill;
                if (SelectedModel == null)
                {
                    EditButtonActiveStateModel.Edit = false;
                }
                else
                {
                    EditButtonActiveStateModel.Edit = true;
                }
            }
        }

        protected override void OnRefreshButtonClicked()
        {
            OnCloseButtonClicked();
            GetModelItems();
        }

        #region API Calls
        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await patientService.GetPatientBills();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                if (response.Patients != null && response.Patients.Count > 0)
                {
                    this.Patients = response.Patients;
                    if (cachedPatient != null)
                    {
                        var patient = Patients.Find(x => x.Id == cachedPatient.Id);
                        if (patient != null)
                        {
                            OnPatientSelected(patient);
                        }
                        cachedPatient = null;
                    }
                    else
                    {
                        var patient = this.Patients.First();
                        if (patient != null)
                        {
                            OnPatientSelected(patient);
                        }
                    }
                   
                    
                }
            }
        }

        protected async override void AddNewModelItem(PatientBill model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = "tester";
            model.ModifiedDate = DateTime.Now;
            model.PatientId = (int)(SelectedPatient?.Id);
            var response = await financeService.InsertPatientBill(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    Properties.Numbers.Default.patient_bill_no += 1;
                    Properties.Numbers.Default.Save();
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new patient bill", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(PatientBill model)
        {
            IsBusy = Visibility.Visible;
            var response = await financeService.UpdatePatientBill(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the new patient bill", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(PatientBill model)
        {
            IsBusy = Visibility.Visible;
            var response = await financeService.DeletePatientBill(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the patient bill", "Error");
                }
            }
        }
        #endregion
        protected override void OnModelSelected(PatientBill selectedModel)
        {
            if (SelectedModel != null)
            {
                EditButtonActiveStateModel.Add = false;
            }
            else
            {
                EditButtonActiveStateModel.Add = true;
            }

            if (selectedModel != null && selectedModel.TotalCharges == 0)
            {
                //Calculate total charges
                if (Tests != null)
                {
                    foreach (var test in Tests)
                    {
                        if (!string.IsNullOrEmpty(test.Amount))
                        {
                            selectedModel.TotalCharges += float.Parse(test.Amount);
                        }
                    }
                }
            }
        }

        public override void OnAddButtonClicked()
        {
            base.OnAddButtonClicked();
            if (SelectedModel != null)
            {
                SelectedModel.BillDate = DateTime.Now;
                SelectedModel.BillNo = Properties.Numbers.Default.patient_bill_no;

                //Calculate total charges
                if (Tests != null)
                {
                    foreach (var test in Tests)
                    {
                        if (string.IsNullOrEmpty(test.Amount))
                        {
                            SelectedModel.TotalCharges += float.Parse(test.Amount);
                        }
                    }
                }
            }
        }

        public override void OnEditButtonClicked()
        {
            base.OnEditButtonClicked();
            if (SelectedModel != null)
            {
                EditButtonActiveStateModel.Add = false;
            }
            else
            {
                EditButtonActiveStateModel.Add = true;
            }
        }

        public override void OnCloseButtonClicked()
        {
            base.OnCloseButtonClicked();
            if (SelectedModel != null)
            {
                EditButtonActiveStateModel.Add = false;
                EditButtonActiveStateModel.Edit = true;
            }
            else
            {
                EditButtonActiveStateModel.Add = true;
                EditButtonActiveStateModel.Edit = false;
            }
        }
    }

    public class TestModel
    {
        public string GroupName { get; set; }
        public string TestName { get; set; }
        public string Amount { get; set; }

        public static List<TestModel> GetTests(List<TestTitles> testTitles)
        {
            var tests = new List<TestModel>();
            foreach (var test in testTitles)
            {
                tests.Add(new TestModel
                {
                    GroupName = test.Group?.Name,
                    TestName = test.Name,
                    Amount = test.Charges.ToString()
                });
            }
            return tests;
        }

    }
}
