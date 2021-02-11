using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class EmployeeRoles
    {
        public EmployeeRoles()
        {
            Employees = new HashSet<Employees>();
        }

        public int Id { get; set; }
        public string EmployeeRole { get; set; }

        [JsonIgnore]
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
