using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Dto
{
    public class PatientRevenueModelDto
    {
        public int BillId { get; set; }
        public int BillNo { get; set; }
        public string PatientName { get; set; }
        public string PatientCode { get; set; }
        public int PatientId { get; set; }
        public int HdlId { get; set; }
        public float TotalAmount { get; set; }
        public float AmountPaid { get; set; }
        public float Balance { get; set; }
        public bool IsPaid { get; set; }
        public string Occupation { get; set; }


    }
}
