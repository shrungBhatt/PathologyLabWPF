using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPMS.Controls
{
    public class EditToolbarButtonVisibilityStateModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Edit toolbar button visibility properties
        private Visibility close = Visibility.Visible;
        public Visibility Close
        {
            get => close;
            set
            {
                close = value;
                RaisePropertyChanged(nameof(Close));
            }
        }

        private Visibility delete = Visibility.Visible;
        public Visibility Delete
        {
            get => delete;
            set
            {
                delete = value;
                RaisePropertyChanged(nameof(Delete));
            }
        }

        private Visibility save = Visibility.Visible;
        public Visibility Save
        {
            get => save;
            set
            {
                save = value;
                RaisePropertyChanged(nameof(Save));
            }
        }

        private Visibility edit = Visibility.Visible;
        public Visibility Edit
        {
            get => edit;
            set
            {
                edit = value;
                RaisePropertyChanged(nameof(Edit));
            }
        }

        private Visibility add = Visibility.Visible;
        public Visibility Add
        {
            get => add;
            set
            {
                add = value;
                RaisePropertyChanged(nameof(Add));
            }
        }

        private Visibility last = Visibility.Visible;
        public Visibility Last
        {
            get => last;
            set
            {
                last = value;
                RaisePropertyChanged(nameof(Last));
            }
        }

        private Visibility next = Visibility.Visible;
        public Visibility Next
        {
            get => next;
            set
            {
                next = value;
                RaisePropertyChanged(nameof(Next));
            }
        }

        private Visibility previous = Visibility.Visible;
        public Visibility Previous
        {
            get => previous;
            set
            {
                previous = value;
                RaisePropertyChanged(nameof(Previous));
            }
        }

        private Visibility first = Visibility.Visible;
        public Visibility First
        {
            get => first;
            set
            {
                first = value;
                RaisePropertyChanged(nameof(First));
            }
        }
        #endregion

        /// <summary>
        /// Show only edit, save and cancel buttons
        /// </summary>
        public void FlavourOne()
        {
            Previous = Visibility.Collapsed;
            First = Visibility.Collapsed;
            Next = Visibility.Collapsed;
            Last = Visibility.Collapsed;
            Add = Visibility.Collapsed;
            Delete = Visibility.Collapsed;           
        }

        /// <summary>
        /// Show the add, edit, save, delete and cancel buttons
        /// </summary>
        public void FlavourTwo()
        {
            Previous = Visibility.Collapsed;
            First = Visibility.Collapsed;
            Next = Visibility.Collapsed;
            Last = Visibility.Collapsed;
        }

        /// <summary>
        /// Hide the add and delete buttons
        /// </summary>
        public void FlavourThree()
        {
            Add = Visibility.Collapsed;
            Delete = Visibility.Collapsed;
        }
    }
}
