using GalaSoft.MvvmLight.Command;
using OPMS.Models;
using OPMS.Models.Report;
using OPMS.Services.Patient;
using OPMS.Services.RequestProvider;
using OPMS.Services.WindowManager;
using OPMS.Utility;
using OPMS.ViewModels.Base;
using OPMS.Windows;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace OPMS.ViewModels
{
    public class TestResultsViewModel : GenericViewModel<Patient>
    {

        #region Fields
        private readonly IPatientService patientService;
        private readonly IWindowManager windowManager;
        private List<ParsedFormula> _parserdFormulaIds = new List<ParsedFormula>();
        private List<TestResultsView> cachedTestTitles;
        Patient cachedPatient;
        #endregion


        #region Properties
        public ICommand NonCultureButton_Command { get; set; }
        public ICommand SelectPatientCommand { get; set; }


        public bool ShowGroupName
        {
            get => Properties.ReportPrintingOptions.Default.ShowGroupTitle;
            set
            {
                Properties.ReportPrintingOptions.Default.ShowGroupTitle = value;
            }
        }

        private List<SignaturePrototypes> signatures;
        public List<SignaturePrototypes> Signatures
        {
            get => signatures;
            set
            {
                signatures = value;
                RaisePropertyChanged(nameof(Signatures));
            }
        }

        private SignaturePrototypes leftSelectedSignature;
        public SignaturePrototypes LeftSelectedSignature
        {
            get => leftSelectedSignature;
            set
            {
                leftSelectedSignature = value;
                RaisePropertyChanged(nameof(LeftSelectedSignature));
            }
        }

        private SignaturePrototypes rightSelectedSignature;
        public SignaturePrototypes RightSelectedSignature
        {
            get => rightSelectedSignature;
            set
            {
                rightSelectedSignature = value;
                RaisePropertyChanged(nameof(RightSelectedSignature));
            }
        }

        private SearchItem selectedSearchItem;
        public SearchItem SelectedSearchItem
        {
            get => selectedSearchItem;
            set
            {
                selectedSearchItem = value;
                RaisePropertyChanged(nameof(SelectedSearchItem));
            }
        }

        private List<SearchItem> searchItems;
        public List<SearchItem> SearchItems
        {
            get => searchItems;
            set
            {
                searchItems = value;
                RaisePropertyChanged(nameof(SearchItems));
            }
        }

        List<TestResultsView> testResults = new List<TestResultsView>();
        public List<TestResultsView> TestResults
        {
            get => testResults;
            set
            {
                testResults = value;
                RaisePropertyChanged(nameof(TestResults));
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
                if (!string.IsNullOrEmpty(SearchText) && SelectedModel != null && TestResults != null)
                {
                    SetSearch(SearchText);
                }
                else
                {
                    TestResults = cachedTestTitles;
                    RaisePropertyChanged(nameof(SelectedModel));
                    cachedTestTitles = null;
                }
            }
        }
        #endregion

        public TestResultsViewModel(IPatientService patientService,
                                    IWindowManager windowManager)
        {
            this.patientService = patientService;
            this.windowManager = windowManager;
            EditToolbarButtonVisibilityState.FlavourThree();
            GetModelItems();
            SetSearchItemSelection();
            CreateCommands();
        }

        private void SetSearchItemSelection()
        {
            SearchItems = SearchItem.GetListItems();
            SelectedSearchItem = SearchItems?.First();
        }

        private void CreateCommands()
        {
            NonCultureButton_Command = new RelayCommand(NonCultureButton_Clicked);
            SelectPatientCommand = new RelayCommand(SelectePatientCommand_Invoked);
        }

        private void NonCultureButton_Clicked()
        {
            var dtos = TestResults.GroupBy(x => x.TitleId).Select(y => y.First()).ToList();
            var modelList = new List<TestGroupSelectionModel>();
            foreach (var dto in dtos)
            {
                modelList.Add(new TestGroupSelectionModel
                {
                    Id = dto.TitleId,
                    Select = true,
                    Title = dto.TitleName
                });
            }
            var dataModel = new SelectWindowDataModel<TestGroupSelectionModel> { ItemList = modelList };
            var selectTestGroupWindow = windowManager.Show<SelectTestGroupWindow>(dataModel);
            if (selectTestGroupWindow != null)
            {
                selectTestGroupWindow.OnOkClicked += SelectTestGroupWindow_OnOkClicked; ;
            }
        }

        private void SelectTestGroupWindow_OnOkClicked(object sender, EventArgs e)
        {
            if(e != null && e is SelectWindowEventArgs)
            {
                var args = e as SelectWindowEventArgs;
                var list = args.Data as List<TestGroupSelectionModel>;
                if(list != null)
                {
                    var testResults = new List<TestResultsView>();
                    foreach(var item in list)
                    {
                        testResults.AddRange(TestResults.FindAll(x => x.TitleId == item.Id));
                    }

                    Properties.ReportPrintingOptions.Default.Save();
                    //Show the report here
                    var reportModel = new TestResultsReportModel
                    {
                        Patient = new List<Patient> { SelectedModel },
                        TestResultsViews = testResults,
                        OptionsModel = new List<ReportPrintingOptionsModel> { new Models.ReportPrintingOptionsModel().SetupOptions() },
                        LeftSignature = new List<SignaturePrototypes> { LeftSelectedSignature },
                        RightSignature = new List<SignaturePrototypes> { RightSelectedSignature },
                        ReportType = BaseReportModel.REPORT_TYPE_TEST_RESULT,
                        ReportResourcePath = ReportResourceFilePath.TEST_RESULT
                    };
                    windowManager.Show<ReportWindow>(reportModel);
                }
            }
        }

        private void SelectePatientCommand_Invoked()
        {
            var dataModel = new SelectWindowDataModel<Patient> { ItemList = ModelDtos, WindowTitle = "Select Patient" };
            var selectWindow = windowManager.Show<SelectPatientWindow>(dataModel);
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

        internal void OnPatientSelected(Patient patient)
        {
            if (patient != null)
            {
                SelectedModel = ModelDtos?.Find(x => x.Id == patient.Id);
                if (SelectedModel == null)
                {
                    cachedPatient = patient;
                    EditButtonActiveStateModel.Edit = false;
                }
                else
                {
                    EditButtonActiveStateModel.Edit = true;
                }
            }
        }


        #region API Calls
        protected override void AddNewModelItem(Patient model)
        {

        }

        protected override void DeleteModelItem(Patient model)
        {

        }

        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await patientService.GetAllPatientWithTests();
            IsBusy = Visibility.Hidden;


            if (response != null && response.Patients.Count > 0)
            {
                Signatures = response.Signatures;
                EditButtonActiveStateModel.Add = false;
                EditButtonActiveStateModel.Edit = false;
                if (response.Patients.Count > 0)
                {
                    EditButtonActiveStateModel.Edit = true;
                    ModelDtos = response.Patients;

                    if (cachedPatient != null)
                    {
                        if (ModelDtos != null && ModelDtos.Count > 0)
                        {
                            SelectedModel = ModelDtos.Find(x => x.Id == cachedPatient.Id);
                            cachedPatient = null;
                        }
                    }
                    else
                    {
                        if (ModelDtos != null && ModelDtos.Count > 0)
                        {
                            SelectedModel = ModelDtos.Last();
                        }
                    }

                }
                else
                {
                    EditButtonActiveStateModel.Edit = false;
                    //Navigate to registration page.
                }
            }
            else
            {
                ModelDtos = null;
                EditButtonActiveStateModel.Add = false;
                EditButtonActiveStateModel.Edit = false;
                System.Windows.MessageBox.Show("No patients found. Please create a new patient in order to proceed");
            }
        }
        protected async override void UpdateModelItem(Patient model)
        {
            IsBusy = Visibility.Visible;
            model.TestResults = TestResults;
            var response = await patientService.UpdatePatientTestResults(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    cachedPatient = model;
                    GetModelItems();
                }
                else
                {
                    System.Windows.MessageBox.Show("Failed in updating the patient test results", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnGroupIdSelected(int id)
        {
        }

        protected async override void OnModelSelected(Patient selectedModel)
        {
            if (selectedModel != null && selectedModel.TestResults == null)
            {
                IsBusy = Visibility.Visible;
                var response = await patientService.GetPatientWithTests(new Models.Response.PatientTestResponseModel { PatientId = selectedModel.Id});
                IsBusy = Visibility.Hidden;
                if(response != null)
                {
                    TestResults = response.TestResults;
                    ParseFormula(TestResults);
                }
                
            }
        }


        private void ParseFormula(List<TestResultsView> testResults)
        {
            Regex regexCurlyBrac = new Regex(@"\{(.*?)\}");
            Regex regexSquareBrac = new Regex(@"\[(.*?)\]");


            foreach (var testResult in testResults)
            {
                if (testResult.Formula != null)
                {
                    testResult.ParsedFormula = new ParsedFormula();
                    testResult.ParsedFormula.OtherTestId = testResult.OtherTestId;
                    var matches = regexCurlyBrac.Matches(testResult.Formula.Formula);
                    if (matches.Count > 0)
                    {
                        foreach (var match in matches)
                        {
                            var matchesC = regexSquareBrac.Matches(match.ToString());
                            foreach (var matchC in matchesC)
                            {
                                var splitArr = matchC.ToString().Split('@');
                                if (splitArr.Length > 1)
                                {
                                    var testId = int.Parse(splitArr[1].Replace(']', '\0'));
                                    testResult.ParsedFormula.RelatedTestsMap.Add(match.ToString(), testId);
                                }
                            }
                        }
                    }
                }
            }
        }


        protected override void OnTestIdSelected(int id)
        {
        }
        #endregion

        new void SetSearch(string searchText)
        {
            if (cachedTestTitles == null)
            {
                cachedTestTitles = TestResults.ToList();
            }

            if (SelectedSearchItem != null)
            {
                switch (SelectedSearchItem.Id)
                {
                    case 1:
                        TestResults = cachedTestTitles.FindAll(x => x.GroupName.Contains(searchText)).ToList();
                        break;
                    case 2:
                        TestResults = cachedTestTitles.FindAll(x => x.TitleName.Contains(searchText)).ToList();
                        break;
                    case 3:
                        TestResults = cachedTestTitles.FindAll(x => x.TestName.Contains(searchText)).ToList();
                        break;
                }
            }
        }

        public void OnTestResultEdited(TestResultsView changedTestResult, string testResultValue)
        {
            if(TestResults != null)
            {
                foreach (var testResult in TestResults)
                {
                    if (testResult.OtherTestId == changedTestResult.OtherTestId)
                    {
                        testResult.TestResult = testResultValue;
                    }
                    else if (testResult.Formula != null)
                    {
                        try
                        {
                            testResult.TestResult = GetTestResultFromFormula(testResult);
                        }
                        catch (Exception e)
                        {
                            App.Logger.Error(e);
                        }

                    }

                }
            }
            
        }

        string GetTestResultFromFormula(TestResultsView testResult)
        {
            var formula = testResult.Formula.Formula;
            foreach (var parsedTestFromFormula in testResult.ParsedFormula.RelatedTestsMap)
            {
                var result = TestResults?.Find(x => x.OtherTestId == parsedTestFromFormula.Value);
                if (result != null)
                {
                    formula = formula.Replace(parsedTestFromFormula.Key, !string.IsNullOrEmpty(result.TestResult) ? result.TestResult : "0");
                }
            }
            if (DoesAllOtherTestContainsValues(testResult))
            {
                return new DataTable().Compute(formula, null).ToString();
            }
            else
            {
                return string.Empty;
            }

        }

        bool DoesAllOtherTestContainsValues(TestResultsView testResult)
        {
            foreach (var parsedTestFromFormula in testResult.ParsedFormula.RelatedTestsMap)
            {
                var result = TestResults.Find(x => x.OtherTestId == parsedTestFromFormula.Value);
                if (result != null)
                {
                    if (!string.IsNullOrEmpty(result.TestResult))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class ParsedFormula
    {
        public int OtherTestId { get; set; }
        public string TestResult { get; set; }
        public Dictionary<string, int> RelatedTestsMap { get; set; } = new Dictionary<string, int>();
        public ParsedFormula()
        {

        }
    }

    public class SearchItem
    {
        public string Title { get; set; }
        public int Id { get; set; }

        public static List<SearchItem> GetListItems()
        {
            return new List<SearchItem>
            {
                new SearchItem
                {
                    Title = "Test group",
                    Id = 1
                },
                new SearchItem
                {
                    Title = "Test title",
                    Id = 2
                },
                new SearchItem
                {
                    Title = "Other test",
                    Id = 3
                }
            };
        }
    }
}


