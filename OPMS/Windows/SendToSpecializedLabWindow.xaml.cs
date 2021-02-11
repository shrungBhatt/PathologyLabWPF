using OPMS.Models.Patient;
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
    /// Interaction logic for SendToSpecializedLabWindow.xaml
    /// </summary>
    public partial class SendToSpecializedLabWindow : WindowBase
    {
        SendToSpecializedLabWindowViewModel _viewModel;
        public SendToSpecializedLabWindow()
        {
            InitializeComponent();
            DataContext = DIContainer.Resolve<SendToSpecializedLabWindowViewModel>();
            _viewModel = DataContext as SendToSpecializedLabWindowViewModel;
        }

        public override void Init(object data)
        {
            if(data is PatientDto)
            {
                _viewModel.OnPatientSelected(data as PatientDto);
            }
        }

        private void AddToTable_Click(object sender, RoutedEventArgs e)
        {
            if(TestsListBox.SelectedItems != null)
            {
                _viewModel.OnItemsSelected(TestsListBox.SelectedItems.Cast<TestTitles>().ToList());
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if(e.Key == Key.Delete || e.Key == Key.Back)
            {
                _viewModel.OnItemDeleted();
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SendSamples();
        }
    }
}
