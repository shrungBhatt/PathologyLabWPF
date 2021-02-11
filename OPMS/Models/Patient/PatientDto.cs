using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientDataEntity = OPSMBackend.DataEntities.Patient;

namespace OPMS.Models.Patient
{
    public class PatientDto : ModelBase
    {

        public PatientDataEntity Patient { get; set; } = new PatientDataEntity();
        public List<TestTitles> SelectedTestTitles { get; set; } = new List<TestTitles>();

        public override object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
