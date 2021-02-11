using OPMS.Controls;
using OPMS.Controls.MoreOptionsMenu;
using OPMS.Models.Dto;
using OPMS.Services.RequestProvider;
using OPMS.Services.User;
using OPMS.Utility;
using OPMS.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPMS.ViewModels
{
    public class UserManagementViewModel : GenericViewModel<UsersDto>
    {

        private IUserService userService;

        public UserManagementViewModel(IUserService userService)
        {
            this.userService = userService;

            GetModelItems();
        }

        private List<RoleTypeDto> roleTypeDtos;
        public List<RoleTypeDto> RoleTypeDtos
        {
            get => roleTypeDtos;
            set
            {
                roleTypeDtos = value;
                RaisePropertyChanged(nameof(RoleTypeDtos));
            }
        }

        private RoleTypeDto roleSelectedItem;
        public RoleTypeDto RoleSelectedItem
        {
            get => roleSelectedItem;
            set
            {
                roleSelectedItem = value;
                RaisePropertyChanged(nameof(RoleSelectedItem));
                if (SelectedModel != null && RoleSelectedItem != null)
                {
                    SelectedModel.RoleId = roleSelectedItem?.Id;
                }

            }
        }

        #region API Calls

        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await userService.GetUsersList();
            IsBusy = Visibility.Hidden;

            if (response != null)
            {
                ModelDtos = response.Users;
                RoleTypeDtos = response.RoleTypes;

                if (ModelDtos != null && ModelDtos.Count > 0)
                {
                    SelectedModel = ModelDtos.First();
                }
            }
        }

        protected async override void AddNewModelItem(UsersDto model)
        {
            IsBusy = Visibility.Visible;
            var response = await userService.AddNewUser(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new user", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(UsersDto model)
        {
            IsBusy = Visibility.Visible;
            var response = await userService.UpdateUser(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the user", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(UsersDto model)
        {
            IsBusy = Visibility.Visible;
            var response = await userService.DeleteUser(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in deleting the user", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(UsersDto selectedModel)
        {
            if(RoleTypeDtos != null)
            {
                RoleSelectedItem = RoleTypeDtos?.Find(x => x.Id == selectedModel?.RoleId);
            }
        }

        protected override void SetSearch(string searchTitle)
        {
            //Do nothing
        }

        protected override void OnGroupIdSelected(int id)
        {
            //Do nothing
        }

        protected override void OnTestIdSelected(int id)
        {
            //Do nothing
        }
        #endregion
    }
}
