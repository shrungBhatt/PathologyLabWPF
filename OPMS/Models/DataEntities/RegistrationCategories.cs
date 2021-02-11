using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class RegistrationCategories
    {
        public RegistrationCategories()
        {
            HdlRegistration = new HashSet<HdlRegistration>();
        }

        public int Id { get; set; }
        public string RegistrationCategory { get; set; }

        [JsonIgnore]
        public virtual ICollection<HdlRegistration> HdlRegistration { get; set; }
    }
}
