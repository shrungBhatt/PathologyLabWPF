using OPMS.Models.Dto;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class PatientBillPaymentReportResponeModel
    {
        public DateTime FromDate {get;set;}
        public DateTime ToDate { get; set; }
        public List<Users> Users { get; set; }
        public List<FieldOptions> PaymentTypes { get; set; }
        public List<PatientBillPaymentReportDto> BillPayments { get; set; }
    }
}
