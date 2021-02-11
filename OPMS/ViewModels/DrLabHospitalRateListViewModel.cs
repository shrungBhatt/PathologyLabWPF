using GalaSoft.MvvmLight.Command;
using OPMS.Models;
using OPMS.Services.Maintenance;
using OPMS.Services.RequestProvider;
using OPMS.Services.WindowManager;
using OPMS.Utility;
using OPMS.ViewModels.Base;
using OPMS.Views;
using OPMS.Windows;
using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace OPMS.ViewModels
{
    public class DrLabHospitalRateListViewModel : GenericViewModel<RateListModel>
    {

        readonly IMaintenanceService maintenanceService;
        readonly IWindowManager _windowManager;
        List<TestTitles> _testTitles;


        #region Properties
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

        HdlRegistration selectedHdlRegistration;
        public HdlRegistration SelectedHdlRegistration
        {
            get => selectedHdlRegistration;
            set
            {
                selectedHdlRegistration = value;
                RaisePropertyChanged(nameof(SelectedHdlRegistration));
            }
        }


        object rateLists;
        public object RateLists
        {
            get => rateLists;
            set
            {
                rateLists = value;
                RaisePropertyChanged(nameof(RateLists));
            }
        }

        private BaseEntity selectedRateListItem;
        public BaseEntity SelectedRateListItem
        {
            get => selectedRateListItem;
            set
            {
                selectedRateListItem = value;
                RaisePropertyChanged(nameof(SelectedRateListItem));
            }
        }

        public List<ReferringCutType> ReferringCutTypes { get => ReferringCutType.GetReferringCutTypes(); }

        ReferringCutType selectedReferringCut;
        public ReferringCutType SelectedReferringCut
        {
            get => selectedReferringCut;
            set
            {
                selectedReferringCut = value;
                RaisePropertyChanged(nameof(SelectedReferringCut));
            }
        }

        float cutValue = 0;
        public float CutValue
        {
            get => cutValue;
            set
            {
                cutValue = value;
                RaisePropertyChanged(nameof(CutValue));
            }
        }

        private bool isReferrCutAvailable = false;
        public bool IsReferrCutAvailable
        {
            get => isReferrCutAvailable;
            set
            {
                isReferrCutAvailable = value;
                RaisePropertyChanged(nameof(IsReferrCutAvailable));
            }
        }

        public IRateListViewInteraction RateListViewInteraction { get; set; }
        #endregion

        public DrLabHospitalRateListViewModel(IWindowManager windowManager,
            IMaintenanceService maintenanceService)
        {
            _windowManager = windowManager;
            this.maintenanceService = maintenanceService;
            EditToolbarButtonVisibilityState.FlavourTwo();
            CreateCommands();
            GetModelItems();
        }

        public ICommand SelectDhlCommand { get; set; }
        public ICommand ApplyCutCommand { get; set; }

        private void CreateCommands()
        {
            SelectDhlCommand = new RelayCommand(SelectDhlCommand_Invoked);
            ApplyCutCommand = new RelayCommand(ApplyCutCommand_Invoked);
        }

        private void ApplyCutCommand_Invoked()
        {
            //Iterate over each item and apply the cut
            if (SelectedReferringCut != null)
            {
                if (RateLists.GetType() == typeof(List<ReferringRateList>))
                {
                    var list = (List<ReferringRateList>)RateLists;
                    foreach (var item in list)
                    {

                        if (SelectedReferringCut.Id == 1)
                        {
                            item.ReferringPercentage = 0;
                            item.ReferringAmount = CutValue;
                        }
                        else if (SelectedReferringCut.Id == 2)
                        {
                            item.ReferringAmount = 0;
                            item.ReferringPercentage = CutValue;
                        }

                    }

                    RaisePropertyChanged(nameof(RateLists));
                    RateListViewInteraction?.RefreshGrid();
                }
            }
            else
            {
                MessageBox.Show("Please select the type of referring cut. Either select \"Amount\" or \"Percentage\"", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        private void SelectDhlCommand_Invoked()
        {
            var dataModel = new SelectWindowDataModel<HdlRegistration> { ItemList = hdlRegistrations, WindowTitle = "Select Doctor/Hospital/Laboratory" };
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
                    OnHdlSelected(args.Data as HdlRegistration);
                }

            }
        }

        void OnHdlSelected(HdlRegistration hdlRegistration)
        {
            SelectedHdlRegistration = hdlRegistration;
            if (ModelDtos != null)
            {
                EditButtonActiveStateModel.Edit = true;
                EditButtonActiveStateModel.Add = true;
                var hdl = ModelDtos.Find(x => x.HdlRegistration.Id == hdlRegistration.Id);
                if (hdl != null)
                {
                    SelectedModel = hdl;
                }
            }
            else
            {
                EditButtonActiveStateModel.Edit = false;
                EditButtonActiveStateModel.Add = false;
            }
        }

        #region API Calls
        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.GetRateLists();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                HdlRegistrations = response.HdlRegistrations;
                _testTitles = response.TestTitles;
                if (response.RateListModels != null && response.RateListModels.Count > 0)
                {
                    ModelDtos = response.RateListModels;
                    OnHdlSelected(ModelDtos.First().HdlRegistration);
                }
                else
                {
                    EditButtonActiveStateModel.Add = false;
                    EditButtonActiveStateModel.Edit = false;
                }

            }
        }

        protected async override void AddNewModelItem(RateListModel model)
        {
            IsBusy = Visibility.Visible;
            UpdateRateLists(model, RateLists);
            var response = await maintenanceService.AddNewRateList(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new rate list", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(RateListModel model)
        {
            IsBusy = Visibility.Visible;
            UpdateRateLists(model, RateLists);
            var response = await maintenanceService.UpdateRatelist(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the rate list of the selected registration", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        void UpdateRateLists(RateListModel model, object rateList)
        {
            if (rateList.GetType() == typeof(List<SpecializedLabRateList>))
            {
                model.SpecializedLabRateLists = (List<SpecializedLabRateList>)RateLists;
            }
            else if (rateList.GetType() == typeof(List<MonthlyRateList>))
            {
                model.MonthlyRateLists = (List<MonthlyRateList>)RateLists;
            }
            else if (rateList.GetType() == typeof(List<ReferringRateList>))
            {
                model.ReferredRateLists = (List<ReferringRateList>)RateLists;
            }
        }

        protected async override void DeleteModelItem(RateListModel model)
        {
            IsBusy = Visibility.Visible;
            var response = await maintenanceService.DeleteRateList(model.HdlRegistration);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the rate list of the selected registration", "Error");
                }
            }
        }
        #endregion
        protected override void OnModelSelected(RateListModel selectedModel)
        {
            if (SelectedModel != null)
            {
                if (SelectedHdlRegistration == null || selectedModel.HdlRegistration == null)
                {
                    return;
                }

                SelectedReferringCut = ReferringCutTypes?.First();

                SelectedHdlRegistration = selectedModel.HdlRegistration;

                if (SelectedModel.HdlRegistration.RegistrationTypeId == 3)
                {
                    IsReferrCutAvailable = false;
                    RateLists = SelectedModel.SpecializedLabRateLists?.Clone();
                }
                else
                {
                    if (SelectedModel.HdlRegistration.RegistrationCategoryId == 1)
                    {
                        IsReferrCutAvailable = false;
                        RateLists = SelectedModel.MonthlyRateLists?.Clone();
                    }
                    else if (SelectedModel.HdlRegistration.RegistrationCategoryId == 2)
                    {
                        IsReferrCutAvailable = true;
                        RateLists = SelectedModel.ReferredRateLists?.Clone();
                    }
                }

                if (RateLists != null &&
                    (SelectedModel.MonthlyRateLists.Count > 0
                        || SelectedModel.ReferredRateLists.Count > 0
                        || SelectedModel.SpecializedLabRateLists.Count > 0))
                {
                    EditButtonActiveStateModel.Edit = true;
                    EditButtonActiveStateModel.Add = false;
                }
                else
                {
                    EditButtonActiveStateModel.Edit = false;
                    EditButtonActiveStateModel.Add = true;
                }

            }
        }

        SpecializedLabRateList GetSpecializedLabRateList(HdlRegistration hdlRegistration, TestTitles testTitle)
        {
            var model = new SpecializedLabRateList();
            model.Charges = 0;
            model.HdlId = hdlRegistration.Id;
            model.TestTitleId = testTitle.Id;
            model.TestTitle = testTitle;
            return model;
        }

        MonthlyRateList GetMonthlyRateList(HdlRegistration hdlRegistration, TestTitles testTitle)
        {
            var model = new MonthlyRateList();
            model.Charges = testTitle.Charges;
            model.HdlId = hdlRegistration.Id;
            model.TestTitleId = testTitle.Id;
            model.TestTitle = testTitle;
            return model;
        }

        ReferringRateList GetReferringRateList(HdlRegistration hdlRegistration, TestTitles testTitle)
        {
            var model = new ReferringRateList();
            model.ReferringPercentage = 0;
            model.ReferringAmount = 0;
            model.HdlId = hdlRegistration.Id;
            model.TestTitleId = testTitle.Id;
            model.TestTitle = testTitle;
            return model;
        }

        public override void OnAddButtonClicked()
        {
            base.OnAddButtonClicked();
            if (SelectedModel != null)
            {
                if (SelectedHdlRegistration == null)
                {
                    return;
                }

                if (SelectedModel.HdlRegistration == null)
                {
                    SelectedModel.HdlRegistration = SelectedHdlRegistration;
                }

                if (SelectedModel.HdlRegistration.RegistrationTypeId == 3)
                {
                    if (SelectedModel.SpecializedLabRateLists == null)
                    {
                        SelectedModel.SpecializedLabRateLists = new List<SpecializedLabRateList>();
                        if (_testTitles != null)
                        {
                            foreach (var test in _testTitles)
                            {
                                SelectedModel.SpecializedLabRateLists.Add(GetSpecializedLabRateList(SelectedModel.HdlRegistration, test));
                            }
                        }
                    }

                    //Load the specialized charges
                    RateLists = SelectedModel.SpecializedLabRateLists.Clone();
                }
                else
                {
                    if (SelectedModel.HdlRegistration.RegistrationCategoryId == 1)
                    {
                        if (SelectedModel.MonthlyRateLists == null)
                        {
                            SelectedModel.MonthlyRateLists = new List<MonthlyRateList>();
                            if (_testTitles != null)
                            {
                                foreach (var test in _testTitles)
                                {
                                    SelectedModel.MonthlyRateLists.Add(GetMonthlyRateList(SelectedModel.HdlRegistration, test));
                                }
                            }
                        }

                        RateLists = SelectedModel.MonthlyRateLists.Clone();
                    }
                    else if (SelectedModel.HdlRegistration.RegistrationCategoryId == 2)
                    {
                        if (SelectedModel.ReferredRateLists == null)
                        {
                            SelectedModel.ReferredRateLists = new List<ReferringRateList>();
                            if (_testTitles != null)
                            {
                                foreach (var test in _testTitles)
                                {
                                    SelectedModel.ReferredRateLists.Add(GetReferringRateList(SelectedModel.HdlRegistration, test));
                                }
                            }
                        }

                        RateLists = SelectedModel.ReferredRateLists.Clone();
                    }
                }
            }
        }

        object clonedRateList = new object();
        public override void OnEditButtonClicked()
        {
            if (RateLists.GetType() == typeof(List<SpecializedLabRateList>))
            {
                clonedRateList = ((List<SpecializedLabRateList>)RateLists).Clone();
            }
            else if (RateLists.GetType() == typeof(List<MonthlyRateList>))
            {
                clonedRateList = ((List<MonthlyRateList>)RateLists).Clone();
            }
            else if (RateLists.GetType() == typeof(List<ReferringRateList>))
            {
                clonedRateList = ((List<ReferringRateList>)RateLists).Clone();
            }

            base.OnEditButtonClicked();
            EditButtonActiveStateModel.Edit = false;
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
                RateLists = clonedRateList;

                if (SelectedModel.MonthlyRateLists.Count > 0
                        || SelectedModel.ReferredRateLists.Count > 0
                        || SelectedModel.SpecializedLabRateLists.Count > 0)
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
            else
            {
                EditButtonActiveStateModel.Add = true;
                EditButtonActiveStateModel.Edit = false;
            }
        }
    }


    public class ReferringCutType
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ReferringCutType()
        {

        }

        public static List<ReferringCutType> GetReferringCutTypes()
        {
            return new List<ReferringCutType>
            {
                new ReferringCutType
                {
                    Id = 1,
                    Title = "Amount"
                },
                new ReferringCutType
                {
                    Id = 2,
                    Title = "Percentage"
                }
            };
        }
    }
}
