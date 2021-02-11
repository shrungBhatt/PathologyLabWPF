using OPMS.Services;
using OPMS.Utility;
using OPMS.ViewModels;
using OPMS.ViewModels.Base;
using OPMS.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OPMS.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : BasePage, IPassword
    {

        LoginViewModel _viewModel;
        public LoginView()
        {
            InitializeComponent();
            App.Logger.Info("Login view loaded");

            DataContext = DIContainer.Resolve<LoginViewModel>();
            _viewModel = (DataContext as LoginViewModel);
            _viewModel.passwordService = this;

            LoginPasswordBox.Focus();
        }

        public override void Init(object sender, NavigationEventArgs eventArgs)
        {
            _viewModel.Username = (string)eventArgs.ExtraData;
            _viewModel.passwordService = this;
        }

        public string GetPassword()
        {
            return LoginPasswordBox.Password;
        }

        public override void OnEnterPressed()
        {
            _viewModel.passwordService = this;
            _viewModel.LoginUser();
        }

        public override void OnEscPressed()
        {
            
        }
    }
}
