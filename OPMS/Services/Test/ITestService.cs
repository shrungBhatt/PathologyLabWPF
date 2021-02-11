using OPMS.Models.Response;
using OPMS.Models.Test;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Services.Test
{
    public interface ITestService
    {
        Task<TestResponseModel> GetTestGroups();
        Task<TestResponseModel> GetTestTitles();
        Task<TestResponseModel> GetTestTitlesByGroupId(int groupId);
        Task<TestResponseModel> GetOtherTestResults();
        Task<FormulasResponseModel> GetFormulas();
        Task<TestGroupDto> AddNewTestGroup(TestGroupDto testGroupDto);
        Task<TestGroupDto> UpdateTestGroup(TestGroupDto testGroupDto);
        Task<TestGroupDto> DeleteTestGroup(TestGroupDto responseModel);
        Task<TestTitleDto> AddNewTestTitle(TestTitleDto testTitleDto);
        Task<TestTitleDto> UpdateTestTitle(TestTitleDto testTitleDto);
        Task<TestTitleDto> DeleteTestTitle(TestTitleDto responseModel);
        Task<OtherTestDto> AddNewOtherResult(OtherTestDto otherTestDto);
        Task<OtherTestDto> UpdateOtherTest(OtherTestDto otherTestDto);
        Task<OtherTestDto> DeleteOtherTest(OtherTestDto responseModel);
        Task<Formulas> AddNewFormula(Formulas formula);
        Task<Formulas> UpdateFormula(Formulas formula);
        Task<Formulas> DeleteForumula(Formulas formula);

        Task<AddTestTitlesInGoupResponseModel> GetTestTitlesForOtherGroup(AddTestTitlesInGoupResponseModel model);

        Task<AddTestTitlesInGoupResponseModel> AddTestTitlesForOtherGroup(AddTestTitlesInGoupResponseModel model);
    }
}
