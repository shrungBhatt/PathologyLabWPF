using Newtonsoft.Json;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Dto
{
    public class PatientBillPaymentReportDto
    {
        public int ReceiptNo { get; set; }
        public int BillPaymentId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientCode { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool CashMode { get; set; }
        public bool ChequeMode { get; set; }
        public bool CardMode { get; set; }

        [JsonIgnore]
        public string PaymentType
        {
            get => BillPaymentType?.Name;
        }

        public string PaymentMode
        {
            get
            {

                if (CashMode)
                {
                    return "CASH";
                }
                else if (ChequeMode)
                {
                    return "CHEQUE";
                }
                else if (CardMode)
                {
                    return "CARD";
                }
                else
                {
                    return "";
                }
            }
        }

        public int PaymentTypeId { get; set; }
        public float Amount { get; set; }
        public string PaidBy { get; set; }
        public string EntryDoneBy { get; set; }
        public FieldOptions BillPaymentType { get; set; }
    }
}
