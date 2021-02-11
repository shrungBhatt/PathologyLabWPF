using System.Windows;
using System.Windows.Controls;


namespace OPMS.Controls
{
    /// <summary>
    /// Interaction logic for EditToolbar.xaml
    /// </summary>
    public partial class EditToolbar : UserControl
    {

        public static readonly DependencyProperty InteractionProperty = DependencyProperty.Register("Interaction",typeof(IEditToolBarInteraction), typeof(EditToolbar), new UIPropertyMetadata(new PropertyChangedCallback(InteractionProperty_Changed)));

        private static void InteractionProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EditToolbar).Interaction = (IEditToolBarInteraction)e.NewValue;
        }

        public IEditToolBarInteraction Interaction
        {
            get => (IEditToolBarInteraction)GetValue(InteractionProperty);
            set => SetValue(InteractionProperty, value);
        }

        public static readonly DependencyProperty ButtonStateProperty = DependencyProperty
            .Register(nameof(ButtonState), typeof(EditToolbarButtonActiveStateModel), typeof(EditToolbar),
            new UIPropertyMetadata(new PropertyChangedCallback(ButtonStateProperty_Changed)));

        public EditToolbarButtonActiveStateModel ButtonState
        {
            get => (EditToolbarButtonActiveStateModel)GetValue(ButtonStateProperty);
            set => SetValue(ButtonStateProperty, value);
        }

        private static void ButtonStateProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EditToolbar).ButtonState = (EditToolbarButtonActiveStateModel)e.NewValue;
        }

        public static readonly DependencyProperty ButtonStateVisibilityProperty = DependencyProperty
            .Register(nameof(ButtonStateVisibility), typeof(EditToolbarButtonVisibilityStateModel), typeof(EditToolbar),
            new UIPropertyMetadata(new PropertyChangedCallback(ButtonStateVisibilityProperty_Changed)));

        public EditToolbarButtonVisibilityStateModel ButtonStateVisibility
        {
            get => (EditToolbarButtonVisibilityStateModel)GetValue(ButtonStateVisibilityProperty);
            set => SetValue(ButtonStateVisibilityProperty, value);
        }

        private static void ButtonStateVisibilityProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EditToolbar).ButtonStateVisibility = (EditToolbarButtonVisibilityStateModel)e.NewValue;
        }

        public EditToolbar()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Interaction.OnCloseButtonClicked();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Interaction.OnDeleteButtonClicked();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Interaction.OnSaveButtonClicked();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Interaction.OnEditButtonClicked();
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            Interaction.OnAddButtonClicked();
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            Interaction.OnLastButtonClicked();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            Interaction.OnNextButtonClicked();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            Interaction.OnPreviousButtonClicked();
        }

        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            Interaction.OnFirstButtonClicked();
        }

    }
}
