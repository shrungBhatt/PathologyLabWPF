using OPMS.Models;
using OPMS.Models.Dto;
using OPMS.Models.User;
using OPMS.Services.RequestProvider;
using OPMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Services.User
{
    public class UserService : IUserService
    {

        private readonly IRequestProvider requestProvider;


        public UserService(IRequestProvider requestProvider)
        {
            this.requestProvider = requestProvider;
        }

        public async Task<UsersDto> AddNewUser(UsersDto usersDto)
        {
            var url = URLBuilder.GetURL(Controllers.USER, EndPoint.USER_ADD);

            return await requestProvider.PostAsync(url,usersDto);
        }

        public async Task<UsersDto> DeleteUser(UsersDto userDto)
        {
            var url = URLBuilder.GetURL(Controllers.USER, EndPoint.USER_DELETE);

            return await requestProvider.DeleteAsync(url, userDto, new Dictionary<string, string> { ["id"] = userDto.Id.ToString()});
        }

        public async Task<UserListResponseModel> GetUsersList()
        {
            var url = URLBuilder.GetURL(Controllers.USER, EndPoint.USER_LIST);

            return await requestProvider.GetAsync<UserListResponseModel>(url);
        }

        public async Task<LoginModel> LoginUser(LoginModel loginModel)
        {
            var url = URLBuilder.GetURL(Controllers.USER, EndPoint.USER_LOGIN);

            return await requestProvider.PostAsync(url, loginModel);
        }

        public async Task<UsersDto> UpdateUser(UsersDto usersDto)
        {
            var url = URLBuilder.GetURL(Controllers.USER, EndPoint.USER_UPDATE);

            return await requestProvider.PutAsync<UsersDto>(url, usersDto);
        }
    }
}
