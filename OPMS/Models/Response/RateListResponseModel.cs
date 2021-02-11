using OPMS.Models;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class RateListResponseModel : ModelBase
    {
        public List<HdlRegistration> HdlRegistrations { get; set; }
        public List<RateListModel> RateListModels { get; set; }
        public List<TestTitles> TestTitles { get; set; }
    }
}
