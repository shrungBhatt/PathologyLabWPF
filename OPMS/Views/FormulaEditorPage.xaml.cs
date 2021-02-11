using OPMS.ViewModels;
using OPMS.ViewModels.Base;
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
    /// Interaction logic for FormulaEditorPage.xaml
    /// </summary>
    public partial class FormulaEditorPage : Page
    {
        public FormulaEditorPage()
        {
            InitializeComponent();
            DataContext = DIContainer.Resolve<FormulaEditorViewModel>();
        }

        private void RichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(UnHandledKeys().Contains(e.Key))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;

            }
        }

        private List<Key> UnHandledKeys()
        {
            return new List<Key>
            {
                Key.Back,
                Key.Divide,
                Key.Multiply,
                Key.Subtract,
                Key.Add,
                Key.OemCloseBrackets,
                Key.OemOpenBrackets,
                Key.NumPad0,
                Key.NumPad1,
                Key.NumPad2,
                Key.NumPad3,
                Key.NumPad4,
                Key.NumPad5,
                Key.NumPad6,
                Key.NumPad7,
                Key.NumPad8,
                Key.NumPad9,
                Key.Decimal,
                Key.LeftShift,
                Key.D0,
                Key.D9
            };
        }
    } 
}
