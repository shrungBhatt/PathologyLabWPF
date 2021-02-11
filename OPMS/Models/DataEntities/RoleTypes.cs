using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class RoleTypes : BaseEntity
    {
        public RoleTypes()
        {
            UserRights = new HashSet<UserRights>();
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Role { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRights> UserRights { get; set; }

        [JsonIgnore]
        public virtual ICollection<Users> Users { get; set; }
    }
}
