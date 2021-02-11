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
    public interface IWindowManager
    {
        TWindow Show<TWindow>(object data) where TWindow : WindowBase;

        void ShowNewAndCloseMainWindow<TWindow>(object data) where TWindow : Window;

        void PushPage<TWindow, TPage>(object data, NavigationService navigationService) where TWindow : Window where TPage : Page;

        void Close(Window window);

        void ClosePopupWindow(Window window);
    }
}
