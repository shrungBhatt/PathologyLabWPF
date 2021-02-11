using OPMS.Models.Response;
using OPMS.Models.Test;
using OPMS.Services.RequestProvider;
using OPMS.Utility;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Services.Test
{
    public class TestService : ITestService
    {

        private readonly IRequestProvider requestProvider;


        public TestService(IRequestProvider requestProvider)
        {
            this.requestProvider = requestProvider;
        }

        public async Task<Formulas> AddNewFormula(Formulas formula)
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_FORMULA_ADD);

            return await requestProvider.PostAsync(url, formula);
        }

        public async Task<OtherTestDto> AddNewOtherResult(OtherTestDto otherTestDto)
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_OTHER_ADD);

            return await requestProvider.PostAsync(url, otherTestDto);
        }

        public async Task<TestGroupDto> AddNewTestGroup(TestGroupDto testGroupDto)
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_GROUP_ADD);

            return await requestProvider.PostAsync(url, testGroupDto);
        }

        public async Task<TestTitleDto> AddNewTestTitle(TestTitleDto testTitleDto)
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_TITLE_ADD);

            return await requestProvider.PostAsync(url, testTitleDto);
        }

        public async Task<AddTestTitlesInGoupResponseModel> AddTestTitlesForOtherGroup(AddTestTitlesInGoupResponseModel model)
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_ADD_TEST_TITLES_FOR_OTHER_GROUP);

            return await requestProvider.PostAsync(url, model);
        }

        public async Task<Formulas> DeleteForumula(Formulas formula)
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_FORMULA_DELETE);

            return await requestProvider.DeleteAsync(url, formula, new Dictionary<string, string> { ["id"] = formula.Id.ToString() });
        }

        public async Task<OtherTestDto> DeleteOtherTest(OtherTestDto responseModel)
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_OTHER_DELETE);

            return await requestProvider.DeleteAsync(url, responseModel, new Dictionary<string, string> { ["id"] = responseModel.Id.ToString() });
        }

        public async Task<TestGroupDto> DeleteTestGroup(TestGroupDto responseModel)
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_GROUP_DELETE);

            return await requestProvider.DeleteAsync(url, responseModel, new Dictionary<string, string> { ["id"] = responseModel.Id.ToString() });

        }

        public async Task<TestTitleDto> DeleteTestTitle(TestTitleDto responseModel)
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_TITLE_DELETE);

            return await requestProvider.DeleteAsync(url, responseModel, new Dictionary<string, string> { ["id"] = responseModel.Id.ToString() });
        }

        public async Task<FormulasResponseModel> GetFormulas()
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_FORMULAS);

            return await requestProvider.GetAsync<FormulasResponseModel>(url);
        }

        public async Task<TestResponseModel> GetOtherTestResults()
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_OTHERS);

            return await requestProvider.GetAsync<TestResponseModel>(url);
        }

        public async Task<TestResponseModel> GetTestGroups()
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_GROUPS);

            return await requestProvider.GetAsync<TestResponseModel>(url);
        }

        public async Task<TestResponseModel> GetTestTitles()
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_TITLES);

            return await requestProvider.GetAsync<TestResponseModel>(url);
        }

        public Task<TestResponseModel> GetTestTitlesByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public async Task<AddTestTitlesInGoupResponseModel> GetTestTitlesForOtherGroup(AddTestTitlesInGoupResponseModel model)
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_GET_TEST_TITLES_FOR_OTHER_GROUP);

            return await requestProvider.PostAsync(url, model);
        }

        public async Task<Formulas> UpdateFormula(Formulas formula)
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_FORMULA_UPDATE);

            return await requestProvider.PutAsync(url, formula);
        }

        public async Task<OtherTestDto> UpdateOtherTest(OtherTestDto otherTestDto)
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_OTHER_UPDATE);

            return await requestProvider.PutAsync(url, otherTestDto);
        }

        public async Task<TestGroupDto> UpdateTestGroup(TestGroupDto testGroupDto)
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_GROUP_UPDATE);

            return await requestProvider.PutAsync(url, testGroupDto);
        }

        public async Task<TestTitleDto> UpdateTestTitle(TestTitleDto testTitleDto)
        {
            var url = URLBuilder.GetURL(Controllers.TEST, EndPoint.TEST_TITLE_UPDATE);

            return await requestProvider.PutAsync(url, testTitleDto);
        }
    }
}
