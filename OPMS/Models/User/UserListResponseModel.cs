using OPMS.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.User
{
    public class UserListResponseModel
    {
        public List<UsersDto> Users { get; set; }

        public List<RoleTypeDto> RoleTypes { get; set; }
    }
}
