using OPMS.Models.Test;
using OPMS.ViewModels;
using OPMS.ViewModels.Base;
using OPMS.Views.Base;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OPMS.Views
{
    /// <summary>
    /// Interaction logic for TestTitleView.xaml
    /// </summary>
    public partial class TestTitleView : BasePage
    {
        TestTitleViewModel _viewModel;

        public TestTitleView()
        {
            InitializeComponent();

            DataContext = DIContainer.Resolve<TestTitleViewModel>();
            _viewModel = DataContext as TestTitleViewModel;
        }

        public override void OnEscPressed()
        {

        }

        public override void OnEnterPressed()
        {

        }

        public override void Init(object sender, NavigationEventArgs eventArgs)
        {
            var model = (TestGroupDto)eventArgs.ExtraData;
            if(model != null)
            {
                _viewModel.OnGroupSelected(model);
            }
        }
    }
}
