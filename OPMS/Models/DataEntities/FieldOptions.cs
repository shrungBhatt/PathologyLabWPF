using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class FieldOptions : BaseEntity
    {
        public int Id { get; set; }

        public string FieldType
        {
            get => Field?.Name;
        }
        [IgnoreColumn]
        public int FieldId { get; set; }
        public string Name { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }



        [IgnoreColumn]
        public virtual Fields Field { get; set; }
    }
}
