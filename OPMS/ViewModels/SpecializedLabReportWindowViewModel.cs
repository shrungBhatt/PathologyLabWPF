using GalaSoft.MvvmLight.Command;
using OPMS.Models.Dto;
using OPMS.Models.Report;
using OPMS.Repository;
using OPMS.Services.Patient;
using OPMS.Services.WindowManager;
using OPMS.Utility;
using OPMS.ViewModels.Base;
using OPMS.Windows;
using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPMS.ViewModels
{
    public class SpecializedLabReportWindowViewModel : ViewModelBase
    {
        #region Properties
        DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        public DateTime FromDate
        {
            get => fromDate;
            set
            {
                fromDate = value;
                RaisePropertyChanged(nameof(FromDate));
                if (FromDate != null && ToDate != null)
                {
                    GetModelItems();
                }
            }
        }

        DateTime toDate = DateTime.Now;
        public DateTime ToDate
        {
            get => toDate;
            set
            {
                toDate = value;
                RaisePropertyChanged(nameof(ToDate));
                if (FromDate != null && ToDate != null)
                {
                    GetModelItems();
                }
            }
        }

        bool showPatientCharges;
        public bool ShowPatientCharges
        {
            get => showPatientCharges;
            set
            {
                showPatientCharges = value;
                RaisePropertyChanged(nameof(ShowPatientCharges));
            }
        }

        List<HdlRegistration> labs;
        public List<HdlRegistration> Labs
        {
            get => labs;
            set
            {
                labs = value;
                RaisePropertyChanged(nameof(Labs));
            }
        }

        public List<HdlRegistration> SelectedLabs = new List<HdlRegistration>();

        private bool isAllLabSelected = true;
        public bool IsAllLabsSelected
        {
            get => isAllLabSelected;
            set
            {
                isAllLabSelected = value;
                RaisePropertyChanged(nameof(IsAllLabsSelected));
                if (IsAllLabsSelected)
                {
                    SelectedLabs.Clear();
                    SelectedLabs.AddRange(Labs);
                }
            }
        }

        bool isSingleLabSelected;
        public bool IsSingleLabSelected
        {
            get => isSingleLabSelected;
            set
            {
                isSingleLabSelected = value;
                RaisePropertyChanged(nameof(IsSingleLabSelected));
            }
        }
        #endregion

        private readonly IWindowManager _windowManager;
        private readonly IPatientService patientService;
        readonly UserRepository userRepository;
        List<SpecializedLabModelDto> _labSamples;

        public SpecializedLabReportWindowViewModel(IWindowManager windowManager,
                                     IPatientService patientService,
                                     UserRepository userRepository)
        {
            this._windowManager = windowManager;
            this.patientService = patientService;
            this.userRepository = userRepository;
            CreateCommands();
            GetModelItems();
        }

        public ICommand ShowReportCommand { get; set; }

        private void CreateCommands()
        {
            ShowReportCommand = new RelayCommand(ShowReportCommand_Invoked);
        }

        private void ShowReportCommand_Invoked()
        {
            var samples = new List<SpecializedLabModelDto>();
            if (IsAllLabsSelected)
            {
                samples = _labSamples;
            }else if (IsSingleLabSelected)
            {
                foreach(var lab in SelectedLabs)
                {
                    samples.AddRange(_labSamples.FindAll(x => x.OutGoingLabId == lab.Id)?.ToList());
                }
            }

            var reportModel = new SpecializedLabReportModel
            {
                LabSamples = samples,
                Options = new List<SpecializedLabOptionsModel> { new SpecializedLabOptionsModel { ShowPatientCharges = ShowPatientCharges, FromDate = this.FromDate.ToString("dd/MM/yyyy"), ToDate = this.ToDate.ToString("dd/MM/yyyy") } },
                ReportType = BaseReportModel.REPORT_TYPE_SPECIALIZED_LAB_REPORT
            };

            if (ShowPatientCharges)
            {
                reportModel.ReportResourcePath = ReportResourceFilePath.SPEC_LAB_PAYMENT_REPORT;
            }
            else
            {
                reportModel.ReportResourcePath = ReportResourceFilePath.SPEC_LAB_PAYMENT_REPORT_WO_PATIENT;
            }

            _windowManager.Show<ReportWindow>(reportModel);
        }

        async void GetModelItems()
        {

            if(FromDate == null || ToDate == null)
            {
                MessageBox.Show("Please enter valid date range");
                return;
            }

            IsBusy = Visibility.Visible;
            var response = await patientService.GetSpecializedLabReport(new SpecializedLabReportResponseModel { FromDate = this.FromDate, ToDate = this.ToDate});
            IsBusy = Visibility.Hidden;
            if (response != null)
            {
                Labs = response.Labs;
                _labSamples = response.SpecializedLabsSamples;
            }
        }

    }
}
