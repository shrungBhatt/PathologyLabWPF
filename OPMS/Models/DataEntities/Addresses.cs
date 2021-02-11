using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Addresses : BaseEntity
    {
        public Addresses()
        {
            Patient = new HashSet<Patient>();
        }

        public int Id { get; set; }
        public string Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<Patient> Patient { get; set; }
    }
}
