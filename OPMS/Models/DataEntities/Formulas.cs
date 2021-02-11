using Newtonsoft.Json;
using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Formulas : BaseEntity
    {
        [IgnoreColumn]
        public int Id { get; set; }
        [IgnoreColumn]
        public int TestId { get; set; }

        public string Formula { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        [IgnoreColumn]
        public int TitleId { get; set; }
        [IgnoreColumn]
        public int GroupId { get; set; }

    }
}
