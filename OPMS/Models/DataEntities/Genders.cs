using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Genders : BaseEntity
    {
        public Genders()
        {
            Employees = new HashSet<Employees>();
            Patient = new HashSet<Patient>();
        }

        public int Id { get; set; }
        public string Gender { get; set; }

        [JsonIgnore]
        public virtual ICollection<Employees> Employees { get; set; }

        [JsonIgnore]
        public virtual ICollection<Patient> Patient { get; set; }
    }
}
