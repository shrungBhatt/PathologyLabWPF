using OPMS.Utility.Attributes;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Dto
{
    public class PatientWithRateListTestCharges
    {
        [IgnoreColumn]
        public int PatientId { get; set; }
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public float TotalCharges
        {
            get
            {
                float value = 0;
                if(TestTitles != null)
                {
                    foreach(var test in TestTitles)
                    {
                        value += test.Charges;
                    }
                }
                return value;
            }
        }

        [IgnoreColumn]
        public List<TestTitles> TestTitles { get; set; } = new List<TestTitles>();
    }
}
