using OPMS.Models.Dto;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class PatientRevenueReportResponseModel : BaseEntity
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<HdlRegistration> Hdls { get; set; }
        public List<FieldOptions> Occupations { get; set; }
        public List<PatientRevenueModelDto> RevenueItems { get; set; }
    }
}
