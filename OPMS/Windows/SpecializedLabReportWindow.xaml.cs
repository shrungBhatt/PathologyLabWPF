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
    public partial class SpecializedLabReportWindow : WindowBase
    {

        SpecializedLabReportWindowViewModel _viewModel;

        public SpecializedLabReportWindow()
        {
            InitializeComponent();
            _viewModel = DIContainer.Resolve<SpecializedLabReportWindowViewModel>();
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
            _viewModel.SelectedLabs.Clear();
            if (TestsListBox.SelectedItems != null)
            {
                _viewModel.SelectedLabs.AddRange(TestsListBox.SelectedItems.Cast<HdlRegistration>().ToList());
            }
        }
    }
}
