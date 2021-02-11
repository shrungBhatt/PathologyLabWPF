using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Utility.Attributes
{
    [DisplayName("IgnoreColumn")]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IgnoreColumnAttribute : Attribute
    {
        public IgnoreColumnAttribute()
        {
            
        }
    }
}
