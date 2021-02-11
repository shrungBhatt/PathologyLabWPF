using OPMS.Models.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace OPMS.Behaviours
{
    public class BindableSelectedItemBehavior : Behavior<TreeView>
    {
        #region SelectedItem Property

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(BindableSelectedItemBehavior), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

            var treeView = (sender as BindableSelectedItemBehavior).AssociatedObject;
            if(treeView != null)
            {
                foreach (var item in treeView.ItemContainerGenerator.Items)
                {
                    var dto = item as OPSMBackend.DataEntities.TestTitles;
                    var selectedItem = treeView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                    if(selectedItem != null)
                    {
                        if (e.NewValue != null && dto.Id == (e.NewValue as OPSMBackend.DataEntities.TestTitles).Id)
                        {
                            selectedItem.SetValue(TreeViewItem.IsSelectedProperty, true);
                            selectedItem.Background = new SolidColorBrush(Colors.Orange);

                        }
                        else
                        {
                            selectedItem.SetValue(TreeViewItem.IsSelectedProperty, false);
                            selectedItem.Background = new SolidColorBrush(Colors.White);

                        }
                    }
                    
                }
            }
            
        }

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
            }
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.SelectedItem = e.NewValue;
        }
    }
}
