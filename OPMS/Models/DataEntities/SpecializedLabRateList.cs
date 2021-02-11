using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class SpecializedLabRateList : BaseEntity
    {
        [IgnoreColumn]
        public int Id { get; set; }
        [IgnoreColumn]
        public int HdlId { get; set; }

        public string Group { get => TestTitle?.Group?.Name; }

        public string Test { get => TestTitle?.Name; }

        [IgnoreColumn]
        public int TestTitleId { get; set; }
        public double? Charges { get; set; }

        [IgnoreColumn]
        public virtual HdlRegistration Hdl { get; set; }
        [IgnoreColumn]
        public virtual TestTitles TestTitle { get; set; }
    }
}
