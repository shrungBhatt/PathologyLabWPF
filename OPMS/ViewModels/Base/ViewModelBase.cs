using OPMS.Controls;
using OPMS.Controls.MoreOptionsMenu;
using OPMS.Controls.NavigationControl;
using OPMS.Controls.ToolbarView;
using OPMS.Services.WindowManager;
using OPMS.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPMS.ViewModels.Base
{
    public class ViewModelBase : DependencyObject, INotifyPropertyChanged, IEditToolBarInteraction, IMoreMenuInteraction, INavigationlnteraction
    {

        private Visibility isBusy = Visibility.Collapsed;
        public Visibility IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }

        private bool isInvalid;
        public bool IsInvalid
        {
            get => isInvalid;
            set
            {
                isInvalid = value;
                RaisePropertyChanged(nameof(isInvalid));
            }
        }

        public IEditToolBarInteraction EditBarInteraction
        {
            get => this;
        }

        public IMoreMenuInteraction MoreMenuInteraction
        {
            get => this;
        }

        public INavigationlnteraction Navigationlnteraction
        {
            get => this;
        }

        public EditToolbarButtonActiveStateModel EditButtonActiveStateModel { get; set; } = new EditToolbarButtonActiveStateModel();
        public EditToolbarButtonVisibilityStateModel EditToolbarButtonVisibilityState { get; set; } = new EditToolbarButtonVisibilityStateModel();

        public NavToolbarButtonActiveStateModel NavToolbarButtonActiveStateModel { get; set; } = new NavToolbarButtonActiveStateModel();
        public NavToolbarButtonVisibilityStateModel NavToolbarButtonVisibilityStateModel { get; set; } = new NavToolbarButtonVisibilityStateModel();

        readonly IWindowManager _windowManager;



        public ViewModelBase()
        {
            _windowManager = DIContainer.Resolve<IWindowManager>();
            UpdateNavButtonStates();
        }

        protected List<MoreOptionsMenuModel> moreOptionsMenuModels = new List<MoreOptionsMenuModel>
            {
                new MoreOptionsMenuModel
                {
                    Id = MoreOptionsMenuModel.Refresh,
                    Title = "Refresh"
                }
            };
        public List<MoreOptionsMenuModel> MoreOptionsMenuModels
        {
            get => moreOptionsMenuModels;
            set
            {
                moreOptionsMenuModels = value;
                RaisePropertyChanged(nameof(MoreOptionsMenuModels));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Edit toolbar interface callbacks
        public virtual void OnSaveButtonClicked()
        {
            EditButtonActiveStateModel.TurnOffEditMode();
        }

        public virtual void OnEditButtonClicked()
        {
            EditButtonActiveStateModel.TurnOnEditMode();
        }

        public virtual void OnDeleteButtonClicked()
        {
            EditButtonActiveStateModel.TurnOffEditMode();
        }

        public virtual void OnCloseButtonClicked()
        {
            EditButtonActiveStateModel.TurnOffEditMode();
        }

        public virtual void OnAddButtonClicked()
        {
            EditButtonActiveStateModel.TurnOnAddMode();
        }

        public virtual void OnLastButtonClicked()
        {
        }

        public virtual void OnNextButtonClicked()
        {
        }

        public virtual void OnPreviousButtonClicked()
        {
        }

        public virtual void OnFirstButtonClicked()
        {
        }
        #endregion

        #region More Options menu interface callbacks
        public void OnExitButtonClicked()
        {
            var result = MessageBox.Show("Are you sure you want to exit? All the unsaved changes will be lost if you choose to proceed", "Close", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _windowManager.Close(Application.Current.MainWindow);
            }
        }

        public void OnMenuButtonClicked(string menuId)
        {
            switch (menuId)
            {
                case MoreOptionsMenuModel.Refresh:
                    OnRefreshButtonClicked();
                    break;
                default:
                    OnOtherButtonClicked(menuId);
                    break;
            }
        }

        protected virtual void OnRefreshButtonClicked()
        {

        }

        protected virtual void OnOtherButtonClicked(string menuId)
        {

        }


        #endregion

        public void GoBack()
        {
            var navService = NavigationServiceSingleton.Instance.OPSMWindowFrameNavigationService;
            if (navService != null && navService.CanGoBack)
            {
                NavigationServiceSingleton.Instance.OPSMWindowFrameNavigationService.GoBack();
            }
            UpdateNavButtonStates();
        }

        public void GoForward()
        {
            var navService = NavigationServiceSingleton.Instance.OPSMWindowFrameNavigationService;
            if (navService != null && navService.CanGoForward)
            {
                NavigationServiceSingleton.Instance.OPSMWindowFrameNavigationService.GoForward();
            }
            UpdateNavButtonStates();
        }

        private void UpdateNavButtonStates()
        {
            var navService = NavigationServiceSingleton.Instance.OPSMWindowFrameNavigationService;

            if (navService != null)
            {
                navService.LoadCompleted -= NavService_LoadCompleted;
                navService.LoadCompleted += NavService_LoadCompleted;

                if (navService.CanGoBack)
                {
                    NavToolbarButtonActiveStateModel.Back = true;
                }
                else
                {
                    NavToolbarButtonActiveStateModel.Back = false;
                }

                if (navService.CanGoForward)
                {
                    NavToolbarButtonActiveStateModel.Forward = true;
                }
                else
                {
                    NavToolbarButtonActiveStateModel.Forward = false;
                }
            }


        }

        private void NavService_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            UpdateNavButtonStates();
        }

        internal virtual void PageIsLoaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        internal virtual void PageIsUnloaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
