using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OPMS.Controls.NavigationControl
{
    /// <summary>
    /// Interaction logic for NavigationControl.xaml
    /// </summary>
    public partial class NavigationControl : UserControl
    {
        public NavigationControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty NavigationInteractionProperty = DependencyProperty.Register("NavigationInteraction", typeof(INavigationlnteraction), typeof(NavigationControl), new UIPropertyMetadata(new PropertyChangedCallback(InteractionProperty_Changed)));

        private static void InteractionProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NavigationControl).NavigationInteraction = (INavigationlnteraction)e.NewValue;
        }

        public INavigationlnteraction NavigationInteraction
        {
            get => (INavigationlnteraction)GetValue(NavigationInteractionProperty);
            set => SetValue(NavigationInteractionProperty, value);
        }

        public static readonly DependencyProperty NavButtonStateProperty = DependencyProperty
            .Register(nameof(NavButtonState), typeof(NavToolbarButtonActiveStateModel), typeof(NavigationControl),
            new UIPropertyMetadata(new PropertyChangedCallback(ButtonStateProperty_Changed)));

        public NavToolbarButtonActiveStateModel NavButtonState
        {
            get => (NavToolbarButtonActiveStateModel)GetValue(NavButtonStateProperty);
            set => SetValue(NavButtonStateProperty, value);
        }

        private static void ButtonStateProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NavigationControl).NavButtonState = (NavToolbarButtonActiveStateModel)e.NewValue;
        }

        public static readonly DependencyProperty NavButtonStateVisibilityProperty = DependencyProperty
            .Register(nameof(NavButtonStateVisibility), typeof(NavToolbarButtonVisibilityStateModel), typeof(NavigationControl),
            new UIPropertyMetadata(new PropertyChangedCallback(ButtonStateVisibilityProperty_Changed)));

        public NavToolbarButtonVisibilityStateModel NavButtonStateVisibility
        {
            get => (NavToolbarButtonVisibilityStateModel)GetValue(NavButtonStateVisibilityProperty);
            set => SetValue(NavButtonStateVisibilityProperty, value);
        }

        private static void ButtonStateVisibilityProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NavigationControl).NavButtonStateVisibility = (NavToolbarButtonVisibilityStateModel)e.NewValue;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationInteraction?.GoBack();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            NavigationInteraction?.GoForward();
        }
    }
}
