using OPMS.Models;
using OPMS.Models.Response;
using OPMS.Services.RequestProvider;
using OPMS.Utility;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Services.Reagent
{
    public class ReagentService : IReagentService
    {
        private readonly IRequestProvider requestProvider;

        public ReagentService(IRequestProvider requestProvider)
        {
            this.requestProvider = requestProvider;
        }

        public async Task<Dealers> AddNewDealer(Dealers dealer)
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.DEALER_INSERT);

            return await requestProvider.PostAsync(url, dealer);
        }

        public async Task<Reagents> AddNewReagent(Reagents reagent)
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.REAGENT_ADD);

            return await requestProvider.PostAsync(url, reagent);
        }

        public async Task<ReagentBillEntries> AddNewReagentEntry(ReagentBillEntries reagentEntry)
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.REAGENT_ENTRY_INSERT);
            return await requestProvider.PostAsync(url, reagentEntry);
        }

        public async Task<TestReagentRelation> AddNewTesetReagentRelation(TestReagentRelation testReagentRelation)
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.TEST_REAGENT_RELATIONS_ADD);

            return await requestProvider.PostAsync(url, testReagentRelation);
        }

        public async Task<Dealers> DeleteDealer(Dealers dealer)
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.DEALER_DELETE);

            return await requestProvider.DeleteAsync(url, dealer, new Dictionary<string, string> { ["id"] = dealer.Id.ToString() });
        }

        public async Task<Reagents> DeleteReagent(Reagents reagent)
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.REAGENT_DELETE);

            return await requestProvider.DeleteAsync(url, reagent, new Dictionary<string, string> { ["id"] = reagent.Id.ToString() });
        }

        public async Task<ReagentBillEntries> DeleteReagentEntry(ReagentBillEntries reagentEntry)
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.REAGENT_ENTRY_DELETE);

            return await requestProvider.DeleteAsync(url, reagentEntry, new Dictionary<string, string> { ["id"] = reagentEntry.Id.ToString() });
        }

        public async Task<TestReagentRelation> DeleteTestReagentRelation(TestReagentRelation testReagentRelation)
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.TEST_REAGENT_RELATIONS_DELETE);

            return await requestProvider.DeleteAsync(url, testReagentRelation, new Dictionary<string, string> { ["id"] = testReagentRelation.Id.ToString() });
        }

        public async Task<DealersResponseModel> GetDealers()
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.DEALER_GET);
            return await requestProvider.GetAsync<DealersResponseModel>(url);
        }

        public async Task<ReagentEntryResponseModel> GetReagentEntries()
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.REAGENT_ENTRY_GET);
            return await requestProvider.GetAsync<ReagentEntryResponseModel>(url);
        }

        public async Task<ReagentResponseModel> GetReagents()
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.REAGENT_LIST);

            return await requestProvider.GetAsync<ReagentResponseModel>(url);
        }

        public async Task<ReagentResponseModel> GetTestReagentsRelations()
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.TEST_REAGENT_RELATIONS);

            return await requestProvider.GetAsync<ReagentResponseModel>(url);
        }

        public async Task<Dealers> UpdateDealer(Dealers dealer)
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.DEALER_UPDATE);

            return await requestProvider.PutAsync(url, dealer);
        }

        public async Task<Reagents> UpdateReagent(Reagents reagent)
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.REAGENT_UPDATE);

            return await requestProvider.PutAsync(url, reagent);
        }

        public async Task<ReagentBillEntries> UpdateReagentEntry(ReagentBillEntries reagentEntry)
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.REAGENT_ENTRY_UPDATE);

            return await requestProvider.PutAsync(url, reagentEntry);
        }

        public async Task<TestReagentRelation> UpdateTestReagentRelation(TestReagentRelation testReagentRelation)
        {
            var url = URLBuilder.GetURL(Controllers.REAGENT, EndPoint.TEST_REAGENT_RELATIONS_UPDATE);

            return await requestProvider.PutAsync(url, testReagentRelation);
        }
    }
}
