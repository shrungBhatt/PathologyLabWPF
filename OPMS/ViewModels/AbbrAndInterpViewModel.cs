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
    public class AbbrAndInterpViewModel : GenericViewModel<Abbrevations>
    {

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

        public ICommand SelectTestCommand { get; set; }

        private List<OtherTests> _tests;

        private readonly IMaintenanceService maintenanceService;
        private readonly IWindowManager _windowManager;
        public AbbrAndInterpViewModel(IMaintenanceService maintenanceService, IWindowManager windowManager)
        {
            this.maintenanceService = maintenanceService;
            _windowManager = windowManager;
            CreateCommands();
            GetModelItems();
        }

        private void CreateCommands()
        {
            SelectTestCommand = new RelayCommand(SelectTestCommand_Invoked);

        }

        private void SelectTestCommand_Invoked()
        {
            var dataModel = new SelectWindowDataModel<OtherTests> { ItemList = _tests, WindowTitle = "Select test" };
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
                if (args.Data is OtherTests)
                {
                    UpdateEmployee(args.Data as OtherTests);
                }

            }
        }

        private void UpdateEmployee(OtherTests test)
        {
            SelectedTest = test;
            SelectedModel.OtherTestId = test.Id;
        }


        #region API Calls

        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.GetAbbrevations();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                _tests = response.OtherTests;
                if (response.Abbrevations != null && response.Abbrevations.Count > 0)
                {
                    ModelDtos = response.Abbrevations;
                    SelectedModel = ModelDtos.First();
                    EditButtonActiveStateModel.Edit = true;
                }
                else
                {
                    ModelDtos = null;
                    SelectedTest = null;
                    EditButtonActiveStateModel.Edit = false;
                }
            }
        }

        protected async override void AddNewModelItem(Abbrevations model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = "tester";
            model.ModifiedDate = DateTime.UtcNow;
            var response = await maintenanceService.AddNewAbbrevation(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new abbrevation", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(Abbrevations model)
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.UpdateAbbrevation(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the abbrevation", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(Abbrevations model)
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.DeleteAbbrevation(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the abbrevation", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(Abbrevations selectedModel)
        {
            if (selectedModel != null)
            {
                SelectedTest = selectedModel.OtherTest;
            }
        }
        #endregion

    }
}
