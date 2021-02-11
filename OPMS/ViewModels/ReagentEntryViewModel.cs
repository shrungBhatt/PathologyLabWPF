using GalaSoft.MvvmLight.Command;
using OPMS.Models;
using OPMS.Services.Reagent;
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
    public class ReagentEntryViewModel : GenericViewModel<ReagentBillEntries>
    {
        private Reagents selectedReagent;
        public Reagents SelectedReagent
        {
            get => selectedReagent;
            set
            {
                selectedReagent = value;
                RaisePropertyChanged(nameof(SelectedReagent));
            }
        }

        private Dealers selectedDealer;
        public Dealers SelectedDealer
        {
            get => selectedDealer;
            set
            {
                selectedDealer = value;
                RaisePropertyChanged(nameof(SelectedDealer));
            }
        }

        public ICommand SelectReagentCommand { get; set; }
        public ICommand SelectDealerCommand { get; set; }

        private List<Dealers> _dealers;
        private List<Reagents> _reagents;

        private readonly IReagentService _reagentService;
        private readonly IWindowManager _windowManager;
        public ReagentEntryViewModel(IReagentService reagentService, IWindowManager windowManager)
        {
            _reagentService = reagentService;
            _windowManager = windowManager;
            CreateCommands();
            GetModelItems();
        }

        private void CreateCommands()
        {
            SelectReagentCommand = new RelayCommand(SelecteReagentCommand_Invoked);
            SelectDealerCommand = new RelayCommand(SelectTestCommand_Invoked);

        }

        private void SelectTestCommand_Invoked()
        {
            var dataModel = new SelectWindowDataModel<Dealers> { ItemList = _dealers, WindowTitle = "Select dealer" };
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
                if (args.Data is Reagents)
                {
                    UpdateReagentInSelectedModel(args.Data as Reagents);
                }
                else if (args.Data is Dealers)
                {
                    UpdateDealerInSelectedModel(args.Data as Dealers);
                }
            }
        }

        private void UpdateDealerInSelectedModel(Dealers dealer)
        {
            SelectedDealer = dealer;
            SelectedModel.DealerId = dealer.Id;
        }

        private void UpdateReagentInSelectedModel(Reagents reagent)
        {
            SelectedReagent = reagent;
            SelectedModel.ReagentId = reagent.Id;
        }

        private void SelecteReagentCommand_Invoked()
        {
            var dataModel = new SelectWindowDataModel<Reagents> { ItemList = _reagents, WindowTitle = "Select Reagent" };
            var selectWindow = _windowManager.Show<SelectWindow>(dataModel);
            if (selectWindow != null)
            {
                selectWindow.OnOkClicked += SelectWindow_OnOkClicked;
            }
        }

        #region API Calls

        protected async override void GetModelItems()
        {
            IsBusy = Visibility.Visible;
            var response = await _reagentService.GetReagentEntries();
            IsBusy = Visibility.Collapsed;

            if (response != null)
            {
                _dealers = response.Dealers;
                _reagents = response.Reagents;
                if (response.ReagentEntries != null && response.ReagentEntries.Count > 0)
                {
                    ModelDtos = response.ReagentEntries;

                    SelectedModel = ModelDtos.First();
                    EditButtonActiveStateModel.Edit = true;
                }
                else
                {
                    ModelDtos = null;
                    EditButtonActiveStateModel.Edit = false;

                }
            }
        }

        protected async override void AddNewModelItem(ReagentBillEntries model)
        {
            IsBusy = Visibility.Visible;
            model.ModifiedBy = "tester";
            model.ModifiedDate = DateTime.UtcNow;
            var response = await _reagentService.AddNewReagentEntry(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in creating new reagent entry", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void UpdateModelItem(ReagentBillEntries model)
        {
            IsBusy = Visibility.Visible;
            var response = await _reagentService.UpdateReagentEntry(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed in updating the reagent entry", "Error");
                    SelectedModel = oldSelectedModel;
                }
            }
        }

        protected async override void DeleteModelItem(ReagentBillEntries model)
        {
            IsBusy = Visibility.Visible;
            var response = await _reagentService.DeleteReagentEntry(model);
            IsBusy = Visibility.Hidden;

            if (response != null && response.Acknowledgement != null)
            {
                if (response.Acknowledgement.Status.Equals(ResponseStatusCode.success.ToString()))
                {
                    GetModelItems();
                }
                else
                {
                    MessageBox.Show("Failed deleting the reagent entry", "Error");
                }
            }
        }
        #endregion

        #region Override methods
        protected override void OnModelSelected(ReagentBillEntries selectedModel)
        {
            if (selectedModel != null)
            {
                SelectedReagent = selectedModel.Reagent;
                SelectedDealer = selectedModel.Dealer;
            }
        }
        #endregion

    }
}
