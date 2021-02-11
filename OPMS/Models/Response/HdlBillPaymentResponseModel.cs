using OPMS.Models;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class HdlBillPaymentResponseModel : ModelBase
    {
        public List<HdlBillPayment> HdlBillPayments { get; set; }
        public List<FieldOptions> PaymentTypes { get; set; }
        public List<HdlRegistration> HdlRegistrations { get; set; }
    }
}
