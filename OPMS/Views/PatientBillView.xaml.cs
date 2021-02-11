using OPMS.Models.Patient;
using OPMS.ViewModels;
using OPMS.ViewModels.Base;
using OPMS.Views.Base;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OPMS.Views
{
    /// <summary>
    /// Interaction logic for PatientBillView.xaml
    /// </summary>
    public partial class PatientBillView : BasePage
    {
        PatientBillViewModel _viewModel;

        public PatientBillView()
        {
            InitializeComponent();
            _viewModel = DIContainer.Resolve<PatientBillViewModel>();
            DataContext = _viewModel;
        }

        public override void Init(object sender, NavigationEventArgs eventArgs)
        {
            var model = (PatientDto)eventArgs.ExtraData;
            if(model != null)
            {
                _viewModel.SaveCachedPatient(model.Patient);
            }
        }

        public override void OnEnterPressed()
        {
        }

        public override void OnEscPressed()
        {
        }
    }
}
