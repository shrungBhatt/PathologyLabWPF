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
    public class TestTitleViewModel : GenericViewModel<TestTitleDto>
    {

        private readonly ITestService testService;
        readonly IWindowManager windowManager;
        TestGroupDto cachedModel;
        List<TestTitleDto> _cachedTitles;

        bool userNavigatedFromGroups;
        public bool UserNavigatedFromGroups
        {
            get => userNavigatedFromGroups;
            set
            {
                userNavigatedFromGroups = value;
                RaisePropertyChanged(nameof(UserNavigatedFromGroups));
            }
        }

        public TestTitleViewModel(ITestService testService, IWindowManager windowManager)
        {
            this.testService = testService;
            this.windowManager = windowManager;
            CreateCommands();
            GetModelItems();
        }

        public ICommand GoToOtherTestsCommand { get; set; }
        public ICommand HeaderEditCommand { get; set; }
        public ICommand FooterEditCommand { get; set; }

        private void CreateCommands()
        {
            GoToOtherTestsCommand = new RelayCommand(GoToOtherTestsCommand_Invoked);
            HeaderEditCommand = new RelayCommand(HeaderEditCommand_Invoked);
            FooterEditCommand = new RelayCommand(FooterEditCommand_Invoked);
        }

        private void FooterEditCommand_Invoked()
        {
            var editWindow = windowManager.Show<CreateRichTextWindow>(SelectedModel.FooterNote);
            if (editWindow != null)
            {
                editWindow.OnOkClicked += Footer_OnOkClicked;
            }
        }

        private void Footer_OnOkClicked(object sender, EventArgs e)
        {
            if (e is CreateRichTextWindowEventArgs)
            {
                var args = e as CreateRichTextWindowEventArgs;
                SelectedModel.FooterNote = args.Text;
            }
        }

        private void HeaderEditCommand_Invoked()
        {
            var editWindow = windowManager.Show<CreateRichTextWindow>(SelectedModel.HeaderNote);
            if (editWindow != null)
            {
                editWindow.OnOkClicked += Header_OnOkClicked;
            }
        }

        private void Header_OnOkClicked(object sender, EventArgs e)
        {
            if (e is CreateRichTextWindowEventArgs)
            {
                var args = e as CreateRichTextWindowEventArgs;
                SelectedModel.HeaderNote = args.Text;
            }
        }

        private void GoToOtherTestsCommand_Invoked()
        {
            var view = OPSMWindow.GetUri<OtherTestView>();
            NavigationServiceSingleton.Instance.OPSMWindowFrameNavigationService.Navigate(view, this.SelectedModel);
        }

        #region API Calls
        protected override async void GetModelItems()
        {
            SearchTitle = string.Empty;
            IsBusy = Visibility.Visible;
            var response = await testService.GetTestTitles();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                if (response.TestTitles != null)
                {
                    ModelDtos = response.TestTitles;
                    _cachedTitles = response.TestTitles;
                    TestGroupTypeDtos = response.Groups;


                    if (UserNavigatedFromGroups)
                    {
                        if (cachedModel != null)
                        {
                            SelectedModel = _cachedTitles?.Find(x => x.GroupId == cachedModel.Id);
                            if(SelectedModel == null)
                            {
                                GroupTypeSelectedItem = TestGroupTypeDtos?.Find(x => x.Id == cachedModel.Id);
                                
                            }
                            ModelDtos = _cachedTitles?.FindAll(x => x.GroupId == cachedModel.Id);
                        }
                    }
                    else
                    {
                        if (ModelDtos.Count > 0)
                        {
                            SelectedModel = ModelDtos.First();

                        }
                    }
                }
            }
        }

        protected override async void AddNewModelItem(TestTitleDto model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = DIContainer.Resolve<UserRepository>().UserName;
            model.ModifiedDate = DateTime.UtcNow;
            var response = await testService.AddNewTestTitle(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new test title", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected override async void UpdateModelItem(TestTitleDto model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = DIContainer.Resolve<UserRepository>().UserName;
            model.ModifiedDate = DateTime.UtcNow;
            var response = await testService.UpdateTestTitle(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the test title", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected override async void DeleteModelItem(TestTitleDto model)
        {
            IsBusy = Visibility.Visible;
            var response = await testService.DeleteTestTitle(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the test title", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(TestTitleDto selectedModel)
        {
            GroupTypeSelectedItem = TestGroupTypeDtos?.Find(x => x.Id == selectedModel.GroupId);
            RaisePropertyChanged(nameof(SelectedModel.HeaderNote));
            RaisePropertyChanged(nameof(SelectedModel.FooterNote));
        }

        int cachedTestTitleId = 0;
        protected override void OnGroupIdSelected(int id)
        {
            SelectedModel.GroupId = id;

            if (UserNavigatedFromGroups)
            {
                if (cachedTestTitleId != id)
                {
                    cachedTestTitleId = id;
                    ModelDtos = _cachedTitles?.FindAll(x => x.GroupId == id).ToList();
                }
            }

        }

        protected override void SetSearch(string searchTitle)
        {
            if (oldSelectedModels == null)
            {
                oldSelectedModels = new List<TestTitleDto>();
                oldSelectedModels.AddRange(ModelDtos);
            }

            ModelDtos = oldSelectedModels.FindAll(title => title.Name.Contains(searchTitle));
        }

        protected override void OnTestIdSelected(int id)
        {
            //Do nothing
        }

        public override void OnAddButtonClicked()
        {
            base.OnAddButtonClicked();
            if(cachedModel != null)
            {
                GroupTypeSelectedItem = TestGroupTypeDtos?.Find(x => x.Id == cachedModel.Id);
            }
        }
        #endregion

        public void OnGroupSelected(TestGroupDto testGroupDto)
        {
            cachedModel = testGroupDto;
            userNavigatedFromGroups = true;
        }

    }
}
