using OPMS.ViewModels;
using OPMS.ViewModels.Base;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class SelectToAddTestTitlesToGroup : WindowBase
    {

        SelectToAddTestTitlesToGroupViewModel viewModel;
        public SelectToAddTestTitlesToGroup()
        {
            InitializeComponent();
            viewModel = DIContainer.Resolve<SelectToAddTestTitlesToGroupViewModel>();
            DataContext = viewModel;
            Loaded += SelectToAddTestTitlesToGroup_Loaded;
        }

        private void SelectToAddTestTitlesToGroup_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel?.PageIsLoaded(sender, e);
        }

        public override void Init(object data)
        {
            viewModel.SelectedGroupId = (int)data;
        }

        public event EventHandler OnOkClicked;
        public event EventHandler OnCancelClicked;

        public void OnCancelClicked_Invoked()
        {
            OnCancelClicked?.Invoke(this, new EventArgs());
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            OnOkClicked = null;
            OnCancelClicked = null;
            base.OnClosing(e);
        }

    }


}
