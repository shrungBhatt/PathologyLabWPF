using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Inventories : BaseEntity
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int ItemType { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
