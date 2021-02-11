using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPMS.Controls
{
    public class NavToolbarButtonVisibilityStateModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Nav toolbar button visibility properties

        private Visibility forward = Visibility.Visible;
        public Visibility Forward
        {
            get => forward;
            set
            {
                forward = value;
                RaisePropertyChanged(nameof(Forward));
            }
        }

        private Visibility back = Visibility.Visible;
        public Visibility Back
        {
            get => back;
            set
            {
                back = value;
                RaisePropertyChanged(nameof(Back));
            }
        }
        #endregion
    }
}
