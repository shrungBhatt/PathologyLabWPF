using GalaSoft.MvvmLight.Command;
using OPMS.Models.Dto;
using OPMS.Models.Report;
using OPMS.Repository;
using OPMS.Services.Finance;
using OPMS.Services.WindowManager;
using OPMS.Utility;
using OPMS.ViewModels.Base;
using OPMS.Windows;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPMS.ViewModels
{
    public class PatientRevenueReportWindowViewModel : ViewModelBase
    {

        #region Fields


        #endregion

        #region Properties
        public List<HdlRegistration> SelectedHdls = new List<HdlRegistration>();
        public List<FieldOptions> SelectedOccupations = new List<FieldOptions>();

        bool showAllReferringHdls = true;
        public bool ShowAllReferringHdls
        {
            get => showAllReferringHdls;
            set
            {
                showAllReferringHdls = value;
                RaisePropertyChanged(nameof(ShowAllReferringHdls));
            }
        }

        bool selectAllReferringHdls;
        public bool SelectAllReferringHdls
        {
            get => selectAllReferringHdls;
            set
            {
                selectAllReferringHdls = value;
                RaisePropertyChanged(nameof(SelectAllReferringHdls));
            }
        }

        bool showAllOccupations = true;
        public bool ShowAllOccupations
        {
            get => showAllOccupations;
            set
            {
                showAllOccupations = value;
                RaisePropertyChanged(nameof(ShowAllOccupations));
            }
        }

        bool selectAllOccupations;
        public bool SelectAllOccupations
        {
            get => selectAllOccupations;
            set
            {
                selectAllOccupations = value;
                RaisePropertyChanged(nameof(SelectAllOccupations));
            }
        }

        bool showAllStatuses = true;
        public bool ShowAllStatuses
        {
            get => showAllStatuses;
            set
            {
                showAllStatuses = value;
                RaisePropertyChanged(nameof(ShowAllStatuses));
            }
        }

        bool selectAllStatus;
        public bool SelectAllStatus
        {
            get => selectAllStatus;
            set
            {
                selectAllStatus = value;
                RaisePropertyChanged(nameof(SelectAllStatus));
            }
        }

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

        List<FieldOptions> occupations;
        public List<FieldOptions> Occupations
        {
            get => occupations;
            set
            {
                occupations = value;
                RaisePropertyChanged(nameof(Occupations));
            }
        }

        List<Status> statuses;
        public List<Status> Statuses
        {
            get => statuses;
            set
            {
                statuses = value;
                RaisePropertyChanged(nameof(Statuses));
            }
        }

        Status selectedStatus;
        public Status SelectedStatus
        {
            get => selectedStatus;
            set
            {
                selectedStatus = value;
                RaisePropertyChanged(nameof(SelectedStatus));
            }
        }
        #endregion

        private readonly IWindowManager _windowManager;
        private readonly IFinanceService financeService;
        readonly UserRepository userRepository;
        List<PatientRevenueModelDto> _revenueItems;

        public PatientRevenueReportWindowViewModel(IWindowManager windowManager,
                                     IFinanceService financeService,
                                     UserRepository userRepository)
        {
            this._windowManager = windowManager;
            this.financeService = financeService;
            this.userRepository = userRepository;
            Statuses = new List<Status> { new Status(1, "PAID"), new Status(2, "UNPAID") };
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
            var items = new List<PatientRevenueModelDto>();

            if (SelectAllReferringHdls)
            {
                foreach(var hdl in SelectedHdls)
                {
                    items.AddRange(_revenueItems.FindAll(x => x.HdlId == hdl.Id));
                }
            }
            else
            {
                items.AddRange(_revenueItems);
            }

            var items2 = new List<PatientRevenueModelDto>();
            if (SelectAllOccupations)
            {
                foreach(var occupation in SelectedOccupations)
                {
                    items2.AddRange(items.FindAll(x => (bool)(x.Occupation?.Equals(occupation.Id.ToString()))));
                }

            }
            else
            {
                items2.AddRange(items);
            }


            var items3 = new List<PatientRevenueModelDto>();
            if (SelectAllStatus)
            {
                if (SelectedStatus != null)
                {
                    if(SelectedStatus.Id == 1)
                    {
                        items3.AddRange(items2.FindAll(x => x.IsPaid));
                    }else if(SelectedStatus.Id == 2)
                    {
                        items3.AddRange(items2.FindAll(x => !x.IsPaid));
                    }
                }
            }
            else
            {
                items3.AddRange(items2);
            }
            

            var reportModel = new PatientRevenueReportModel
            {
                RevenueItems = items3,
                Options = new List<SpecializedLabOptionsModel> { new SpecializedLabOptionsModel {  FromDate = this.FromDate.ToString("dd/MM/yyyy"), ToDate = this.ToDate.ToString("dd/MM/yyyy") } },
                ReportType = BaseReportModel.REPORT_PATIENT_REVENUE_REPORT,
                ReportResourcePath = ReportResourceFilePath.PATIENT_REVENUE_REPORT
            };

            _windowManager.Show<ReportWindow>(reportModel);
        }


        async void GetModelItems()
        {

            if (FromDate == null || ToDate == null)
            {
                MessageBox.Show("Please enter valid date range");
                return;
            }

            IsBusy = Visibility.Visible;
            var response = await financeService.GetPatientRevenueResponseModel(new OPSMBackend.Models.Response.PatientRevenueReportResponseModel { FromDate = this.FromDate, ToDate = this.ToDate });
            IsBusy = Visibility.Hidden;
            if (response != null)
            {
                Labs = response.Hdls;
                _revenueItems = response.RevenueItems;
                Occupations = response.Occupations;
            }
        }

    }

    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Status(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
