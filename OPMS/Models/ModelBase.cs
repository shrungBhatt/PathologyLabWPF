using Newtonsoft.Json;
using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Models
{
    public class ModelBase : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        [IgnoreColumn]
        [JsonProperty("acknowledgement")]
        public AckModel Acknowledgement { get; set; }

        [IgnoreColumn]
        [JsonProperty("error")]
        public ErrorModel Error { get; set; }
    }
}
