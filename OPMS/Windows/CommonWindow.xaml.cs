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
    public partial class CommonWindow : WindowBase
    {
        public CommonWindow()
        {
            InitializeComponent();
        }

        public override void Init(object data)
        {
            var uri = data as Uri;
            ViewFrame.Navigate(uri);
        }

        public event EventHandler OnOkClicked;
        public event EventHandler OnCancelClicked;

        public void OnOkClicked_Invoked(object data)
        {
            OnOkClicked?.Invoke(this, new SelectWindowEventArgs(data));
        }

        public void OnCancelClicked_Invoked()
        {
            OnCancelClicked?.Invoke(this, new EventArgs());
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            OnCancelClicked?.Invoke(this, new EventArgs());
            OnOkClicked = null;
            OnCancelClicked = null;
            base.OnClosing(e);
        }

    }


}
