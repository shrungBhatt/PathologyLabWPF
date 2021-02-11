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
    public class PatientBillPaymentsReportWindowViewModel : ViewModelBase
    {

        #region Fields


        #endregion

        #region Properties
        public List<Users> SelectedUsers = new List<Users>();
        public List<FieldOptions> SelectedPaymentTypes = new List<FieldOptions>();

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

        List<Users> users;
        public List<Users> Users
        {
            get => users;
            set
            {
                users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }

        List<FieldOptions> paymentTypes;
        public List<FieldOptions> PaymentTypes
        {
            get => paymentTypes;
            set
            {
                paymentTypes = value;
                RaisePropertyChanged(nameof(PaymentTypes));
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
        List<PatientBillPaymentReportDto> _billPaymentItems;

        public PatientBillPaymentsReportWindowViewModel(IWindowManager windowManager,
                                     IFinanceService financeService,
                                     UserRepository userRepository)
        {
            this._windowManager = windowManager;
            this.financeService = financeService;
            this.userRepository = userRepository;
            Statuses = new List<Status> { new Status(1, "Cash Mode"), new Status(2, "Card Mode"), new Status(3, "Cheque Mode") };
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
            var items = new List<PatientBillPaymentReportDto>();

            if (SelectAllReferringHdls)
            {
                foreach (var user in SelectedUsers)
                {
                    items.AddRange(_billPaymentItems.FindAll(x => (bool)(x.EntryDoneBy?.Equals(user.UserName))));
                }
            }
            else
            {
                items.AddRange(_billPaymentItems);
            }

            var items2 = new List<PatientBillPaymentReportDto>();
            if (SelectAllOccupations)
            {
                foreach (var paymentType in SelectedPaymentTypes)
                {
                    items2.AddRange(items.FindAll(x => x.PaymentTypeId == paymentType.Id));
                }

            }
            else
            {
                items2.AddRange(items);
            }


            var items3 = new List<PatientBillPaymentReportDto>();
            if (SelectAllStatus)
            {
                if (SelectedStatus != null)
                {
                    if (SelectedStatus.Id == 1)
                    {
                        items3.AddRange(items2.FindAll(x => x.CashMode));
                    }
                    else if (SelectedStatus.Id == 2)
                    {
                        items3.AddRange(items2.FindAll(x => x.CardMode));
                    }
                    else if (SelectedStatus.Id == 3)
                    {
                        items3.AddRange(items2.FindAll(x => x.ChequeMode));
                    }
                }
            }
            else
            {
                items3.AddRange(items2);
            }


            var reportModel = new PatientBillPaymentsReportModel
            {
                BillPayments = items3,
                Options = new List<SpecializedLabOptionsModel> { new SpecializedLabOptionsModel { FromDate = this.FromDate.ToString("dd/MM/yyyy"), ToDate = this.ToDate.ToString("dd/MM/yyyy") } },
                ReportType = BaseReportModel.REPORT_PATIENT_BILL_PAYMENT_REPORT,
                ReportResourcePath = ReportResourceFilePath.PATIENT_BILL_PAYMENTS_REPORT
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
            var response = await financeService.GetPatientBillPaymentReport(new OPSMBackend.Models.Response.PatientBillPaymentReportResponeModel { FromDate = this.FromDate, ToDate = this.ToDate });
            IsBusy = Visibility.Hidden;
            if (response != null)
            {
                Users = response.Users;
                _billPaymentItems = response.BillPayments;
                PaymentTypes = response.PaymentTypes;
            }
        }

    }
}
