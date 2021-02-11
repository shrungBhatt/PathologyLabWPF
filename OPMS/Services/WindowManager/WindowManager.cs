using OPMS.Utility;
using OPMS.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OPMS.Services.WindowManager
{
    public class WindowManager : IWindowManager
    {
        public void PushPage<TWindow, TPage>(object data, NavigationService navigationService)
            where TWindow : Window
            where TPage : Page
        {
            foreach(Window window in Application.Current.Windows)
            {
                if(window is TWindow)
                {
                    navigationService.Navigate((TPage)Activator.CreateInstance(typeof(TPage)),data);
                    break;
                }
            }
        }

        public TWindow Show<TWindow>(object data) where TWindow : WindowBase
        {
            var window = (TWindow)Activator.CreateInstance(typeof(TWindow));
            window.Init(data);
            window.Show();
            return window;
        }

        public void Close(Window window)
        {
            App.Current.Shutdown();
        }

        public void ClosePopupWindow(Window window)
        {
            if(window != null)
            {
                window.Close();
            }            
        }

        public void ShowNewAndCloseMainWindow<TWindow>(object data) where TWindow : Window
        {
            var window = (TWindow)Activator.CreateInstance(typeof(TWindow));
            window.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
