using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Controls
{
    public class NavToolbarButtonActiveStateModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Edit toolbar button enabling properties
        

        private bool forward = true;
        public bool Forward
        {
            get => forward;
            set
            {
                forward = value;
                RaisePropertyChanged(nameof(Forward));
            }
        }

        private bool back = true;
        public bool Back
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
