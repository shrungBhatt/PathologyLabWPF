using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Controls
{
    public class EditToolbarButtonActiveStateModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Edit toolbar button enabling properties
        private bool close = false;
        public bool Close
        {
            get => close;
            set
            {
                close = value;
                RaisePropertyChanged(nameof(Close));
            }
        }

        private bool delete = false;
        public bool Delete
        {
            get => delete;
            set
            {
                delete = value;
                RaisePropertyChanged(nameof(Delete));
            }
        }

        private bool save = false;
        public bool Save
        {
            get => save;
            set
            {
                save = value;
                RaisePropertyChanged(nameof(Save));
            }
        }

        private bool edit = true;
        public bool Edit
        {
            get => edit;
            set
            {
                edit = value;
                RaisePropertyChanged(nameof(Edit));
            }
        }

        private bool add = true;
        public bool Add
        {
            get => add;
            set
            {
                add = value;
                RaisePropertyChanged(nameof(Add));
            }
        }

        private bool last = true;
        public bool Last
        {
            get => last;
            set
            {
                last = value;
                RaisePropertyChanged(nameof(Last));
            }
        }

        private bool next = true;
        public bool Next
        {
            get => next;
            set
            {
                next = value;
                RaisePropertyChanged(nameof(Next));
            }
        }

        private bool previous = true;
        public bool Previous
        {
            get => previous;
            set
            {
                previous = value;
                RaisePropertyChanged(nameof(Previous));
            }
        }

        private bool first = true;
        public bool First
        {
            get => first;
            set
            {
                first = value;
                RaisePropertyChanged(nameof(First));
            }
        }
        #endregion

        private EditToolbarMode editToolbarMode = EditToolbarMode.IDLE;
        public EditToolbarMode EditToolbarMode
        {
            get => editToolbarMode;
            set
            {
                editToolbarMode = value;
                RaisePropertyChanged(nameof(EditToolbarMode));
            }
        }

        public void TurnOnEditMode()
        {
            First = false;
            Last = false;
            Next = false;
            Previous = false;
            Close = true;
            Edit = false;
            Add = true;
            Save = true;
            Delete = true;

            EditToolbarMode = EditToolbarMode.EDIT;
        }

        public void TurnOnAddMode()
        {
            First = false;
            Last = false;
            Next = false;
            Previous = false;
            Close = true;
            Edit = false;
            Add = false;
            Save = true;
            Delete = false;

            EditToolbarMode = EditToolbarMode.ADD;

        }

        public void TurnOffEditMode()
        {
            First = true;
            Last = true;
            Next = true;
            Previous = true;
            Edit = true;
            Close = false;
            Add = true;
            Save = false;
            Delete = false;

            EditToolbarMode = EditToolbarMode.IDLE;
        }

    }

    public enum EditToolbarMode
    {
        EDIT,
        IDLE,
        ADD
    }
}
