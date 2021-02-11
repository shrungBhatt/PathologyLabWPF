using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Models.Report
{
    public class BaseReportModel
    {
        public const int REPORT_TYPE_TEST_RESULT = 1;
        public const int REPORT_TYPE_PATIENT_BILL = 2;
        public const int REPORT_TYPE_HDL_BILL = 3;
        public const int REPORT_TYPE_HDL_BILL_PAYMENT = 4;
        public const int REPORT_TYPE_SPECIALIZED_LAB_REPORT = 5;
        public const int REPORT_TYPE_REFERRING_CUT_PAYMENT_REPORT = 6;
        public const int REPORT_PATIENT_REVENUE_REPORT = 7;
        public const int REPORT_PATIENT_BILL_PAYMENT_REPORT = 8;

        public string ReportResourcePath { get; set; }
        public int ReportType { get; set; }

    }
}
