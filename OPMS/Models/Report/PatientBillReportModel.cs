using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Models.Report
{
    public class PatientBillReportModel : BaseReportModel
    {
        public List<OPSMBackend.DataEntities.Patient> Patient { get; set; }
        public List<TestTitles> TestTitles { get; set; }
        public List<PatientBill> PatientBill { get; set; }
    }
}
