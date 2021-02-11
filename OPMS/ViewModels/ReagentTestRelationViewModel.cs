using GalaSoft.MvvmLight.Command;
using OPMS.Models;
using OPMS.Services.Reagent;
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
    public class ReagentTestRelationViewModel : GenericViewModel<TestReagentRelation>
    {

        private Reagents selectedReagent;
        public Reagents SelectedReagent
        {
            get => selectedReagent;
            set
            {
                selectedReagent = value;
                RaisePropertyChanged(nameof(SelectedReagent));
            }
        }

        private OtherTests selectedTest;
        public OtherTests SelectedTest
        {
            get => selectedTest;
            set
            {
                selectedTest = value;
                RaisePropertyChanged(nameof(SelectedTest));
            }
        }

        public ICommand SelectReagentCommand { get; set; }
        public ICommand SelectTestCommand { get; set; }

        private List<OtherTests> _tests;
        private List<Reagents> _reagents;

        private readonly IReagentService _reagentService;
        private readonly IWindowManager _windowManager;
        public ReagentTestRelationViewModel(IReagentService reagentService, IWindowManager windowManager)
        {
            _reagentService = reagentService;
            _windowManager = windowManager;
            CreateCommands();
            GetModelItems();
        }

        private void CreateCommands()
        {
            SelectReagentCommand = new RelayCommand(SelecteReagentCommand_Invoked);
            SelectTestCommand = new RelayCommand(SelectTestCommand_Invoked);

        }

        private void SelectTestCommand_Invoked()
        {
            var dataModel = new SelectWindowDataModel<OtherTests> { ItemList = _tests, WindowTitle = "Select test" };
            var selectWindow = _windowManager.Show<SelectWindow>(dataModel);
            if(selectWindow != null)
            {
                selectWindow.OnOkClicked += SelectWindow_OnOkClicked;
            }
        }

        private void SelectWindow_OnOkClicked(object sender, EventArgs e)
        {
            if(e is SelectWindowEventArgs)
            {
                var args = e as SelectWindowEventArgs;
                if(args.Data is Reagents)
                {
                    UpdateReagentInSelectedModel(args.Data as Reagents);
                }else if(args.Data is OtherTests)
                {
                    UpdateTestInSelectedModel(args.Data as OtherTests);
                }
            }
        }

        private void UpdateTestInSelectedModel(OtherTests otherTest)
        {
            SelectedTest = otherTest;
            SelectedModel.OtherTestId = otherTest.Id;
        }

        private void UpdateReagentInSelectedModel(Reagents reagent)
        {
            SelectedReagent = reagent;
            SelectedModel.ReagentId = reagent.Id;
        }

        private void SelecteReagentCommand_Invoked()
        {
            var dataModel = new SelectWindowDataModel<Reagents> { ItemList = _reagents, WindowTitle = "Select Reagent" };
            var selectWindow = _windowManager.Show<SelectWindow>(dataModel);
            if (selectWindow != null)
            {
                selectWindow.OnOkClicked += SelectWindow_OnOkClicked;
            }
        }

        #region API Calls

        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await _reagentService.GetTestReagentsRelations();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                _tests = response.Tests;
                _reagents = response.Reagents;
                if (response.TestReagentRelations != null && response.TestReagentRelations.Count > 0)
                {
                    ModelDtos = response.TestReagentRelations;

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

        protected async override void AddNewModelItem(TestReagentRelation model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = "tester";
            model.ModifiedDate = DateTime.UtcNow;
            var response = await _reagentService.AddNewTesetReagentRelation(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new reagent test relation", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(TestReagentRelation model)
        {
            IsBusy = Visibility.Visible;
            var response = await _reagentService.UpdateTestReagentRelation(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the reagent test relation", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(TestReagentRelation model)
        {
            IsBusy = Visibility.Visible;
            var response = await _reagentService.DeleteTestReagentRelation(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the reagent test relation", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(TestReagentRelation selectedModel)
        {
            if(selectedModel != null)
            {
                SelectedReagent = selectedModel.ReagentModel;
                SelectedTest = selectedModel.OtherTest;
            }
        }
        #endregion
    }
}
