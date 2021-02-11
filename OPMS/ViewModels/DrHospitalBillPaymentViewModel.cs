using GalaSoft.MvvmLight.Command;
using OPMS.Models;
using OPMS.Repository;
using OPMS.Services.Finance;
using OPMS.Services.RequestProvider;
using OPMS.Services.WindowManager;
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
    public class DrHospitalBillPaymentViewModel : GenericViewModel<HdlBillPayment>
    {

        readonly IFinanceService financeService;
        readonly IWindowManager _windowManager;
        public DrHospitalBillPaymentViewModel(IFinanceService financeService,
            IWindowManager windowManager)
        {
            this.financeService = financeService;
            _windowManager = windowManager;
            EditToolbarButtonVisibilityState.FlavourTwo();
            CreateCommands();
            GetModelItems();
            SelectedHdl = null;
        }

        #region Field and properties
        private List<HdlRegistration> hdlRegistrations;
        public List<HdlRegistration> HdlRegistrations
        {
            get => hdlRegistrations;
            set
            {
                hdlRegistrations = value;
                RaisePropertyChanged(nameof(HdlRegistrations));
            }
        }

        private HdlRegistration selectedHdl;
        public HdlRegistration SelectedHdl
        {
            get => selectedHdl;
            set
            {
                selectedHdl = value;
                RaisePropertyChanged(nameof(SelectedHdl));
                if (SelectedHdl == null)
                {
                    EditButtonActiveStateModel.Edit = false;
                    EditButtonActiveStateModel.Add = false;
                }
                else
                {
                    EditButtonActiveStateModel.Edit = true;
                    EditButtonActiveStateModel.Add = true;
                }
            }
        }

        private List<FieldOptions> paymentTypes;
        public List<FieldOptions> PaymentTypes
        {
            get => paymentTypes;
            set
            {
                paymentTypes = value;
                RaisePropertyChanged(nameof(PaymentTypes));
            }
        }

        private FieldOptions selectedPaymentType;
        public FieldOptions SelectedPaymentType
        {
            get => selectedPaymentType;
            set
            {
                selectedPaymentType = value;
                RaisePropertyChanged(nameof(SelectedPaymentType));
                if (SelectedPaymentType != null && SelectedModel != null)
                {
                    SelectedModel.PaymentType = SelectedPaymentType.Id;
                }
            }
        }
        #endregion

        public ICommand SelectHdlCommand { get; set; }
        public ICommand ResetReceiptNoCommand { get; set; }

        private void CreateCommands()
        {
            SelectHdlCommand = new RelayCommand(SelectePatientCommand_Invoked);
            ResetReceiptNoCommand = new RelayCommand(ResetReceiptNoCommand_Invoked);
        }

        private void ResetReceiptNoCommand_Invoked()
        {
            Properties.Numbers.Default.hdl_receipt_no = 1;
            Properties.Numbers.Default.Save();
            if (SelectedModel != null)
            {
                SelectedModel.ReceiptNo = Properties.Numbers.Default.hdl_receipt_no;
            }
        }

        private void SelectePatientCommand_Invoked()
        {
            var dataModel = new SelectWindowDataModel<HdlRegistration> { ItemList = HdlRegistrations, WindowTitle = "Select Hospital/Doctor/Laboratory" };
            var selectWindow = _windowManager.Show<SelectWindow>(dataModel);
            if (selectWindow != null)
            {
                selectWindow.OnOkClicked += SelectWindow_OnOkClicked;
            }
        }

        private void SelectWindow_OnOkClicked(object sender, EventArgs e)
        {
            if (e is SelectWindowEventArgs)
            {
                var args = e as SelectWindowEventArgs;
                if (args.Data is HdlRegistration)
                {
                    OnHdlSelected((HdlRegistration)args.Data);
                }

            }
        }

        public void OnHdlSelected(HdlRegistration hdlRegistration)
        {
            if (hdlRegistration != null)
            {
                SelectedHdl = hdlRegistration;
                SelectedModel = ModelDtos?.Find(x => x.BillId == hdlRegistration.Bill?.Id);
                if (SelectedModel == null)
                {
                    SelectedPaymentType = null;
                    EditButtonActiveStateModel.Edit = false;
                    EditButtonActiveStateModel.Add = true;
                }
                else
                {
                    SelectedPaymentType = PaymentTypes?.Find(x => x.Id == SelectedModel.PaymentType);
                    SelectedModel.Balance = SelectedModel.Bill.FinalAmount - SelectedModel.PaymentAmount;
                    EditButtonActiveStateModel.Edit = true;
                    EditButtonActiveStateModel.Add = false;
                }
            }
        }

        protected override void OnRefreshButtonClicked()
        {
            OnCloseButtonClicked();
            GetModelItems();
        }

        #region API Calls
        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await financeService.GetHdlBillPayments();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                PaymentTypes = response.PaymentTypes;
                if (response.HdlRegistrations != null && response.HdlRegistrations.Count > 0)
                {
                    this.HdlRegistrations = response.HdlRegistrations;
                    SelectedHdl = HdlRegistrations.First();
                }

                if (response.HdlBillPayments != null)
                {
                    this.ModelDtos = response.HdlBillPayments;
                }

                if (SelectedHdl != null)
                {
                    OnHdlSelected(SelectedHdl);
                }
            }
        }

        protected async override void AddNewModelItem(HdlBillPayment model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = DIContainer.Resolve<UserRepository>().UserName;
            model.ModifiedDate = DateTime.Now;
            model.BillId = (int)(SelectedHdl?.Bill?.Id);
            model.FromDate = (DateTime)(SelectedModel?.Bill?.FromDate);
            model.ToDate = (DateTime)(SelectedModel?.Bill?.ToDate);
            model.Bill = null;
            var response = await financeService.InsertHdlBillPayment(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    Properties.Numbers.Default.hdl_receipt_no += 1;
                    Properties.Numbers.Default.Save();
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new bill payment record", "Error");
                    SelectedModel = oldSelectedModel;
                    if (SelectedModel == null)
                    {
                        EditButtonActiveStateModel.Add = true;
                        EditButtonActiveStateModel.Edit = false;
                    }
                    else
                    {
                        EditButtonActiveStateModel.Add = false;
                        EditButtonActiveStateModel.Edit = true;
                    }
                }
            }
        }

        protected async override void UpdateModelItem(HdlBillPayment model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = DIContainer.Resolve<UserRepository>().UserName;
            model.ModifiedDate = DateTime.Now;
            var response = await financeService.UpdateHdlBillPayment(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the new bill payment record", "Error");
                    SelectedModel = oldSelectedModel;
                    if (SelectedModel == null)
                    {
                        EditButtonActiveStateModel.Add = true;
                        EditButtonActiveStateModel.Edit = false;
                    }
                    else
                    {
                        EditButtonActiveStateModel.Add = false;
                        EditButtonActiveStateModel.Edit = true;
                    }
                }
            }
        }

        protected async override void DeleteModelItem(HdlBillPayment model)
        {
            IsBusy = Visibility.Visible;
            var response = await financeService.DeleteHdlBillPayment(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the bill payment record", "Error");
                }
            }
        }
        #endregion
        protected override void OnModelSelected(HdlBillPayment selectedModel)
        {
            //Do nothing
        }

        public override void OnAddButtonClicked()
        {
            base.OnAddButtonClicked();
            if (SelectedModel != null)
            {
                SelectedModel.Bill = SelectedHdl.Bill;
                SelectedModel.PaymentDate = DateTime.Now;
                SelectedModel.ReceiptNo = Properties.Numbers.Default.hdl_receipt_no;
                SelectedModel.CashMode = true;
                SelectedModel.PaymentAmount = 0;
                SelectedPaymentType = PaymentTypes.Find(x => x.Id == 2003);
            }
        }

        public override void OnEditButtonClicked()
        {
            base.OnEditButtonClicked();
            if (SelectedModel != null)
            {
                EditButtonActiveStateModel.Add = false;
            }
            else
            {
                EditButtonActiveStateModel.Add = true;
            }
        }

        public override void OnCloseButtonClicked()
        {
            base.OnCloseButtonClicked();
            if (SelectedModel != null)
            {
                EditButtonActiveStateModel.Add = false;
                EditButtonActiveStateModel.Edit = true;
            }
            else
            {
                EditButtonActiveStateModel.Add = true;
                EditButtonActiveStateModel.Edit = false;
            }
        }

    }
}
