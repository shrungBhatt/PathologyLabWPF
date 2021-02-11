using GalaSoft.MvvmLight.Command;
using OPMS.Models;
using OPMS.Models.Dto;
using OPMS.Models.Request;
using OPMS.Repository;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPMS.ViewModels
{
    public class DrHospitalBillViewModel : GenericViewModel<HdlBill>
    {

        readonly IPatientService patientService;
        readonly IFinanceService financeService;
        readonly IWindowManager _windowManager;
        public DrHospitalBillViewModel(IFinanceService financeService,
            IWindowManager windowManager,
            IPatientService patientService)
        {
            this.financeService = financeService;
            this.patientService = patientService;
            _windowManager = windowManager;
            EditToolbarButtonVisibilityState.FlavourTwo();
            CreateCommands();
            GetModelItems();
        }

        #region Properties
        List<HdlRegistration> hdlRegistrations;
        public List<HdlRegistration> HdlRegistrations
        {
            get => hdlRegistrations;
            set
            {
                hdlRegistrations = value;
                RaisePropertyChanged(nameof(HdlRegistrations));
            }
        }

        HdlRegistration selectedHdlRegistration;
        public HdlRegistration SelectedHdlRegistration
        {
            get => selectedHdlRegistration;
            set
            {
                selectedHdlRegistration = value;
                RaisePropertyChanged(nameof(SelectedHdlRegistration));
            }
        }

        List<PatientWithRateListTestCharges> patients;
        public List<PatientWithRateListTestCharges> Patients
        {
            get => patients;
            set
            {
                patients = value;
                RaisePropertyChanged(nameof(Patients));
            }
        }

        PatientWithRateListTestCharges selectedPatient;
        public PatientWithRateListTestCharges SelectedPatient
        {
            get => selectedPatient;
            set
            {
                selectedPatient = value;
                RaisePropertyChanged(nameof(SelectedPatient));
            }
        }

        DateTime fromDate;
        public DateTime FromDate
        {
            get => fromDate;
            set
            {
                fromDate = value;
                RaisePropertyChanged(nameof(FromDate));
                if (FromDate != null && ToDate != null)
                {
                    if (FromDate <= ToDate)
                    {
                        if (SelectedHdlRegistration != null)
                        {
                            GetPatientsForTheGivenHdl(SelectedHdlRegistration, FromDate, ToDate);
                        }
                    }
                }
            }
        }

        DateTime toDate;
        public DateTime ToDate
        {
            get => toDate;
            set
            {
                toDate = value;
                RaisePropertyChanged(nameof(ToDate));
                if (FromDate != null && ToDate != null)
                {
                    if (ToDate >= FromDate)
                    {
                        if (SelectedHdlRegistration != null)
                        {
                            GetPatientsForTheGivenHdl(SelectedHdlRegistration, FromDate, ToDate);
                        }
                    }
                }
            }
        }

        bool suppressGroups;
        public bool SuppressGroups
        {
            get => suppressGroups;
            set
            {
                suppressGroups = value;
                RaisePropertyChanged(nameof(SuppressGroups));
            }
        }

        bool summaryBill;
        public bool SummaryBill
        {
            get => summaryBill;
            set
            {
                summaryBill = value;
                RaisePropertyChanged(nameof(SummaryBill));
            }
        }
        #endregion

        #region Commands
        public ICommand SelectHdlCommand { get; set; }
        public ICommand ResetBillNoCommand { get; set; }
        public ICommand PrintBillCommand { get; set; }
        public ICommand GoToPaymentCommand { get; set; }

        private void CreateCommands()
        {
            SelectHdlCommand = new RelayCommand(SelecteHdlCommand_Invoked);
            ResetBillNoCommand = new RelayCommand(ResetBillNoCommand_Invoked);
            PrintBillCommand = new RelayCommand(PrintBillCommand_Invoked);
            GoToPaymentCommand = new RelayCommand(GoToPaymentCommand_Invoked);
        }

        private void GoToPaymentCommand_Invoked()
        {
            var paymentView = new DrHospitalBillPaymentView();
            NavigationServiceSingleton.Instance.OPSMWindowFrameNavigationService.Navigate(paymentView, this.SelectedHdlRegistration);
        }

        private void PrintBillCommand_Invoked()
        {
            //Show the report here
            //var reportModel = new PatientBillReportModel
            //{
            //    Patient = new List<Patient> { SelectedPatient },
            //    PatientBill = new List<PatientBill> { SelectedModel },
            //    TestTitles = SelectedPatient.TestTitles,
            //    ReportType = BaseReportModel.REPORT_TYPE_PATIENT_BILL,
            //    ReportResourcePath = ReportResourceFilePath.PATIENT_BILL
            //};
            //_windowManager.Show<ReportWindow>(reportModel);
        }

        private void ResetBillNoCommand_Invoked()
        {
            Properties.Numbers.Default.hdl_bill_no = 1;
            Properties.Numbers.Default.Save();
            if (SelectedModel != null)
            {
                SelectedModel.BillNo = Properties.Numbers.Default.hdl_bill_no;
            }
        }

        private void SelecteHdlCommand_Invoked()
        {
            var dataModel = new SelectWindowDataModel<HdlRegistration> { ItemList = HdlRegistrations, WindowTitle = "Select Hospital/Doctor/Laboratory" };
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
                if (args.Data is HdlRegistration)
                {
                    OnHdlSelected((HdlRegistration)args.Data);
                }

            }
        }

        private void OnHdlSelected(HdlRegistration hdl)
        {
            if (hdl != null)
            {
                SelectedHdlRegistration = hdl;
                SelectedModel = hdl.Bill;
                FromDate = DateTime.Now;
                ToDate = DateTime.Now;
                Patients = null;
                if (SelectedModel == null)
                {
                    EditButtonActiveStateModel.Edit = false;
                    EditButtonActiveStateModel.Add = true;
                }
                else
                {
                    FromDate = hdl.Bill.FromDate;
                    ToDate = hdl.Bill.ToDate;
                    EditButtonActiveStateModel.Edit = true;
                    EditButtonActiveStateModel.Add = false;
                }
            }
        }
        #endregion

        #region API Calls
        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await financeService.GetHdlBills();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                if (response.HdlRegistrations != null && response.HdlRegistrations.Count > 0)
                {
                    this.HdlRegistrations = response.HdlRegistrations;
                    var hdl = this.HdlRegistrations.First();
                    if (hdl != null)
                    {
                        OnHdlSelected(hdl);
                    }
                }
            }
        }

        protected async override void AddNewModelItem(HdlBill model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = DIContainer.Resolve<UserRepository>().UserName;
            model.ModifiedDate = DateTime.Now;
            model.HdlId = (int)(SelectedHdlRegistration?.Id);
            model.FromDate = FromDate;
            model.ToDate = ToDate;
            var response = await financeService.InsertHdlBill(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    Properties.Numbers.Default.hdl_bill_no += 1;
                    Properties.Numbers.Default.Save();
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new bill", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(HdlBill model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = DIContainer.Resolve<UserRepository>().UserName;
            model.ModifiedDate = DateTime.Now;
            model.FromDate = FromDate;
            model.ToDate = ToDate;
            var response = await financeService.UpdateHdlBill(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the bill", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(HdlBill model)
        {
            IsBusy = Visibility.Visible;
            var response = await financeService.DeleteHdlBill(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the bill", "Error");
                }
            }
        }

        async void GetPatientsForTheGivenHdl(HdlRegistration hdl, DateTime fromDate, DateTime toDate)
        {
            IsBusy = Visibility.Visible;
            var requestModel = new PatientWithRateListChargesRequest
            {
                HdlId = hdl.Id,
                FromDate = fromDate,
                ToDate = toDate
            };
            var response = await patientService.GetPatientWithRateListCharges(requestModel);
            IsBusy = Visibility.Hidden;

            if (response != null)
            {
                if (response.Patients != null && response.Patients.Count > 0)
                {
                    Patients = response.Patients;
                    if (SelectedModel != null)
                    {
                        SelectedModel.TotalCharges = 0;
                        foreach (var patient in Patients)
                        {
                            SelectedModel.TotalCharges += patient.TotalCharges;
                        }
                    }
                }
            }
        }

        #endregion
        protected override void OnModelSelected(HdlBill selectedModel)
        {
            if (SelectedModel != null)
            {
                EditButtonActiveStateModel.Add = false;
            }
            else
            {
                EditButtonActiveStateModel.Add = true;
            }
        }

        public override void OnAddButtonClicked()
        {
            base.OnAddButtonClicked();
            if (SelectedModel != null)
            {
                SelectedModel.BillDate = DateTime.Now;
                SelectedModel.BillNo = Properties.Numbers.Default.hdl_bill_no;
                var today = DateTime.Now;
                FromDate = new DateTime(today.Year, today.Month, 1);
                ToDate = today;
            }
        }

        public override void OnEditButtonClicked()
        {
            base.OnEditButtonClicked();
            if (SelectedModel != null)
            {
                if (SelectedHdlRegistration != null && FromDate != null && ToDate != null)
                {
                    GetPatientsForTheGivenHdl(SelectedHdlRegistration, FromDate, ToDate);
                }
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
}
