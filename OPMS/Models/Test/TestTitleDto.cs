using OPMS.Models;
using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Test
{
    public class TestTitleDto : ModelBase
    {
        [IgnoreColumn]
        public int Id { get; set; }
        [IgnoreColumn]
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int OrderBy { get; set; }
        public int Charges { get; set; }
        string headerNote;

        [IgnoreColumn]
        public string HeaderNote
        {
            get => headerNote;
            set
            {
                headerNote = value;
                RaisePropertyChanged(nameof(HeaderNote));
            }
        }
        string footerNote;
        [IgnoreColumn]
        public string FooterNote
        {
            get => footerNote;
            set
            {
                footerNote = value;
                RaisePropertyChanged(nameof(FooterNote));
            }
        }
        public bool WordFormatResult { get; set; }
        public bool ShowNormalRangeHeader { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
