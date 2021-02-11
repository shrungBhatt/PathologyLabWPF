using OPMS.Controls.MoreOptionsMenu;
using OPMS.Controls.NavigationControl;
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

namespace OPMS.Controls.ToolbarView
{
    /// <summary>
    /// Interaction logic for ToolbarView.xaml
    /// </summary>
    public partial class ToolbarView : UserControl
    {

        #region Properties

        #region Nav toolbar view
        public static readonly DependencyProperty NavigationInteractionProperty = DependencyProperty.Register(nameof(NavigationInteraction), typeof(INavigationlnteraction), typeof(ToolbarView), new UIPropertyMetadata(new PropertyChangedCallback(InteractionProperty_Changed)));

        private static void InteractionProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ToolbarView).NavigationInteraction = (INavigationlnteraction)e.NewValue;
        }

        public INavigationlnteraction NavigationInteraction
        {
            get => (INavigationlnteraction)GetValue(NavigationInteractionProperty);
            set => SetValue(NavigationInteractionProperty, value);
        }

        public static readonly DependencyProperty NavButtonStateProperty = DependencyProperty
            .Register(nameof(NavButtonState), typeof(NavToolbarButtonActiveStateModel), typeof(ToolbarView),
            new UIPropertyMetadata(new PropertyChangedCallback(NavButtonStateProperty_Changed)));

        public NavToolbarButtonActiveStateModel NavButtonState
        {
            get => (NavToolbarButtonActiveStateModel)GetValue(NavButtonStateProperty);
            set => SetValue(NavButtonStateProperty, value);
        }

        private static void NavButtonStateProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ToolbarView).NavButtonState = (NavToolbarButtonActiveStateModel)e.NewValue;
        }

        public static readonly DependencyProperty NavButtonStateVisibilityProperty = DependencyProperty
            .Register(nameof(NavButtonStateVisibility), typeof(NavToolbarButtonVisibilityStateModel), typeof(ToolbarView),
            new UIPropertyMetadata(new PropertyChangedCallback(NavButtonStateVisibilityProperty_Changed)));

        public NavToolbarButtonVisibilityStateModel NavButtonStateVisibility
        {
            get => (NavToolbarButtonVisibilityStateModel)GetValue(NavButtonStateVisibilityProperty);
            set => SetValue(NavButtonStateVisibilityProperty, value);
        }

        private static void NavButtonStateVisibilityProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ToolbarView).NavButtonStateVisibility = (NavToolbarButtonVisibilityStateModel)e.NewValue;
        }
        #endregion

        #region Title view
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title),
            typeof(string), typeof(ToolbarView), new UIPropertyMetadata("Test",TitlePropertyChanged_Callback));

        public static void TitlePropertyChanged_Callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ToolbarView).Title = (string)e.NewValue;
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        #endregion

        #region Edit toolbar view
        public static readonly DependencyProperty EditToolBarInteractionProperty = DependencyProperty.Register(
            nameof(EditToolBarInteraction), typeof(IEditToolBarInteraction), typeof(ToolbarView), new UIPropertyMetadata(EditToolBarInteractionProperty_Changed));

        private static void EditToolBarInteractionProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ToolbarView).EditToolBarInteraction = (IEditToolBarInteraction)e.NewValue;
        }

        public IEditToolBarInteraction EditToolBarInteraction
        {
            get => (IEditToolBarInteraction)GetValue(EditToolBarInteractionProperty);
            set => SetValue(EditToolBarInteractionProperty, value);
        }

        public static readonly DependencyProperty ButtonStateProperty = DependencyProperty
            .Register(nameof(ButtonState), typeof(EditToolbarButtonActiveStateModel), typeof(ToolbarView),
            new UIPropertyMetadata(new PropertyChangedCallback(ButtonStateProperty_Changed)));

        public EditToolbarButtonActiveStateModel ButtonState
        {
            get => (EditToolbarButtonActiveStateModel)GetValue(ButtonStateProperty);
            set => SetValue(ButtonStateProperty, value);
        }

        private static void ButtonStateProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ToolbarView).ButtonState = (EditToolbarButtonActiveStateModel)e.NewValue;
        }

        public static readonly DependencyProperty ButtonStateVisibilityProperty = DependencyProperty
            .Register(nameof(ButtonStateVisibility), typeof(EditToolbarButtonVisibilityStateModel), typeof(ToolbarView),
            new UIPropertyMetadata(new PropertyChangedCallback(ButtonStateVisibilityProperty_Changed)));

        public EditToolbarButtonVisibilityStateModel ButtonStateVisibility
        {
            get => (EditToolbarButtonVisibilityStateModel)GetValue(ButtonStateVisibilityProperty);
            set => SetValue(ButtonStateVisibilityProperty, value);
        }

        private static void ButtonStateVisibilityProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ToolbarView).ButtonStateVisibility = (EditToolbarButtonVisibilityStateModel)e.NewValue;
        }
        #endregion

        #region More options three dots
        public static readonly DependencyProperty MoreOptionsMenuInteractionProperty = DependencyProperty.Register(
           nameof(MoreOptionsMenuInteraction), typeof(IMoreMenuInteraction), typeof(ToolbarView), new UIPropertyMetadata(MoreOptionsMenuInteractionProperty_Changed));

        private static void MoreOptionsMenuInteractionProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ToolbarView).MoreOptionsMenuInteraction = (IMoreMenuInteraction)e.NewValue;
        }

        public IMoreMenuInteraction MoreOptionsMenuInteraction
        {
            get => (IMoreMenuInteraction)GetValue(MoreOptionsMenuInteractionProperty);
            set => SetValue(EditToolBarInteractionProperty, value);
        }

        public static readonly DependencyProperty MoreOptionsMenuItemSourceProperty = DependencyProperty
            .Register(nameof(MoreOptionsMenuItemSource), typeof(List<MoreOptionsMenuModel>), typeof(ToolbarView));


        public List<MoreOptionsMenuModel> MoreOptionsMenuItemSource
        {
            get => (List<MoreOptionsMenuModel>)GetValue(MoreOptionsMenuItemSourceProperty);
            set => SetValue(MoreOptionsMenuItemSourceProperty, value);
        }
        #endregion

        #endregion



        public ToolbarView()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
