using OPMS.Models.Patient;
using OPMS.ViewModels;
using OPMS.ViewModels.Base;
using OPMS.Views.Base;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using TestResults = OPSMBackend.DataEntities.TestResultsView;

namespace OPMS.Views
{
    /// <summary>
    /// Interaction logic for TestResultsView.xaml
    /// </summary>
    public partial class TestResultsView : BasePage
    {

        TestResultsViewModel _viewModel;
        public TestResultsView()
        {
            InitializeComponent();

            _viewModel = DIContainer.Resolve<TestResultsViewModel>();
            DataContext = _viewModel;
            
        }

        private void CustomDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var value = "";
                if (e.EditingElement is TextBox)
                {
                    value = (e.EditingElement as TextBox).Text;
                }
                (DataContext as TestResultsViewModel).OnTestResultEdited(e.Row.DataContext as TestResults, value);
            }
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        public override void OnEnterPressed()
        {
            
        }

        public override void OnEscPressed()
        {
            
        }

        public override void Init(object sender, NavigationEventArgs eventArgs)
        {
            var model = eventArgs.ExtraData as PatientDto;
            if(model != null)
            {
                _viewModel.OnPatientSelected(model.Patient);
            }
        }

        private void MergeDifferentGroupCB_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            MergeSingleGroupCB.IsChecked = true;
        }

        private void MergeDifferentGroupCB_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            MergeSingleGroupCB.IsChecked = false;
        }

        private void MergeSingleGroupCB_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MergeDifferentGroupCB.IsChecked.Value)
            {
                MergeSingleGroupCB.IsChecked = true;
            }
        }
    }
}
