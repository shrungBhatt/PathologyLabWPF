using OPMS.Models.Test;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OPMS.Views
{
    /// <summary>
    /// Interaction logic for OtherTestView.xaml
    /// </summary>
    public partial class OtherTestView : BasePage
    {
        OtherTestViewModel _viewModel;

        public OtherTestView()
        {
            InitializeComponent();

            DataContext = DIContainer.Resolve<OtherTestViewModel>();
            _viewModel = DataContext as OtherTestViewModel;
        }

        public override void OnEscPressed()
        {

        }

        public override void OnEnterPressed()
        {

        }

        public override void Init(object sender, NavigationEventArgs eventArgs)
        {
            var model = (TestTitleDto)eventArgs.ExtraData;
            _viewModel.OnTestTitleSelected(model);
        }
    }
}
