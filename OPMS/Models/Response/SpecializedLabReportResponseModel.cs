using OPMS.Models;
using OPMS.Models.Dto;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class SpecializedLabReportResponseModel : ModelBase
    {

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<SpecializedLabModelDto> SpecializedLabsSamples { get; set; } = new List<SpecializedLabModelDto>();
        public List<HdlRegistration> Labs { get; set; }

    }
}
