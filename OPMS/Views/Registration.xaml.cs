using OPMS.ViewModels;
using OPMS.ViewModels.Base;
using OPMS.Views.Base;
using OPSMBackend.DataEntities;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OPMS.Views
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : BasePage
    {
        private readonly RegistrationViewModel ViewModel;
        public Registration()
        {
            InitializeComponent();

            DataContext = DIContainer.Resolve<RegistrationViewModel>();
            ViewModel = DataContext as RegistrationViewModel;
            viewModelBase = ViewModel;
        }

        private void TestGroup_Checked(object sender, RoutedEventArgs e)
        {
            if(ViewModel != null)
            {
                ViewModel.OnTestGroupSelected((sender as CheckBox).DataContext as TestGroups, true);
            }
        }

        private void TestGroup_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.OnTestGroupSelected((sender as CheckBox).DataContext as TestGroups, false);
            }
        }

        private void TestTitle_Checked(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.OnTestTitleSelected((sender as CheckBox).DataContext as TestTitles, true);
            }
        }

        private void TestTitle_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.OnTestTitleSelected((sender as CheckBox).DataContext as TestTitles, false);
            }
        }

        public override void Init(object sender, NavigationEventArgs eventArgs)
        {
            
        }
    }
}
