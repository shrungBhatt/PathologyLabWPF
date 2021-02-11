using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class OtherTests : BaseEntity
    {
        public int Id { get; set; }
        public int TestGroupId { get; set; }
        public int TestTitleId { get; set; }
        public string Name { get; set; }
        public int OrderBy { get; set; }
        public string Unit { get; set; }
        public bool? DisplayInTestResult { get; set; }
        public bool? DisplayInBoldFontInReport { get; set; }
        public bool? DescriptiveResult { get; set; }
        public string ValMale { get; set; }
        public string ValFemale { get; set; }
        public string ValNoenatal { get; set; }
        public string ValChild { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string DefaultValue { get; set; }
        public string Options { get; set; }

        [JsonIgnore]
        public virtual TestGroups TestGroup { get; set; }

        [JsonIgnore]
        public virtual ICollection<TestResults> TestResults { get; set; }

        [JsonIgnore]
        public virtual TestTitles TestTitle { get; set; }
    }
}
