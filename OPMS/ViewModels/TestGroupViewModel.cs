using GalaSoft.MvvmLight.Command;
using OPMS.Controls;
using OPMS.Models.Test;
using OPMS.Repository;
using OPMS.Services.RequestProvider;
using OPMS.Services.Test;
using OPMS.Services.WindowManager;
using OPMS.Utility;
using OPMS.ViewModels.Base;
using OPMS.Views;
using OPMS.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPMS.ViewModels
{
    public class TestGroupViewModel : GenericViewModel<TestGroupDto>
    {        

        private readonly ITestService testService;
        readonly IWindowManager windowManager;

        public TestGroupViewModel(ITestService testService, IWindowManager windowManager)
        {
            this.testService = testService;
            this.windowManager = windowManager;
            CreateCommands();
            GetModelItems();
        }

        public ICommand GoToTestTitlesCommand { get; set; }
        public ICommand AddTitlesFromOtherGroupCommand { get; set; }

        void CreateCommands()
        {
            GoToTestTitlesCommand = new RelayCommand(GoToTestTitlesCommand_Invoked);
            AddTitlesFromOtherGroupCommand = new RelayCommand(AddTitlesFromOtherGroupCommand_Invoked);
        }

        private void AddTitlesFromOtherGroupCommand_Invoked()
        {
            var window = windowManager.Show<SelectToAddTestTitlesToGroup>(SelectedModel.Id);
        }

        private void GoToTestTitlesCommand_Invoked()
        {
            var paymentView = OPSMWindow.GetUri<TestTitleView>();
            NavigationServiceSingleton.Instance.OPSMWindowFrameNavigationService.Navigate(paymentView, this.SelectedModel);
        }

        #region API Calls

        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await testService.GetTestGroups();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                if (response.TestGroups != null && response.TestGroups.Count > 0)
                {
                    ModelDtos = response.TestGroups;
                    EditButtonActiveStateModel.Edit = true;
                    SelectedModel = ModelDtos.First();
                }
                else
                {
                    ModelDtos = null;
                    EditButtonActiveStateModel.Edit = false;
                    
                }
            }
        }

        protected async override void AddNewModelItem(TestGroupDto model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = DIContainer.Resolve<UserRepository>().UserName;
            model.ModifiedDate = DateTime.Now;
            var response = await testService.AddNewTestGroup(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new test group", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(TestGroupDto model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = DIContainer.Resolve<UserRepository>().UserName;
            model.ModifiedDate = DateTime.Now;
            var response = await testService.UpdateTestGroup(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the test group", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(TestGroupDto model)
        {
            IsBusy = Visibility.Visible;
            var response = await testService.DeleteTestGroup(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the test group", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(TestGroupDto selectedModel)
        {
            //Do nothing
        }

        protected override void SetSearch(string searchTitle)
        {
            //Do nothing
        }

        protected override void OnGroupIdSelected(int id)
        {
            //Do nothing
        }

        protected override void OnTestIdSelected(int id)
        {
            //Do nothing
        }

        public override void OnAddButtonClicked()
        {
            base.OnAddButtonClicked();
            //if(SelectedModel != null)
            //{
            //    SelectedModel.OrderNo = 0;
            //    SelectedModel.Name = string.Empty;
            //}
        }
        #endregion
    }
}
