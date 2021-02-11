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
    public partial class PatientRevenueReportWindow : WindowBase
    {

        PatientRevenueReportWindowViewModel _viewModel;

        public PatientRevenueReportWindow()
        {
            InitializeComponent();
            _viewModel = DIContainer.Resolve<PatientRevenueReportWindowViewModel>();
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
            _viewModel.SelectedHdls.Clear();
            if (TestsListBox.SelectedItems != null)
            {
                _viewModel.SelectedHdls.AddRange(TestsListBox.SelectedItems.Cast<HdlRegistration>().ToList());
            }
        }

        private void Occupation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SelectedOccupations.Clear();
            if(OccupationListBox.SelectedItems != null)
            {
                _viewModel.SelectedOccupations.AddRange(OccupationListBox.SelectedItems.Cast<FieldOptions>().ToList());
            }
        }
    }
}
