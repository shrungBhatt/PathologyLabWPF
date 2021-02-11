using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class HdlBill : BaseEntity
    {
        public HdlBill()
        {
            HdlBillPayment = new HashSet<HdlBillPayment>();
        }

        public int Id { get; set; }

        private int? billNo;
        public int? BillNo
        {
            get => billNo;
            set
            {
                billNo = value;
                RaisePropertyChanged(nameof(BillNo));
            }
        }
        public int HdlId { get; set; }

        DateTime fromDate;
        public DateTime FromDate
        {
            get => fromDate;
            set
            {
                fromDate = value;
                RaisePropertyChanged(nameof(FromDate));
            }
        }

        DateTime toDate;
        public DateTime ToDate
        {
            get => toDate;
            set
            {
                toDate = value;
                RaisePropertyChanged(nameof(ToDate));
            }
        }

        DateTime billDate;
        public DateTime BillDate
        {
            get => billDate;
            set
            {
                billDate = value;
                RaisePropertyChanged(nameof(BillDate));
            }
        }

        float totalCharges;
        public float TotalCharges
        {
            get => totalCharges;
            set
            {
                totalCharges = value;
                RaisePropertyChanged(nameof(TotalCharges));
                RaisePropertyChanged(nameof(FinalAmount));
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

        private float? discount = 0;
        public float? Discount
        {
            get => discount;
            set
            {
                discount = value;
                RaisePropertyChanged(nameof(Discount));
                if (discount.HasValue)
                {
                    RaisePropertyChanged(nameof(FinalAmount));
                }
            }
        }

        private float? gst = 0;
        public float? Gst {
            get => gst;
            set
            {
                gst = value;
                RaisePropertyChanged(nameof(Gst));
                if (gst.HasValue)
                {
                    RaisePropertyChanged(nameof(FinalAmount));
                }
            }
        }
        public float? AmountPaid { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public float FinalAmount
        {
            get => (float)TotalCharges - DiscountAmount + GstAmount;
        }

        public float GstAmount
        {
            get => ConvertPercentToValue(Gst.HasValue ? (float)Gst : 0);
        }

        public float DiscountAmount
        {
            get => ConvertPercentToValue(Discount.HasValue ? (float)Discount : 0);
        }

        float ConvertPercentToValue(float percent)
        {
            return ((float)TotalCharges * percent) / 100;
        }

        public virtual HdlRegistration Hdl { get; set; }
        public virtual ICollection<HdlBillPayment> HdlBillPayment { get; set; }
    }
}
