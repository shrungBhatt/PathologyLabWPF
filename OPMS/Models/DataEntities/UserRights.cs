using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class UserRights
    {
        public int Id { get; set; }
        public int RoleTypeId { get; set; }
        public bool Registration { get; set; }
        public bool TestResult { get; set; }
        public bool Finance { get; set; }
        public bool Maintenance { get; set; }
        public bool Report { get; set; }
        public bool UserManagement { get; set; }
        public bool Windows { get; set; }
        public bool Tools { get; set; }
        public bool About { get; set; }
        public bool Help { get; set; }
        public bool AddOption { get; set; }
        public bool EditOption { get; set; }
        public bool DeleteOption { get; set; }
        public bool Navigation { get; set; }
        public bool IncludeDigitalSignature { get; set; }

        [JsonIgnore]
        public virtual RoleTypes RoleType { get; set; }
    }
}
