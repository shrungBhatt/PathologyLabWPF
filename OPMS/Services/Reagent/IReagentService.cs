using OPMS.Models;
using OPMS.Models.Response;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Services.Reagent
{
    public interface IReagentService
    {
        Task<ReagentResponseModel> GetReagents();
        Task<ReagentResponseModel> GetTestReagentsRelations();
        Task<Reagents> AddNewReagent(Reagents reagent);
        Task<TestReagentRelation> AddNewTesetReagentRelation(TestReagentRelation testReagentRelation);
        Task<Reagents> UpdateReagent(Reagents reagent);
        Task<TestReagentRelation> UpdateTestReagentRelation(TestReagentRelation testReagentRelation);
        Task<Reagents> DeleteReagent(Reagents reagent);
        Task<TestReagentRelation> DeleteTestReagentRelation(TestReagentRelation testReagentRelation);

        Task<DealersResponseModel> GetDealers();
        Task<Dealers> AddNewDealer(Dealers dealer);
        Task<Dealers> UpdateDealer(Dealers dealer);
        Task<Dealers> DeleteDealer(Dealers dealer);

        Task<ReagentEntryResponseModel> GetReagentEntries();
        Task<ReagentBillEntries> AddNewReagentEntry(ReagentBillEntries reagentEntry);
        Task<ReagentBillEntries> UpdateReagentEntry(ReagentBillEntries reagentEntry);
        Task<ReagentBillEntries> DeleteReagentEntry(ReagentBillEntries reagentEntry);
    }
}
