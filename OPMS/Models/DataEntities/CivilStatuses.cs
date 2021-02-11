using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class CivilStatuses : BaseEntity
    {
        public CivilStatuses()
        {
            Employees = new HashSet<Employees>();
            Patient = new HashSet<Patient>();
        }

        public int Id { get; set; }
        public string CivilStatus { get; set; }

        [JsonIgnore]
        public virtual ICollection<Employees> Employees { get; set; }

        [JsonIgnore]
        public virtual ICollection<Patient> Patient { get; set; }
    }
}
