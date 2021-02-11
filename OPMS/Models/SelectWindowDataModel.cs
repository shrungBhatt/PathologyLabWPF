using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Models
{
    public class SelectWindowDataModel<T> : ModelBase
    {
        public string WindowTitle { get; set; }

        List<T> itemList;
        public List<T> ItemList
        {
            get => itemList;
            set
            {
                itemList = value;
                RaisePropertyChanged(nameof(itemList));
            }
        }
    }
}
