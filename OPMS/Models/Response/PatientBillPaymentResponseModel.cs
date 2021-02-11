using OPMS.Models;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class PatientBillPaymentResponseModel : ModelBase
    {
        public List<PatientBillPayment> BillPayments { get; set; }
        public List<FieldOptions> PaymentTypes { get; set; }
        public List<Patient> Patients { get; set; }
    }
}
