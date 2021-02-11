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

namespace OPMS.Controls.MoreOptionsMenu
{
    /// <summary>
    /// Interaction logic for MoreOptionsMenu.xaml
    /// </summary>
    public partial class MoreOptionsMenu : UserControl
    {

        #region Bindable Properties
        public static readonly DependencyProperty MoreOptionsMenuInteractionProperty = DependencyProperty.Register(
            nameof(MoreOptionsMenuInteraction), typeof(IMoreMenuInteraction), typeof(MoreOptionsMenu), new UIPropertyMetadata(MoreOptionsMenuInteractionProperty_CallBack));

        private static void MoreOptionsMenuInteractionProperty_CallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MoreOptionsMenu).MoreOptionsMenuInteraction = (IMoreMenuInteraction)e.NewValue;
        }

        public IMoreMenuInteraction MoreOptionsMenuInteraction
        {
            get => (IMoreMenuInteraction)GetValue(MoreOptionsMenuInteractionProperty);
            set => SetValue(MoreOptionsMenuInteractionProperty, value);
        }

        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register(
            nameof(ItemSource), typeof(List<MoreOptionsMenuModel>), typeof(MoreOptionsMenu), new FrameworkPropertyMetadata(new PropertyChangedCallback(ItemSourcePropertyChanged)));

        private static void ItemSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MoreOptionsMenu).ItemSource = (List<MoreOptionsMenuModel>)e.NewValue;
            (d as MoreOptionsMenu).InitView();
        }

        public List<MoreOptionsMenuModel> ItemSource
        {
            get => (List<MoreOptionsMenuModel>)GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }
        #endregion

        private void InitView()
        {

            if (ItemSource != null && ItemSource.Count > 0)
            {
                foreach (var item in ItemSource)
                {
                    var button = GetButton(item.Title, item.Id.ToString());
                    button.Click += Button_Click;
                    ParentStack.Children.Add(button);
                }

            }


            if (ParentStack.Children.Count > 0)
            {
                ParentStack.Children.Add(new Separator());
            }

            var exitButton = GetButton("Exit","exit");
            exitButton.Click += ExitButton_Click;
            ParentStack.Children.Add(exitButton);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button)
            {
                MoreOptionsMenuInteraction.OnMenuButtonClicked((sender as Button).Name);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MoreOptionsMenuInteraction.OnExitButtonClicked();
        }

        private Button GetButton(string text, string id)
        {
            var button = new Button();
            button.Content = text;
            button.Name = id;
            button.Background = (Brush)new BrushConverter().ConvertFromString("#00FFFFFF");
            button.Foreground = (Brush)new BrushConverter().ConvertFromString("#000000");

            return button;
        }

        public MoreOptionsMenu()
        {
            InitializeComponent();
            DataContext = this;
        }
    }

    public class MoreOptionsMenuModel
    {
        public const string ONE = "one";
        public const string TWO = "two";
        public const string THREE = "three";
        public const string FOUR = "four";
        public const string FIVE = "five";
        public const string Refresh = "refresh";

        public string Id { get; set; }
        public string Title { get; set; }
    }
}
