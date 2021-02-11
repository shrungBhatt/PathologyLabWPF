using Newtonsoft.Json;
using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Reagents : BaseEntity
    {
        public Reagents()
        {
            ReagentBillEntries = new HashSet<ReagentBillEntries>();
            TestReagentRelation = new HashSet<TestReagentRelation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitInStock { get; set; }
        public int ReorderLevel { get; set; }
        public int UnitPrice { get; set; }
        public int PackingSize { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string AdditionalNotes { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        [IgnoreColumn]
        [JsonIgnore]
        public virtual ICollection<ReagentBillEntries> ReagentBillEntries { get; set; }

        [IgnoreColumn]
        [JsonIgnore]
        public virtual ICollection<TestReagentRelation> TestReagentRelation { get; set; }
    }
}
