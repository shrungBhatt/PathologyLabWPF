using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Occupations : BaseEntity
    {
        public Occupations()
        {
            Patient = new HashSet<Patient>();
        }

        public int Id { get; set; }
        public string Occupation { get; set; }

        [JsonIgnore]
        public virtual ICollection<Patient> Patient { get; set; }
    }
}
