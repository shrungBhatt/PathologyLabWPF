using OPMS.Models;
using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Test
{
    public class OtherTestDto : ModelBase
    {
        [IgnoreColumn]
        public int Id { get; set; }
        [IgnoreColumn]
        public int TestGroupId { get; set; }
        [IgnoreColumn]
        public int TestTitleId { get; set; }
        public string Name { get; set; }
        public int OrderBy { get; set; }
        public string Unit { get; set; }
        public bool DisplayInTestResult { get; set; }
        public bool DisplayInBoldFontInReport { get; set; }
        public bool DescriptiveResult { get; set; }
        public string ValMale { get; set; }
        public string ValFemale { get; set; }
        public string ValNoenatal { get; set; }
        public string ValChild { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string DefaultValue { get; set; }
        public string Options { get; set; }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
