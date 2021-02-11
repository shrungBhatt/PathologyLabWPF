using OPMS.Models;
using OPMS.Models.Dto;
using OPMS.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Services.User
{
    public interface IUserService
    {
        Task<LoginModel> LoginUser(LoginModel loginModel);
        Task<UserListResponseModel> GetUsersList();
        Task<UsersDto> AddNewUser(UsersDto usersDto);
        Task<UsersDto> UpdateUser(UsersDto usersDto);
        Task<UsersDto> DeleteUser(UsersDto responseModel);

    }
}
