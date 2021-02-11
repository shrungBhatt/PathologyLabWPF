using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class OtherIncome : BaseEntity
    {
        public int Id { get; set; }
        public int ReceiptNo { get; set; }
        public string NatureOfWork { get; set; }
        public DateTime IncomeDate { get; set; }
        public decimal Amount { get; set; }
        public bool? CashMode { get; set; }
        public bool? ChequeMode { get; set; }
        public bool? CardMode { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string CardNo { get; set; }
        [IgnoreColumn]
        public int AccountHeadId { get; set; }
        public string AccountHeadName { get => AccountHead?.Name; }
        public string PaidBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        [IgnoreColumn]
        public virtual AccountHead AccountHead { get; set; }
    }
}
