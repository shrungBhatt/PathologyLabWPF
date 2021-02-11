using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Models.Dto
{
    public class UsersDto : ModelBase
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

        public override object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
