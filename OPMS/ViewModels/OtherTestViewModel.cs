using OPMS.Controls;
using OPMS.Models.Test;
using OPMS.Repository;
using OPMS.Services.RequestProvider;
using OPMS.Services.Test;
using OPMS.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPMS.ViewModels
{
    public class OtherTestViewModel : GenericViewModel<OtherTestDto>
    {
        private readonly ITestService testService;
        TestTitleDto cachedModel;

        public OtherTestViewModel(ITestService testService)
        {
            this.testService = testService;

            GetModelItems();
        }

        #region API Calls        

        protected async override void GetModelItems()
        {
            SearchTitle = string.Empty;
            IsBusy = Visibility.Visible;
            var response = await testService.GetOtherTestResults();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                if (response.OtherTests != null)
                {
                    ModelDtos = SortOtherTests(response.OtherTests);
                    TestGroupTypeDtos = response.Groups;
                    TestTitleTypeDtos = response.Titles;

                    if (cachedModel != null)
                    {
                        SelectedModel = ModelDtos?.FirstOrDefault(x => x.TestTitleId == cachedModel.Id);
                        if (SelectedModel == null)
                        {
                            if (ModelDtos.Count > 0)
                            {
                                SelectedModel = ModelDtos.First();
                            }
                        }
                        cachedModel = null;
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

        List<OtherTestDto> SortOtherTests(List<OtherTestDto> otherTests)
        {
            var tests = new List<OtherTestDto>();
            var titleIds = new List<int>();

            foreach(var otherTest in otherTests)
            {
                if (!titleIds.Contains(otherTest.TestTitleId))
                {
                    titleIds.Add(otherTest.TestTitleId);
                }
            }

            foreach(var id in titleIds)
            {
                var xTests = otherTests.FindAll(x => x.TestTitleId == id);
                if(xTests != null)
                {
                    xTests = xTests.OrderBy(x => x.OrderBy).ToList();
                    tests.AddRange(xTests);
                }
            }
            return tests;
        }

        protected async override void AddNewModelItem(OtherTestDto model)
        {
            model.ModifiedBy = DIContainer.Resolve<UserRepository>().UserName;
            model.ModifiedDate = DateTime.UtcNow;
            if (model.TestTitleId == 0)
            {
                MessageBox.Show("Please select a test title");
                return;
            }

            if(model.TestGroupId == 0)
            {
                MessageBox.Show("Please select a test group");
                return;
            }
            IsBusy = Visibility.Visible;

            var response = await testService.AddNewOtherResult(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    newSelectedModel = new OtherTestDto();
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new test title", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
            else
            {
                SelectedModel = oldSelectedModel;
            }
        }

        protected async override void UpdateModelItem(OtherTestDto model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = DIContainer.Resolve<UserRepository>().UserName;
            model.ModifiedDate = DateTime.UtcNow;
            var response = await testService.UpdateOtherTest(model);
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

        protected async override void DeleteModelItem(OtherTestDto model)
        {
            IsBusy = Visibility.Visible;
            var response = await testService.DeleteOtherTest(model);
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
        protected override void OnModelSelected(OtherTestDto selectedModel)
        {
            GroupTypeSelectedItem = TestGroupTypeDtos?.Find(x => x.Id == selectedModel.TestGroupId);
            TitleTypeSelectedItem = TestTitleTypeDtos?.Find(x => x.Id == selectedModel.TestTitleId);
        }

        protected override void SetSearch(string searchTitle)
        {
            if (oldSelectedModels == null)
            {
                oldSelectedModels = new List<OtherTestDto>();
                oldSelectedModels.AddRange(ModelDtos);
            }

            ModelDtos = oldSelectedModels.FindAll(title => title.Name.Contains(searchTitle));
        }

        protected override void OnGroupIdSelected(int id)
        {
            SelectedModel.TestGroupId = id;
        }

        protected override void OnTestIdSelected(int id)
        {
            SelectedModel.TestTitleId = id;
        }
        #endregion

        public void OnTestTitleSelected(TestTitleDto model)
        {
            cachedModel = model;
        }

    }
}
