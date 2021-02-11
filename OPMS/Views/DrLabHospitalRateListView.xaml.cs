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
    /// Interaction logic for DrLabHospitalRateListView.xaml
    /// </summary>
    public partial class DrLabHospitalRateListView : Page, IRateListViewInteraction
    {
        public DrLabHospitalRateListView()
        {
            InitializeComponent();
            DataContext = DIContainer.Resolve<DrLabHospitalRateListViewModel>();
            (DataContext as DrLabHospitalRateListViewModel).RateListViewInteraction = this;
        }

        public void RefreshGrid()
        {
            RateListsGrid.Items?.Refresh();
        }
    }

    public interface IRateListViewInteraction
    {
        void RefreshGrid();
    }
}
