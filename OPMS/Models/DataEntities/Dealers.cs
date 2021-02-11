using Newtonsoft.Json;
using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Dealers : BaseEntity
    {
        public Dealers()
        {
            ReagentBillEntries = new HashSet<ReagentBillEntries>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        [IgnoreColumn]
        [JsonIgnore]
        public virtual ICollection<ReagentBillEntries> ReagentBillEntries { get; set; }
    }
}
