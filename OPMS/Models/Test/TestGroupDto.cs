using OPMS.Models;
using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Test
{
    public class TestGroupDto : ModelBase
    {
        [IgnoreColumn]
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public string Notes { get; set; }
        public int OrderNo { get; set; }
        public bool ShowTitleInReports { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
