using Newtonsoft.Json;
using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class SpecializedLabSamples : BaseEntity
    {
        [IgnoreColumn]
        [JsonIgnore]
        public int Id { get; set; }
        [IgnoreColumn]
        public int LabId { get; set; }
        [IgnoreColumn]
        public int PatientId { get; set; }
        [IgnoreColumn]
        public int TestTitleId { get; set; }
        [IgnoreColumn]
        public string ModifiedBy { get; set; }
        [IgnoreColumn]
        public DateTime? ModifiedDate { get; set; }

        public string LabName { get; set; }
        public string TestName { get; set; }

        [IgnoreColumn]
        [JsonIgnore]
        public virtual HdlRegistration Lab { get; set; }
        [IgnoreColumn]
        [JsonIgnore]
        public virtual Patient Patient { get; set; }
        [IgnoreColumn]
        [JsonIgnore]
        public virtual TestTitles TestTitle { get; set; }
    }
}
