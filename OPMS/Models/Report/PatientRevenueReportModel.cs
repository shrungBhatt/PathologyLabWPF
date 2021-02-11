using OPMS.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Models.Report
{
    public class PatientRevenueReportModel : BaseReportModel
    {
        public List<PatientRevenueModelDto> RevenueItems { get; set; }
        public List<SpecializedLabOptionsModel> Options { get; set; }
    }
}
