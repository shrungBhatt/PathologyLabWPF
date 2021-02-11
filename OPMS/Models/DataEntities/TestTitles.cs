using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class TestTitles : BaseEntity
    {
        public TestTitles()
        {
            Abbrevations = new HashSet<Abbrevations>();
            OtherTests = new HashSet<OtherTests>();
        }
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int Charges { get; set; }
        public int? OrderBy { get; set; }
        public string HeaderNote { get; set; }
        public string FooterNote { get; set; }
        public bool WordFormatResult { get; set; }
        public bool ShowNormalRangeHeader { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
            }
        }

        public virtual TestGroups Group { get; set; }

        [JsonIgnore]
        public virtual ICollection<Abbrevations> Abbrevations { get; set; }

        [JsonIgnore]
        public virtual ICollection<TestResults> TestResults { get; set; }

        [JsonIgnore]
        public virtual ICollection<OtherTests> OtherTests { get; set; }
    }
}
