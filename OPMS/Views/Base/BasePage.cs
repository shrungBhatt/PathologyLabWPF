using OPMS.Utility;
using OPMS.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OPMS.Views.Base
{
    public class BasePage : Page
    {
        protected ViewModelBase viewModelBase;
        public virtual void OnEnterPressed()
        {

        }
        public virtual void OnEscPressed()
        {

        }

        public virtual void Init(object sender, NavigationEventArgs eventArgs)
        {

        }


        public BasePage()
        {
            KeyDown += BasePage_KeyDown;
            Loaded += BasePage_Loaded;
            Unloaded += BasePage_Unloaded; 

            //If I want some data from the previous screen
            if (NavigationServiceSingleton.Instance.OPSMWindowFrameNavigationService != null)
            {
                App.Logger.Info("Added the navigation service handler");
                NavigationServiceSingleton.Instance.OPSMWindowFrameNavigationService.LoadCompleted += NavigationService_LoadCompleted;
            }
        }

        private void BasePage_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if(viewModelBase != null)
            {
                viewModelBase.PageIsUnloaded(sender, e);
            }
        }

        private void BasePage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (viewModelBase != null)
            {
                viewModelBase.PageIsLoaded(sender, e);
            }
        }

        private void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            Init(sender, e);
            NavigationServiceSingleton.Instance.OPSMWindowFrameNavigationService.LoadCompleted -= NavigationService_LoadCompleted;
            App.Logger.Info("Removed the navigation service handler");
        }

        private void BasePage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Enter:
                    OnEnterPressed();
                    break;
                case System.Windows.Input.Key.Escape:
                    OnEscPressed();
                    break;
            }
        }

    }
}
