using GalaSoft.MvvmLight.Command;
using OPMS.Models.Dto;
using OPMS.Models.Test;
using OPMS.Repository;
using OPMS.Services.RequestProvider;
using OPMS.Services.Test;
using OPMS.Services.WindowManager;
using OPMS.ViewModels.Base;
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
    public class SelectToAddTestTitlesToGroupViewModel : ViewModelBase
    {

        private ObservableCollection<TestTitleSelectionDto> testTitles = new ObservableCollection<TestTitleSelectionDto>();
        public ObservableCollection<TestTitleSelectionDto> TestTitles
        {
            get => testTitles;
            set
            {
                testTitles = value;
                RaisePropertyChanged(nameof(TestTitles));
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
            }
        }

        List<TestTitles> cachedTestTitles;

        readonly ITestService testService;
        readonly IWindowManager windowManager;
        Window cachedWindow;

        internal int SelectedGroupId;

        public ICommand SaveCommand { get; set; }
        

        public SelectToAddTestTitlesToGroupViewModel(ITestService testService,
            IWindowManager windowManager)
        {
            this.testService = testService;
            this.windowManager = windowManager;
            SaveCommand = new RelayCommand(SavedCommand_Invoked);
        }

        private async void SavedCommand_Invoked()
        {
            IsBusy = Visibility.Visible;
            var model = new Models.Response.AddTestTitlesInGoupResponseModel();
            model.SelectedGroupId = SelectedGroupId;
            model.ModifiedBy = DIContainer.Resolve<UserRepository>().UserName;
            var selectedTestTitles = TestTitles.Where(x => x.Select).ToList();
            if(selectedTestTitles.Count > 0)
            {
                foreach(var title in selectedTestTitles)
                {
                    model.SelectedTestTitles.Add(cachedTestTitles.Find(x => x.Id == title.Id));
                }
            }

            var response = await testService.AddTestTitlesForOtherGroup(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    MessageBox.Show("Record saved successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    windowManager.ClosePopupWindow(cachedWindow);
                }
                else
                {
                    MessageBox.Show(response.Error.ErrorMessage, "Error");
                }
            }

        }

        private async void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await testService.GetTestTitlesForOtherGroup(new Models.Response.AddTestTitlesInGoupResponseModel { SelectedGroupId = this.SelectedGroupId });
            IsBusy = Visibility.Collapsed;

            if(response != null)
            {
                cachedTestTitles = response.TestTitles;
                var titles = response.TestTitles;
                foreach(var test in titles)
                {
                    if(test.GroupId != SelectedGroupId)
                    {
                        TestTitles.Add(TestTitleSelectionDto.GetDto(test));
                    }
                }
            }
        }

        internal override void PageIsLoaded(object sender, RoutedEventArgs e)
        {
            cachedWindow = sender as Window;
            GetModelItems();
            
        }
    }
}
