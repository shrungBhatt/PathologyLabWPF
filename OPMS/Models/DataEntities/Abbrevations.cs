using Newtonsoft.Json;
using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Abbrevations : BaseEntity
    {
        public int Id { get; set; }
        [IgnoreColumn]
        public int OtherTestId { get; set; }
        public string TestName
        {
            get => OtherTest?.Name;
        }
        public long SerialNo { get; set; }
        public string Abbreavation { get; set; }
        public string Interpretation { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        [IgnoreColumn]
        public virtual OtherTests OtherTest { get; set; }
    }
}
