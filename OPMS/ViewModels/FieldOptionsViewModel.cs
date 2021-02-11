using OPMS.Services.Maintenance;
using OPMS.Services.RequestProvider;
using OPMS.ViewModels.Base;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPMS.ViewModels
{
    public class FieldOptionsViewModel : GenericViewModel<FieldOptions>
    {

        private readonly IMaintenanceService maintenanceService;

        public FieldOptionsViewModel(IMaintenanceService maintenanceService)
        {
            this.maintenanceService = maintenanceService;

            GetModelItems();
        }

        private List<FieldOptions> allFieldOptions;
        public List<FieldOptions> AllFieldOptions
        {
            get => allFieldOptions;
            set
            {
                allFieldOptions = value;
                RaisePropertyChanged(nameof(AllFieldOptions));
            }
        }

        private List<FieldOptions> civilStatuses;
        public List<FieldOptions> CivilStatuses
        {
            get => civilStatuses;
            set
            {
                civilStatuses = value;
                RaisePropertyChanged(nameof(CivilStatuses));
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

        private List<FieldOptions> addresses;
        public List<FieldOptions> Addresses
        {
            get => addresses;
            set
            {
                addresses = value;
                RaisePropertyChanged(nameof(Addresses));
            }
        }

        private List<FieldOptions> occupations;
        public List<FieldOptions> Occupations
        {
            get => occupations;
            set
            {
                occupations = value;
                RaisePropertyChanged(nameof(Occupations));
            }
        }

        private List<Fields> fields;
        public List<Fields> Fields
        {
            get => fields;
            set
            {
                fields = value;
                RaisePropertyChanged(nameof(Fields));
            }
        }

        private Fields selectedField;
        public Fields SelectedField
        {
            get => selectedField;
            set
            {
                selectedField = value;
                RaisePropertyChanged(nameof(SelectedField));
                if (selectedField != null)
                {
                    switch (selectedField.Id)
                    {
                        case 1:
                            ModelDtos = Addresses;
                            break;
                        case 2:
                            ModelDtos = Occupations;
                            break;
                        case 3:
                            ModelDtos = CivilStatuses;
                            break;
                        case 4:
                            ModelDtos = PaymentTypes;
                            break;
                        case 99:
                            ModelDtos = AllFieldOptions;
                            break;
                    }
                    if (ModelDtos != null && ModelDtos.Count > 0)
                    {
                        SelectedModel = ModelDtos.First();
                    }
                }
            }
        }

        #region API Calls

        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.GetFieldOptions();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                Fields = response.Fields;
                if (response.FieldOptions != null && response.FieldOptions.Count > 0)
                {
                    AllFieldOptions = response.FieldOptions;
                    Addresses = response.FieldOptions.FindAll(x => x.FieldId == 1).ToList();
                    Occupations = response.FieldOptions.FindAll(x => x.FieldId == 2).ToList();
                    CivilStatuses = response.FieldOptions.FindAll(x => x.FieldId == 3).ToList();
                    PaymentTypes = response.FieldOptions.FindAll(x => x.FieldId == 4).ToList();

                    var firstOption = response.FieldOptions.First();
                    SelectedField = Fields?.Find(x => x.Id == firstOption.FieldId);
                    EditButtonActiveStateModel.Edit = true;
                }
                else
                {
                    ModelDtos = null;
                    EditButtonActiveStateModel.Edit = false;
                }
            }
        }

        protected async override void AddNewModelItem(FieldOptions model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = "tester";
            model.ModifiedDate = DateTime.UtcNow;
            model.FieldId = (int)(SelectedField?.Id);
            var response = await maintenanceService.AddNewFieldOption(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new field option", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(FieldOptions model)
        {
            IsBusy = Visibility.Visible;
            model.FieldId = (int)(SelectedField?.Id);
            var response = await maintenanceService.UpdateFieldOption(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the field option", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(FieldOptions model)
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.DeleteFieldOption(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the field option", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(FieldOptions selectedModel)
        {
            //Do nothing
        }
        #endregion

    }
}
