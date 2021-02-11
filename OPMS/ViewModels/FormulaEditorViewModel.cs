using GalaSoft.MvvmLight.Command;
using OPMS.Models.Test;
using OPMS.Services.RequestProvider;
using OPMS.Services.Test;
using OPMS.ViewModels.Base;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPMS.ViewModels
{
    public class FormulaEditorViewModel : GenericViewModel<Formulas>
    {

        #region Field and Properties
        private readonly ITestService testService;

        private List<OtherTests> _otherTests;

        private OtherTests selectedOtherTest;
        public OtherTests SelectedOtherTest
        {
            get => selectedOtherTest;
            set
            {
                selectedOtherTest = value;
                RaisePropertyChanged(nameof(SelectedOtherTest));
                if(SelectedOtherTest != null)
                {
                    OnOtherTestSelected(SelectedOtherTest.Id);
                }
                
            }
        }


        private List<OtherTests> selectedOtherTests;
        public List<OtherTests> SelectedOtherTests
        {
            get => selectedOtherTests;
            set
            {
                selectedOtherTests = value;
                RaisePropertyChanged(nameof(SelectedOtherTests));
            }
        }

        private List<TestTitles> selectedTestTitles;
        public List<TestTitles> SelectedTestTitles
        {
            get => selectedTestTitles;
            set
            {
                selectedTestTitles = value;
                RaisePropertyChanged(nameof(SelectedTestTitles));
            }
        }

        private string formula;
        public string Formula
        {
            get => formula;
            set
            {
                formula = value;
                RaisePropertyChanged(nameof(Formula));
            }
        }

        private OtherTests selectedOtherTestForFormula;
        public OtherTests SelectedOtherTestForFormula
        {
            get => selectedOtherTestForFormula;
            set
            {
                selectedOtherTestForFormula = value;
                RaisePropertyChanged(nameof(SelectedOtherTestForFormula));
                if(SelectedOtherTestForFormula != null)
                {
                    UpdateFormula(SelectedOtherTestForFormula);
                }
            }
        }

        public ICommand ParserFormulaCommand { get; set; }

        private void CreateCommands()
        {
            ParserFormulaCommand = new RelayCommand(OnParserForumulaCommand_Invoked);
        }

        private void OnParserForumulaCommand_Invoked()
        {
            string result = string.Empty;
            Regex regexA = new Regex(@"\{(.*?)\}");
            Regex regexC = new Regex(@"\[(.*?)\]");

            var listOfTests = new List<int>();

            var matches = regexA.Matches(Formula);
            if (matches.Count > 0)
            {
                foreach (var match in matches)
                {
                    var matchesC = regexC.Matches(match.ToString());
                    foreach(var matchC in matchesC)
                    {
                        var splitArr = matchC.ToString().Split('@');
                        if(splitArr.Length > 1)
                        {
                            var testId = int.Parse(splitArr[1].Replace(']','\0'));
                            if (!listOfTests.Contains(testId))
                            {
                                listOfTests.Add(testId);
                            }
                            
                        }
                    }
                }
            }

            foreach(var id in listOfTests)
            {
                result += $"\n{_otherTests?.Find(x => x.Id == id)?.Name}";
            }

            result += $"\n\nTotal count of tests in the formula is {listOfTests.Count}. If the count is less than then entered tests titles, check whether each test title is covered with {{ }} braces or not.";

            MessageBox.Show(result, "Parse results", MessageBoxButton.OK, MessageBoxImage.Information);
            
        }

        #endregion

        public FormulaEditorViewModel(ITestService testService)
        {
            this.testService = testService;
            CreateCommands();
            GetModelItems();
        }

        #region API Calls
        protected async override void AddNewModelItem(Formulas model)
        {
            IsBusy = Visibility.Visible;
            model.Formula = Formula ?? string.Empty;
            model.ModifiedBy = "admin";
            model.ModifiedDate = DateTime.UtcNow;
            var response = await testService.AddNewFormula(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Error occurred while creating new formula", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(Formulas model)
        {
            IsBusy = Visibility.Visible;
            var response = await testService.DeleteForumula(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the formula", "Error");
                }
            }
        }

        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await testService.GetFormulas();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                if (response.Formulas != null)
                {
                    TestGroupsDtos = response.Groups;
                    TestTitlesDtos = response.Titles;
                    _otherTests = response.OtherTests;
                    ModelDtos = response.Formulas;

                    if (ModelDtos.Count > 0)
                    {
                        SelectedModel = ModelDtos.First();

                    }
                }
            }
        }


        protected async override void UpdateModelItem(Formulas model)
        {
            IsBusy = Visibility.Visible;
            model.Formula = Formula ?? string.Empty;
            var response = await testService.UpdateFormula(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Error occurred while updating the formula", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }
        #endregion

        #region Override Methods

        protected override void OnModelSelected(Formulas selectedModel)
        {
            if (selectedModel != null)
            {
                SelectedTestGroup = TestGroupsDtos?.Find(x => x.Id == selectedModel.GroupId);
            }
        }

        protected override void OnGroupIdSelected(int id)
        {
            if (SelectedModel != null)
            {
                SelectedOtherTest = null;
                SelectedOtherTests = null;
                Formula = null;

                SelectedModel.GroupId = id;
                SelectedTestTitles = null;
                SelectedTestTitles = TestTitlesDtos?.FindAll(x => x.GroupId == id);
                SelectedTestTitle = SelectedTestTitles?.Find(x => x.Id == SelectedModel.TitleId);
            }

        }

        protected override void OnTestIdSelected(int id)
        {

            if (SelectedModel != null)
            {
                SelectedModel.TitleId = id;
                SelectedOtherTests = _otherTests?.FindAll(x => x.TestTitleId == id);
                SelectedOtherTest = SelectedOtherTests?.Find(x => x.Id == SelectedModel.TestId);
            }
        }

        

        protected override void SetSearch(string searchTitle)
        {

        }

        public override void OnAddButtonClicked()
        {
            base.OnAddButtonClicked();
            Formula = string.Empty;
            SelectedTestGroup = null;
            SelectedTestTitle = null;
            SelectedTestTitles = null;
            SelectedOtherTest = null;
            SelectedOtherTests = null;
        }
        #endregion

        private void OnOtherTestSelected(int id)
        {
            if (SelectedModel != null)
            {
                if (SelectedModel.TestId == id)
                {
                    Formula = SelectedModel.Formula;
                }
                else
                {
                    SelectedModel.TestId = id;
                }

            }
        }

        private void UpdateFormula(OtherTests selectedOtherTestForFormula)
        {
            if (Formula == null)
            {
                Formula = string.Empty;
            }

            Formula += $" {{{selectedOtherTestForFormula.Name}[id@{selectedOtherTestForFormula.Id}]}}";
        }
    }
}
