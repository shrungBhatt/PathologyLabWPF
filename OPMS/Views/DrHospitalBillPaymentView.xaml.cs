using OPMS.ViewModels;
using OPMS.ViewModels.Base;
using OPMS.Views.Base;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OPMS.Views
{
    /// <summary>
    /// Interaction logic for DrHospitalBillPaymentView.xaml
    /// </summary>
    public partial class DrHospitalBillPaymentView : BasePage
    {

        DrHospitalBillPaymentViewModel _viewModel;

        public DrHospitalBillPaymentView()
        {
            InitializeComponent();
            _viewModel = DIContainer.Resolve<DrHospitalBillPaymentViewModel>();
            DataContext = _viewModel;
            
        }

        public override void OnEscPressed()
        {

        }

        public override void OnEnterPressed()
        {

        }

        public override void Init(object sender, NavigationEventArgs eventArgs)
        {
            var patient = (HdlRegistration)eventArgs.ExtraData;
            _viewModel.OnHdlSelected(patient);
        }
    }
}
