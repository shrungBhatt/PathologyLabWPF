using OPMS.Models;
using OPMS.Models.Dto;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class HdlReferringCutReportResponseModel : ModelBase
    {

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<HdlReferringCutModelDto> HdlReferringCutModelDtos { get; set; } = new List<HdlReferringCutModelDto>();
        public List<HdlRegistration> Hdls { get; set; }

    }
}
