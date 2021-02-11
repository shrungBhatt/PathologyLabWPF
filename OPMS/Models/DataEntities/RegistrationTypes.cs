using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class RegistrationTypes
    {
        public RegistrationTypes()
        {
            HdlRegistration = new HashSet<HdlRegistration>();
        }

        public int Id { get; set; }
        public string RegistrationType { get; set; }

        [JsonIgnore]
        public virtual ICollection<HdlRegistration> HdlRegistration { get; set; }
    }
}
