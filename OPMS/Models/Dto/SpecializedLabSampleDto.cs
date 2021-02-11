using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Dto
{
    public class SpecializedLabSampleDto
    {
        public int PatientId { get; set; }
        public List<HdlRegistration> SpecializedLabs { get; set; }
        public List<SpecializedLabSamples> SentSamples { get; set; }
        public List<SpecializedLabSamples> DeletedSamples { get; set; }
        public List<SpecializedLabSamples> NewSamples { get; set; }

    }
}
