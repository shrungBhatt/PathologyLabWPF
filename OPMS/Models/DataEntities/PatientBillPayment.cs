using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class PatientBillPayment : BaseEntity
    {
        public int Id { get; set; }
        public int BillId { get; set; }

        private int receiptNo; 
        public int ReceiptNo
        {
            get => receiptNo;
            set
            {
                receiptNo = value;
                RaisePropertyChanged(nameof(ReceiptNo));
            }
        }

        private bool? cashMode = false;
        public bool? CashMode
        {
            get => cashMode;
            set
            {
                cashMode = value;
                RaisePropertyChanged(nameof(CashMode));
            }
        }

        private bool? chequeMode = false;
        public bool? ChequeMode
        {
            get => chequeMode;
            set
            {
                chequeMode = value;
                RaisePropertyChanged(nameof(chequeMode));
            }
        }

        private bool? cardMode = false;
        public bool? CardMode
        {
            get => cardMode;
            set
            {
                cardMode = value;
                RaisePropertyChanged(nameof(CardMode));
            }
        }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string ChequeNo { get; set; }
        public string CardNo { get; set; }

        private DateTime? paymentDate;
        public DateTime? PaymentDate
        {
            get => paymentDate;
            set
            {
                paymentDate = value;
                RaisePropertyChanged(nameof(PaymentDate));
            }
        }
        public DateTime? ChequeDate { get; set; }

        private float? paymentAmount;
        public float? PaymentAmount
        {
            get => paymentAmount;
            set
            {
                paymentAmount = value;
                RaisePropertyChanged(nameof(PaymentAmount));
                if (PaymentAmount.HasValue && Bill != null)
                {
                    Balance = Bill.FinalAmount - PaymentAmount;
                }
            }
        }

        private float? balance;
        public float? Balance
        {
            get => balance;
            set
            {
                balance = value;
                RaisePropertyChanged(nameof(Balance));
            }
        }
        public string BillPaidBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int PaymentType { get; set; }

        public virtual PatientBill Bill { get; set; }
    }
}
