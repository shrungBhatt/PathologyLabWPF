using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Initials : BaseEntity
    {
        public Initials()
        {
            Patient = new HashSet<Patient>();
        }

        public int Id { get; set; }
        public string Initial { get; set; }

        [JsonIgnore]
        public virtual ICollection<Patient> Patient { get; set; }
    }
}
