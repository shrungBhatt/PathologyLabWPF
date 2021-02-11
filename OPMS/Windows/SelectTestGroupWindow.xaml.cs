using OPMS.Models;
using OPMS.Utility.Attributes;
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
    public partial class SelectTestGroupWindow : WindowBase
    {
        public SelectTestGroupWindow()
        {
            InitializeComponent();
        }

        public override void Init(object data)
        {
            DataContext = data;
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
            var model = (DataContext as SelectWindowDataModel<TestGroupSelectionModel>);
            if(model != null)
            {
                var list = model.ItemList.FindAll(x => x.Select).ToList();
                OnOkClicked_Invoked(list);
                Close();
            }
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            OnOkClicked = null;
            OnCancelClicked = null;
            base.OnClosing(e);
        }

    }

    public class TestGroupSelectionModel
    {
        [IgnoreColumn]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Select { get; set; }
    }
}
