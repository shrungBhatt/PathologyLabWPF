using Newtonsoft.Json;
using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class TestReagentRelation : BaseEntity
    {
        [IgnoreColumn]
        public int Id { get; set; }

        [IgnoreColumn]
        public int ReagentId { get; set; }

        [IgnoreColumn]
        public int OtherTestId { get; set; }

        [JsonIgnore]
        public string Reagent
        {
            get => ReagentModel?.Name;
        }
        [JsonIgnore]
        public string Test
        {
            get => OtherTest?.Name;
        }
        public int Unit { get; set; }
        public int QtyPerTest { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        [IgnoreColumn]
        public virtual OtherTests OtherTest { get; set; }

        [JsonProperty("reagent")]
        [IgnoreColumn]
        public virtual Reagents ReagentModel { get; set; }
    }
}
