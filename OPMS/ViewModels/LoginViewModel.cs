using GalaSoft.MvvmLight.Command;
using OPMS.Models.User;
using OPMS.Repository;
using OPMS.Services;
using OPMS.Services.RequestProvider;
using OPMS.Services.User;
using OPMS.Services.WindowManager;
using OPMS.ViewModels.Base;
using System;
using System.Windows;
using System.Windows.Input;

namespace OPMS.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        private readonly IUserService userService;
        public IPassword passwordService { private get; set; }

        private readonly IWindowManager windowManager;
        private readonly UserRepository userRepository;

        public LoginViewModel(IUserService userService, IWindowManager windowManager,
            UserRepository userRepository)
        {
            this.userService = userService;
            this.windowManager = windowManager;
            this.userRepository = userRepository;
            CreateCommands();
        }

        private string _username = "admin";
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                RaisePropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginUserCommand { get; set; }

        private void CreateCommands()
        {
            LoginUserCommand = new RelayCommand(LoginUser);
        }

        public async void LoginUser()
        {
            IsBusy = Visibility.Visible;
            var loginModel = new LoginModel { Username = this.Username, Password = passwordService.GetPassword() };
            var response = await userService.LoginUser(loginModel);
            IsBusy = Visibility.Collapsed;
            if (response != null && response.Acknowledgement != null && response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
            {
                App.Logger.Info("Login success");
                MessageBox.Show("Login in success", "Success", MessageBoxButton.OK);
                userRepository.CurrentUser = new OPSMBackend.DataEntities.Users { UserName = this.Username };
                userRepository.LoginTime = DateTime.UtcNow;
                windowManager.ShowNewAndCloseMainWindow<OPSMWindow>(null);
            }
            else
            {
                App.Logger.Warn("Login failed");
            }
        }

    }
}
