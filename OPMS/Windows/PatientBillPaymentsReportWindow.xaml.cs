using OPMS.ViewModels;
using OPMS.ViewModels.Base;
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
using System.Windows.Shapes;

namespace OPMS.Windows
{
    /// <summary>
    /// Interaction logic for SpecializedLabReportWindow.xaml
    /// </summary>
    public partial class PatientBillPaymentsReportWindow : WindowBase
    {

        PatientBillPaymentsReportWindowViewModel _viewModel;

        public PatientBillPaymentsReportWindow()
        {
            InitializeComponent();
            _viewModel = DIContainer.Resolve<PatientBillPaymentsReportWindowViewModel>();
            DataContext = _viewModel;
        }

        public override void Init(object data)
        {

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TestsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SelectedUsers.Clear();
            if (TestsListBox.SelectedItems != null)
            {
                _viewModel.SelectedUsers.AddRange(TestsListBox.SelectedItems.Cast<Users>().ToList());
            }
        }

        private void Occupation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SelectedPaymentTypes.Clear();
            if(OccupationListBox.SelectedItems != null)
            {
                _viewModel.SelectedPaymentTypes.AddRange(OccupationListBox.SelectedItems.Cast<FieldOptions>().ToList());
            }
        }
    }
}
