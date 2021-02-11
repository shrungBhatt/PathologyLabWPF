using GalaSoft.MvvmLight.Command;
using OPMS.Controls.MoreOptionsMenu;
using OPMS.Controls.ToolbarView;
using OPMS.Models;
using OPMS.Models.Patient;
using OPMS.Repository;
using OPMS.Services.Finance;
using OPMS.Services.Maintenance;
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPMS.ViewModels
{
    public class RegistrationViewModel : GenericViewModel<PatientDto>
    {

        #region Properties
        private List<TestGroups> testGroups;
        public List<TestGroups> TestGroups
        {
            get => testGroups;
            set
            {
                testGroups = value;
                RaisePropertyChanged(nameof(TestGroups));
            }
        }

        private ObservableCollection<TestTitles> testTitles = new ObservableCollection<TestTitles>();
        public ObservableCollection<TestTitles> TestTitles
        {
            get => testTitles;
            set
            {
                testTitles = value;
                RaisePropertyChanged(nameof(TestTitles));
            }
        }

        private List<Initials> initials;
        public List<Initials> Initials
        {
            get => initials;
            set
            {
                initials = value;
                RaisePropertyChanged(nameof(Initials));
            }
        }

        private Initials selectedInitial;
        public Initials SelectedInitial
        {
            get => selectedInitial;
            set
            {
                selectedInitial = value;
                RaisePropertyChanged(nameof(SelectedInitial));
                if (SelectedModel != null && selectedInitial != null)
                {
                    SelectedModel.Patient.InitialId = selectedInitial.Id;
                    if ((EditButtonActiveStateModel.EditToolbarMode == Controls.EditToolbarMode.ADD || EditButtonActiveStateModel.EditToolbarMode == Controls.EditToolbarMode.EDIT) && Genders != null)
                    {
                        if (SelectedInitial.Id == 2 || SelectedInitial.Id == 3)
                        {
                            SelectedGender = Genders?.Find(x => x.Id == 2);
                        }
                        else
                        {
                            SelectedGender = Genders?.Find(x => x.Id == 1);
                        }
                    }
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
                if (SelectedModel != null && selectedGender != null)
                {
                    SelectedModel.Patient.GenderId = selectedGender.Id;
                }
            }
        }

        private List<FieldOptions> addresses;
        public List<FieldOptions> Addresses
        {
            get => addresses;
            set
            {
                addresses = value;
                RaisePropertyChanged(nameof(Addresses));
            }
        }

        private FieldOptions selectedAddress;
        public FieldOptions SelectedAddress
        {
            get => selectedAddress;
            set
            {
                selectedAddress = value;
                RaisePropertyChanged(nameof(SelectedAddress));
                if (SelectedModel != null && selectedAddress != null)
                {
                    SelectedModel.Patient.Address = selectedAddress.Name;
                }
            }
        }

        private List<FieldOptions> occupations;
        public List<FieldOptions> Occupations
        {
            get => occupations;
            set
            {
                occupations = value;
                RaisePropertyChanged(nameof(Occupations));
            }
        }

        private FieldOptions selectedOccupation;
        public FieldOptions SelectedOccupation
        {
            get => selectedOccupation;
            set
            {
                selectedOccupation = value;
                RaisePropertyChanged(nameof(SelectedOccupation));
                if (SelectedModel != null && selectedOccupation != null)
                {
                    SelectedModel.Patient.OccupationId = selectedOccupation.Id;
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
                if (SelectedModel != null && selectedCivilStatus != null)
                {
                    SelectedModel.Patient.CivilStatusId = selectedCivilStatus.Id;
                }
            }
        }

        private List<RegistrationTypes> referredByTypes;
        public List<RegistrationTypes> ReferredByTypes
        {
            get => referredByTypes;
            set
            {
                referredByTypes = value;
                RaisePropertyChanged(nameof(ReferredByTypes));
            }
        }

        private List<HdlRegistration> _hdlRegistrations;
        public List<HdlRegistration> HdlRegistrations
        {
            get => _hdlRegistrations;
            set
            {
                _hdlRegistrations = value;
                RaisePropertyChanged(nameof(HdlRegistrations));
            }
        }

        private HdlRegistration selectedHdl;
        public HdlRegistration SelectedHdl
        {
            get => selectedHdl;
            set
            {
                selectedHdl = value;
                RaisePropertyChanged(nameof(SelectedHdl));
                if (SelectedHdl != null)
                {
                    SelectedModel.Patient.ReferredBy = SelectedHdl.Id;
                }
            }
        }

        private bool selectAllGroups = false;
        public bool SelectAllGroups
        {
            get => selectAllGroups;
            set
            {
                selectAllGroups = value;
                RaisePropertyChanged(nameof(SelectAllGroups));
                if (SelectAllGroups)
                {
                    SelectAllTestGroups(true);
                }
                else
                {
                    SelectAllTestGroups(false);
                }
            }
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                RaisePropertyChanged(nameof(SearchText));
                if (!string.IsNullOrEmpty(SearchText) && TestTitles != null)
                {
                    SetSearch(SearchText);
                }
                else
                {
                    TestTitles = GetTestTitles(cachedTestTitles);
                    RaisePropertyChanged(nameof(TestTitles));
                    cachedTestTitles = null;
                }
            }
        }

        bool showViewBill = false;
        public bool ShowViewBill
        {
            get => showViewBill;
            set
            {
                showViewBill = false;
                RaisePropertyChanged(nameof(ShowViewBill));
            }
        }

        bool? isHospitalOrLabSelected = null;
        public bool? IsHospitalOrLabSelected
        {
            get => isHospitalOrLabSelected;
            set
            {
                isHospitalOrLabSelected = value;
                RaisePropertyChanged(nameof(IsHospitalOrLabSelected));
                if (IsHospitalOrLabSelected.Value)
                {
                    HdlRegistrations = hdlRegistrations.FindAll(x => x.RegistrationTypeId == 2);
                }
            }
        }

        bool? isDoctorSelected = null;
        public bool? IsDoctorSelected
        {
            get => isDoctorSelected;
            set
            {
                isDoctorSelected = value;
                RaisePropertyChanged(nameof(IsDoctorSelected));
                if (IsDoctorSelected.Value)
                {
                    HdlRegistrations = hdlRegistrations.FindAll(x => x.RegistrationTypeId == 1);
                }
            }
        }

        private float billAmount;
        public float BillAmount
        {
            get => billAmount;
            set
            {
                billAmount = value;
                RaisePropertyChanged(nameof(BillAmount));
            }
        }
        #endregion

        #region Fields 
        private readonly IWindowManager _windowManager;
        private readonly IPatientService patientService;
        private readonly IMaintenanceService maintenanceService;
        readonly IFinanceService financeService;
        readonly UserRepository userRepository;
        const string WorkListId = "WorkList";
        const string UnfinishedListId = "UnfinishedListId";

        private List<TestTitles> _testTitles = new List<TestTitles>();
        private List<TestTitles> oldSelectedTestTitles = new List<TestTitles>();
        private List<TestTitles> cachedTestTitles = new List<TestTitles>();
        private List<HdlRegistration> hdlRegistrations = new List<HdlRegistration>();
        PatientDto cachedModelItem { get; set; }
        #endregion

        public RegistrationViewModel(IWindowManager windowManager,
                                     IPatientService patientService,
                                     UserRepository userRepository,
                                     IFinanceService financeService,
                                     IMaintenanceService maintenanceService)
        {
            this._windowManager = windowManager;
            this.patientService = patientService;
            this.userRepository = userRepository;
            this.financeService = financeService;
            this.maintenanceService = maintenanceService;
            moreOptionsMenuModels.Add(new MoreOptionsMenuModel { Id = WorkListId, Title = "Work List" });
            moreOptionsMenuModels.Add(new MoreOptionsMenuModel { Id = UnfinishedListId, Title = "Unfinished List" });
            MoreOptionsMenuModels = moreOptionsMenuModels;
            RaisePropertyChanged(nameof(MoreOptionsMenuModels));
            CreateCommands();
            GetModelItems();
        }

        internal override void PageIsLoaded(object sender, RoutedEventArgs e)
        {
        }

        internal override void PageIsUnloaded(object sender, RoutedEventArgs e)
        {

        }

        #region Patient selection
        public ICommand SelectPatientCommand { get; set; }
        public ICommand NavigateToAddHdlCommand { get; set; }
        public ICommand NavigateToPatientBillCommand { get; set; }
        public ICommand GenerateReceiptCommand { get; set; }
        public ICommand NavigateToTestResultsCommand { get; set; }
        public ICommand SendToSpecializedLabCommand { get; set; }
        public ICommand GenerateBillCommand { get; set; }

        private void CreateCommands()
        {
            SelectPatientCommand = new RelayCommand(SelectePatientCommand_Invoked);
            NavigateToAddHdlCommand = new RelayCommand(NavigateToAddHdlCommand_Invoked);
            NavigateToPatientBillCommand = new RelayCommand(NavigateToPatientBillCommand_Invoked);
            GenerateReceiptCommand = new RelayCommand(GenerateReceiptCommand_Invoked);
            NavigateToTestResultsCommand = new RelayCommand(NavigateToTestResultsCommand_Invoked);
            SendToSpecializedLabCommand = new RelayCommand(SendToSpecializedLabCommand_Invoked);
            GenerateBillCommand = new RelayCommand(GenerateBillCommand_Invoked);
        }

        private async void GenerateBillCommand_Invoked()
        {
            IsBusy = Visibility.Visible;
            var model = new PatientBill();
            model.ModifiedBy = userRepository.UserName;
            model.ModifiedDate = DateTime.Now;
            model.PatientId = (int)(SelectedModel?.Patient.Id);
            model.BillNo = Properties.Numbers.Default.patient_bill_no;
            model.BillDate = DateTime.Now;
            float charges = 0;
            if (SelectedModel != null && SelectedModel.SelectedTestTitles != null)
            {
                SelectedModel.SelectedTestTitles.ForEach(x => charges += x.Charges);
            }
            model.TotalCharges = charges;

            var response = await financeService.InsertPatientBill(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    Properties.Numbers.Default.patient_bill_no += 1;
                    Properties.Numbers.Default.Save();
                    cachedModelItem = SelectedModel;
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new patient bill", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        private void SendToSpecializedLabCommand_Invoked()
        {
            _windowManager.Show<SendToSpecializedLabWindow>(SelectedModel);
        }

        private void NavigateToTestResultsCommand_Invoked()
        {
            NavigationServiceSingleton.Instance.OPSMWindowFrameNavigationService.Navigate(OPSMWindow.GetUri<Views.TestResultsView>(), SelectedModel);
        }

        private async void GenerateReceiptCommand_Invoked()
        {
            IsBusy = Visibility.Visible;
            var model = new PatientBillPayment();
            model.BillId = SelectedModel.Patient.Bill.Id;
            model.PaymentAmount = SelectedModel.Patient.Bill.FinalAmount;
            model.PaymentDate = DateTime.Now;
            model.ReceiptNo = Properties.Numbers.Default.patient_receipt_no;
            model.PaymentType = 2003;
            model.CashMode = true;
            model.ModifiedBy = DIContainer.Resolve<UserRepository>().UserName;
            model.ModifiedDate = DateTime.Now;
            model.Bill = null;
            var response = await financeService.InsertPatientBillPayment(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    cachedModelItem = SelectedModel;
                    Properties.Numbers.Default.patient_receipt_no += 1;
                    Properties.Numbers.Default.Save();
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new patient bill payment record", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        private void NavigateToPatientBillCommand_Invoked()
        {
            NavigationServiceSingleton.Instance.OPSMWindowFrameNavigationService.Navigate(OPSMWindow.GetUri<PatientBillView>(), SelectedModel);
        }

        private void NavigateToAddHdlCommand_Invoked()
        {
            var window = _windowManager.Show<CommonWindow>(OPSMWindow.GetUri<DrLabHospitalRegistrationView>());
            window.OnCancelClicked += Window_OnCancelClicked;
        }

        private async void Window_OnCancelClicked(object sender, EventArgs e)
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.GetDhlRegistrations();
            IsBusy = Visibility.Hidden;

            if (response != null)
            {
                HdlRegistrations = response.HdlRegistrations;
                hdlRegistrations = response.HdlRegistrations.ToList();
                ReferredByTypes = response.RegistrationTypes;
                OnModelSelected(SelectedModel);
            }
        }

        private void SelectePatientCommand_Invoked()
        {
            var _patients = new List<Patient>();
            foreach (var model in ModelDtos)
            {
                _patients.Add(model.Patient);
            }
            var dataModel = new SelectWindowDataModel<Patient> { ItemList = _patients, WindowTitle = "Select Patient" };
            var selectWindow = _windowManager.Show<SelectPatientWindow>(dataModel);
            if (selectWindow != null)
            {
                selectWindow.OnOkClicked += SelectWindow_OnOkClicked;
            }
        }

        private void SelectPatientList(string id)
        {

            var _patients = new List<Patient>();
            foreach (var model in ModelDtos)
            {
                _patients.Add(model.Patient);
            }

            if (id.Equals(WorkListId))
            {
                _patients = _patients.FindAll(x => x.IsFinished);
            }
            else if (id.Equals(UnfinishedListId))
            {
                _patients = _patients.FindAll(x => !x.IsFinished);
            }

            var dataModel = new SelectWindowDataModel<Patient> { ItemList = _patients, WindowTitle = "Select Patient" };
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

        private void OnPatientSelected(Patient patient)
        {
            if (patient != null)
            {
                SelectedModel = ModelDtos?.Find(x => x.Patient.Id == patient.Id);
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
        #endregion

        #region API Calls
        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await patientService.GetAllPatients();
            IsBusy = Visibility.Hidden;

            if (response != null)
            {
                if (response.PatientDetails != null && response.PatientDetails.Count > 1)
                {
                    response.PatientDetails.Reverse();
                }

                ModelDtos = response.PatientDetails;
                TestGroups = response.TestGroups;
                _testTitles = response.TestTitles;

                if (response.FieldOptions != null)
                {
                    Addresses = response.FieldOptions.FindAll(x => x.FieldId == 1);
                    CivilStatuses = response.FieldOptions.FindAll(x => x.FieldId == 3);
                    Occupations = response.FieldOptions.FindAll(x => x.FieldId == 2);
                }
                ReferredByTypes = response.RegistrationTypes;
                hdlRegistrations = response.HdlRegistrations.ToList();
                Genders = response.Genders;
                Initials = response.Initials;

                if (ModelDtos != null && ModelDtos.Count > 0)
                {
                    if (cachedModelItem != null)
                    {
                        SelectedModel = ModelDtos.Find(x => x.Patient.Id == cachedModelItem.Patient.Id);
                        cachedModelItem = null;
                    }
                    else
                    {
                        SelectedModel = ModelDtos.First();
                    }

                    EditButtonActiveStateModel.Edit = true;

                    if (SelectedModel != null)
                    {
                        UpdateTestGroupSelection(SelectedModel);
                    }
                }
                else
                {
                    SelectedModel = null;
                    EditButtonActiveStateModel.Edit = false;
                }
            }
        }

        protected async override void AddNewModelItem(PatientDto model)
        {
            IsBusy = Visibility.Visible;
            model.Patient.ModifiedDate = DateTime.UtcNow;
            model.Patient.ModifiedBy = userRepository.UserName;
            
            if (SelectedHdl != null)
            {
                model.Patient.ReferredBy = SelectedHdl.Id;
            }
            var response = await patientService.InsertPatient(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                    OnLastButtonClicked();
                }
                else
                {
                    MessageBox.Show("Failed in registering new patient", "Error");
                    SelectedModel = oldSelectedModel;
                    SelectedModel.SelectedTestTitles = oldSelectedTestTitles;
                }
            }
        }

        protected async override void UpdateModelItem(PatientDto model)
        {
            IsBusy = Visibility.Visible;
            model.Patient.ModifiedDate = DateTime.UtcNow;
            model.Patient.ModifiedBy = userRepository.UserName;
            var response = await patientService.UpdatePatient(model);
            IsBusy = Visibility.Hidden;


            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    cachedModelItem = model;
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the patient details", "Error");
                    SelectedModel = oldSelectedModel;
                    SelectedModel.SelectedTestTitles = oldSelectedTestTitles;
                }
            }
        }

        protected async override void DeleteModelItem(PatientDto model)
        {
            IsBusy = Visibility.Visible;
            var response = await patientService.DeletePatient(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in deleting the patient details", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(PatientDto selectedModel)
        {

            if (Initials != null)
            {
                SelectedInitial = Initials.Find(x => x.Id == selectedModel.Patient?.InitialId);
            }

            if (Genders != null)
            {
                SelectedGender = Genders.Find(x => x.Id == selectedModel.Patient?.GenderId);
            }

            if (CivilStatuses != null)
            {
                SelectedCivilStatus = CivilStatuses.Find(x => x.Id == selectedModel.Patient?.CivilStatusId);
            }

            if (Addresses != null)
            {
                SelectedAddress = Addresses.Find(x => x.Name.Equals(selectedModel.Patient?.Address));
            }

            if (Occupations != null)
            {
                SelectedOccupation = Occupations.Find(x => x.Id == selectedModel.Patient?.OccupationId);
            }

            if (SelectedModel.Patient.ReferredBy.HasValue)
            {
                SelectedHdl = hdlRegistrations.Find(x => x.Id == SelectedModel.Patient.ReferredBy);
                if (SelectedHdl != null)
                {
                    if (SelectedHdl.RegistrationTypeId == 1)
                    {
                        IsDoctorSelected = true;
                        IsHospitalOrLabSelected = false;
                    }
                    else if(SelectedHdl.RegistrationTypeId == 2)
                    {
                        IsHospitalOrLabSelected = true;
                        IsDoctorSelected = false;
                    }
                }
            }

            HandleEnableStateOfBillButtons();

            if(SelectedModel.Patient != null)
            {
                if (SelectedModel.Patient.Bill == null)
                {
                    var total = 0f;
                    foreach (var test in SelectedModel.SelectedTestTitles)
                    {
                        total += test.Charges;
                    }

                    BillAmount = total;
                }
                else
                {
                    BillAmount = SelectedModel.Patient.Bill.FinalAmount;
                }
            }


            UpdateTestGroupSelection(selectedModel);
        }

        private void HandleEnableStateOfBillButtons()
        {
            if (EditButtonActiveStateModel.EditToolbarMode == Controls.EditToolbarMode.IDLE)
            {
                if (SelectedModel.Patient != null && SelectedModel.Patient.Bill != null)
                {
                    ShowViewBill = true;
                }
                else
                {
                    ShowViewBill = false;
                }
            }
        }

        public override void OnEditButtonClicked()
        {
            oldSelectedTestTitles.AddRange(SelectedModel.SelectedTestTitles);
            base.OnEditButtonClicked();
        }

        public override void OnCloseButtonClicked()
        {
            SelectedModel.SelectedTestTitles.Clear();
            SelectedModel.SelectedTestTitles.AddRange(oldSelectedTestTitles);
            SearchText = string.Empty;
            base.OnCloseButtonClicked();
        }

        protected override void OnOtherButtonClicked(string menuId)
        {
            switch (menuId)
            {
                case UnfinishedListId:
                    SelectPatientList(UnfinishedListId);
                    break;
                case WorkListId:
                    SelectPatientList(WorkListId);
                    break;
            }
        }
        #endregion

        #region Methods
        public void UpdateTestGroupSelection(PatientDto selectedModel)
        {
            if (TestGroups != null)
            {

                foreach (var group in TestGroups)
                {

                    if (selectedModel != null && selectedModel.SelectedTestTitles != null && selectedModel.SelectedTestTitles.Count > 0)
                    {
                        foreach (var title in selectedModel.SelectedTestTitles)
                        {
                            if (group.Id == title.GroupId)
                            {
                                group.IsSelected = true;
                                break;
                            }
                            else
                            {
                                group.IsSelected = false;
                            }
                        }
                    }
                    else
                    {
                        group.IsSelected = false;
                    }
                }
                RaisePropertyChanged(nameof(TestGroups));
                UpdateTestTitleSelection(TestGroups, selectedModel);

            }
        }

        public void UpdateTestTitleSelection(List<TestGroups> testGroups, PatientDto selectedModel)
        {
            TestTitles.Clear();
            foreach (var testGroup in testGroups)
            {
                var testTitles = _testTitles.FindAll(x => x.GroupId == testGroup.Id);
                if (testTitles.Count > 0 && testGroup.IsSelected)
                {
                    foreach (var testTitle in testTitles)
                    {
                        if (selectedModel != null && selectedModel.SelectedTestTitles != null && selectedModel.SelectedTestTitles.Count > 0)
                        {
                            foreach (var selectedTestTitle in selectedModel.SelectedTestTitles)
                            {
                                if (selectedTestTitle.Id == testTitle.Id)
                                {
                                    testTitle.IsSelected = true;
                                    break;
                                }
                                else
                                {
                                    testTitle.IsSelected = false;
                                }
                            }
                        }
                        else
                        {
                            testTitle.IsSelected = false;
                        }
                    }

                    foreach (var title in testTitles)
                    {
                        TestTitles.Add(title);
                    }
                }
            }
            RaisePropertyChanged(nameof(TestTitles));

        }

        public void OnTestGroupSelected(TestGroups selectedTestGroup, bool isSelected)
        {
            if (selectedTestGroup != null)
            {
                var testGroup = TestGroups.Find(x => x.Id == selectedTestGroup.Id);
                if (testGroup != null)
                {
                    if (isSelected)
                    {
                        testGroup.IsSelected = true;
                    }
                    else
                    {
                        testGroup.IsSelected = false;
                    }
                }
                RaisePropertyChanged(nameof(TestGroups));
                UpdateTestTitleSelection(TestGroups, SelectedModel);
            }
        }

        public void OnTestTitleSelected(TestTitles selectedTestTitle, bool isSelected)
        {
            if (selectedTestTitle != null)
            {
                var title = SelectedModel.SelectedTestTitles.Find(x => x.Id == selectedTestTitle.Id);
                if (title == null)
                {
                    if (isSelected)
                    {
                        SelectedModel.SelectedTestTitles.Add(selectedTestTitle);
                    }
                }
                else
                {
                    SelectedModel.SelectedTestTitles.Remove(title);
                }
            }
        }

        public void SelectAllTestGroups(bool doSelect)
        {
            if (doSelect)
            {
                if (TestGroups != null)
                {
                    TestGroups?.ForEach(x => x.IsSelected = true);
                }
            }
            else
            {
                if (TestGroups != null)
                {
                    TestGroups?.ForEach(x => x.IsSelected = false);
                }
            }
        }

        new void SetSearch(string searchText)
        {
            if (cachedTestTitles == null)
            {
                cachedTestTitles = TestTitles.ToList();
            }

            TestTitles = GetTestTitles(TestTitles.Where(x => x.Name.Contains(searchText)).ToList());
        }

        ObservableCollection<TestTitles> GetTestTitles(List<TestTitles> testTitles)
        {
            var list = new ObservableCollection<TestTitles>();
            if (testTitles != null)
            {

                foreach (var item in testTitles)
                {
                    list.Add(item);
                }
            }

            return list;
        }
        #endregion
    }
}
