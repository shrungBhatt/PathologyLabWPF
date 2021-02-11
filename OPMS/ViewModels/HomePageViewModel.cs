using OPMS.Repository;
using OPMS.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {

        readonly UserRepository userRepository;


        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                RaisePropertyChanged(nameof(Username));
            }
        }

        public HomePageViewModel(UserRepository userRepository)
        {
            this.userRepository = userRepository;
            SetupUsername();
        }

        private void SetupUsername()
        {
            Username = userRepository.UserName;
        }
    }
}
