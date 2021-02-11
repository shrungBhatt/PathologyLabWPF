using OPMS.Views;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestResults = OPSMBackend.DataEntities.TestResultsView;
using PatientModel = OPSMBackend.DataEntities.Patient;
using System.Collections;

namespace OPMS.Models.Report
{
    public class TestResultsReportModel : BaseReportModel
    {
        public List<TestResults> TestResultsViews { get; set; }
        public List<PatientModel> Patient { get; set; }
        public List<ReportPrintingOptionsModel> OptionsModel { get; set; }
        public List<SignaturePrototypes> LeftSignature { get; set; }
        public List<SignaturePrototypes> RightSignature { get; set; }

    }
}
