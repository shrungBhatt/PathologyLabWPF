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
using System.Windows;
using System.Windows.Input;

namespace OPMS.ViewModels
{
    public class PatientBillPaymentViewModel : GenericViewModel<PatientBillPayment>
    {
        readonly IFinanceService financeService;
        readonly IWindowManager _windowManager;
        Patient cachedPatient;
        bool navigatedFromPatientBill;
        public PatientBillPaymentViewModel(IFinanceService financeService,
            IWindowManager windowManager)
        {
            this.financeService = financeService;
            _windowManager = windowManager;
            EditToolbarButtonVisibilityState.FlavourTwo();
            CreateCommands();
            GetModelItems();
            SelectedPatient = null;
        }

        #region Field and properties
        private List<Patient> patients;
        public List<Patient> Patients
        {
            get => patients;
            set
            {
                patients = value;
                RaisePropertyChanged(nameof(Patients));
            }
        }

        private Patient selectedPatient;
        public Patient SelectedPatient
        {
            get => selectedPatient;
            set
            {
                selectedPatient = value;
                RaisePropertyChanged(nameof(SelectedPatient));
                if (SelectedPatient == null)
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
                if(SelectedPaymentType != null && SelectedModel != null)
                {
                    SelectedModel.PaymentType = SelectedPaymentType.Id;
                }
            }
        }
        #endregion

        public ICommand SelectPatientCommand { get; set; }
        public ICommand ResetReceiptNoCommand { get; set; }

        private void CreateCommands()
        {
            SelectPatientCommand = new RelayCommand(SelectePatientCommand_Invoked);
            ResetReceiptNoCommand = new RelayCommand(ResetReceiptNoCommand_Invoked);
        }

        private void ResetReceiptNoCommand_Invoked()
        {
            Properties.Numbers.Default.patient_receipt_no = 1;
            Properties.Numbers.Default.Save();
            if (SelectedModel != null)
            {
                SelectedModel.ReceiptNo = Properties.Numbers.Default.patient_receipt_no;
            }
        }

        private void SelectePatientCommand_Invoked()
        {
            var dataModel = new SelectWindowDataModel<Patient> { ItemList = Patients, WindowTitle = "Select Patient" };
            var selectWindow = _windowManager.Show<SelectPatientWindow>(dataModel);
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
                if (args.Data is Patient)
                {
                    OnPatientSelected((Patient)args.Data);
                }

            }
        }

        public void OnPatientSelected(Patient patient)
        {
            if (patient != null)
            {
                SelectedPatient = patient;
                SelectedModel = ModelDtos?.Find(x => x.BillId == patient.Bill?.Id);
                if (SelectedModel == null)
                {
                    EditButtonActiveStateModel.Edit = false;
                    EditButtonActiveStateModel.Add = true;
                    OnAddButtonClicked();
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

        internal void NavigatedFromPatientBill(Patient patient)
        {
            if(patient != null)
            {
                cachedPatient = patient;
                navigatedFromPatientBill = true;
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
            var response = await financeService.GetBillPayments();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                PaymentTypes = response.PaymentTypes;
                if (response.Patients != null && response.Patients.Count > 0)
                {
                    this.Patients = response.Patients;

                    if(cachedPatient != null)
                    {
                        SelectedPatient = Patients.Find(x => x.Id == cachedPatient.Id);
                        cachedPatient = null;
                    }
                    else
                    {
                        SelectedPatient = Patients.First();
                    }

                }

                if(response.BillPayments != null)
                {
                    this.ModelDtos = response.BillPayments;
                }

                if(SelectedPatient != null)
                {
                    OnPatientSelected(SelectedPatient);
                }
            }
        }

        protected async override void AddNewModelItem(PatientBillPayment model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = DIContainer.Resolve<UserRepository>().UserName;
            model.ModifiedDate = DateTime.Now;
            model.BillId = (int)(SelectedPatient?.Bill?.Id);
            model.Bill = null;
            var response = await financeService.InsertPatientBillPayment(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    Properties.Numbers.Default.patient_receipt_no += 1;
                    Properties.Numbers.Default.Save();
                    cachedPatient = SelectedPatient;
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new patient bill payment record", "Error");
                    SelectedModel = oldSelectedModel;
                    if(SelectedModel == null)
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

        protected async override void UpdateModelItem(PatientBillPayment model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = DIContainer.Resolve<UserRepository>().UserName;
            model.ModifiedDate = DateTime.Now;
            var response = await financeService.UpdatePatientBillPayment(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    cachedPatient = SelectedPatient;
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the new patient bill payment record", "Error");
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

        protected async override void DeleteModelItem(PatientBillPayment model)
        {
            IsBusy = Visibility.Visible;
            var response = await financeService.DeletePatientBillPayment(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the patient bill payment record", "Error");
                }
            }
        }
        #endregion
        protected override void OnModelSelected(PatientBillPayment selectedModel)
        {
            //Do nothing
        }

        public override void OnAddButtonClicked()
        {
            base.OnAddButtonClicked();
            if (SelectedModel != null)
            {
                SelectedModel.Bill = SelectedPatient.Bill;
                SelectedModel.PaymentDate = DateTime.Now;
                SelectedModel.ReceiptNo = Properties.Numbers.Default.patient_receipt_no;
                SelectedModel.CashMode = true;
                if(SelectedPatient.Bill != null)
                {
                    SelectedModel.PaymentAmount = SelectedPatient.Bill.FinalAmount;
                }
                else
                {
                    SelectedModel.PaymentAmount = 0;
                }
                
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
