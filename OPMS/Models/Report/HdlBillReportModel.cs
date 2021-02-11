using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Models.Report
{
    public class HdlBillReportModel : BaseReportModel
    {
        public List<HdlRegistration> HdlRegistrations { get; set; }
        public List<HdlBill> HdlBills { get; set; }
        public List<TestTitles> TestTitles { get; set; }
        public List<TestGroups> TestGroups { get; set; }
    }
}
