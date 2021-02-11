using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Users : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmailId { get; set; }
        public int? RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Degree { get; set; }
        public string Specialization { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [JsonIgnore]
        public virtual RoleTypes Role { get; set; }
    }
}
