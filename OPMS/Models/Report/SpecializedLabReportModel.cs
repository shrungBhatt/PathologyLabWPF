using OPMS.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Models.Report
{
    public class SpecializedLabReportModel : BaseReportModel
    {
        public List<SpecializedLabModelDto> LabSamples { get; set; }
        public List<SpecializedLabOptionsModel> Options { get; set; }
    }
}
