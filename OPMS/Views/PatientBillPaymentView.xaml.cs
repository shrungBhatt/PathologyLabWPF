using OPMS.ViewModels;
using OPMS.ViewModels.Base;
using OPMS.Views.Base;
using OPSMBackend.DataEntities;
using System.Windows.Navigation;

namespace OPMS.Views
{
    /// <summary>
    /// Interaction logic for PatientBillPaymentView.xaml
    /// </summary>
    public partial class PatientBillPaymentView : BasePage
    {

        PatientBillPaymentViewModel _viewModel;

        public PatientBillPaymentView()
        {
            InitializeComponent();
            DataContext = DIContainer.Resolve<PatientBillPaymentViewModel>();
            _viewModel = (DataContext as PatientBillPaymentViewModel);
        }

        public override void OnEscPressed()
        {

        }

        public override void OnEnterPressed()
        {

        }

        public override void Init(object sender, NavigationEventArgs eventArgs)
        {
            var patient = (Patient)eventArgs.ExtraData;
            _viewModel.NavigatedFromPatientBill(patient);
        }
    }
}
