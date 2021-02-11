using OPMS.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Request
{
    public class PatientWithRateListChargesRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int HdlId { get; set; }
        public List<PatientWithRateListTestCharges> Patients { get; set; }
    }
}
