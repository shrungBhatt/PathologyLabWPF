using Newtonsoft.Json;
using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class PatientBill : BaseEntity
    {
        public PatientBill()
        {
        }
        public int Id { get; set; }

        private float totalCharges;
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
        public int PatientId { get; set; }

        private int? discount = 0;
        public int? Discount
        {
            get => discount;
            set
            {
                discount = value;
                RaisePropertyChanged(nameof(Discount));
                if (discount.HasValue)
                {
                    DiscountedAmount = ConvertPercentToValue((float)discount);
                    RaisePropertyChanged(nameof(FinalAmount));
                }
            }
        }
        public float? DiscountedAmount { get; set; }

        private float? gst = 0;
        public float? Gst
        {
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

        private float amountPaid = 0;
        public float AmountPaid
        {
            get => amountPaid;
            set
            {
                amountPaid = value;
                RaisePropertyChanged(nameof(AmountPaid));
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
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

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

        private DateTime? billDate;
        public DateTime? BillDate
        {
            get => billDate;
            set
            {
                billDate = value;
                RaisePropertyChanged(nameof(BillDate));
            }
        }

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

        public virtual Patient Patient { get; set; }

        [JsonIgnore]
        public virtual ICollection<PatientBillPayment> PatientBillPayment { get; set; }

        [JsonIgnore]
        [IgnoreColumn]
        public bool IsAmountPaid { get => AmountPaid >= FinalAmount; }
    }
}
