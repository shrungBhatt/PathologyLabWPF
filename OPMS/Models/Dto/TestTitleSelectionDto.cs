using OPMS.Models.Test;
using OPMS.Utility.Attributes;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Models.Dto
{
    public class TestTitleSelectionDto : ModelBase
    {
        [IgnoreColumn]
        public int Id { get; set; }
        [IgnoreColumn]
        public int GroupId { get; set; }

        bool select;
        public bool Select
        {
            get => select;
            set
            {
                select = value;
                RaisePropertyChanged(nameof(Select));
            }
        }

        public string TestTitle { get; set; }

        public string TestGroup { get; set; }
        [IgnoreColumn]
        public int OrderBy { get; set; }
        [IgnoreColumn]
        public int Charges { get; set; }

        [IgnoreColumn]
        public string HeaderNote
        {
            get; set;
        }
        [IgnoreColumn]
        public string FooterNote
        {
            get; set;
        }
        [IgnoreColumn]
        public bool WordFormatResult { get; set; }
        [IgnoreColumn]
        public bool ShowNormalRangeHeader { get; set; }
        [IgnoreColumn]
        public string ModifiedBy { get; set; }
        [IgnoreColumn]
        public DateTime ModifiedDate { get; set; }


        public static TestTitleSelectionDto GetDto(TestTitles testTitle)
        {
            return new TestTitleSelectionDto
            {
                Id = testTitle.Id,
                GroupId = testTitle.GroupId,
                Charges = testTitle.Charges,
                FooterNote = testTitle.FooterNote,
                HeaderNote = testTitle.HeaderNote,
                TestTitle = testTitle.Name,
                ShowNormalRangeHeader = testTitle.ShowNormalRangeHeader,
                WordFormatResult = testTitle.WordFormatResult,
                TestGroup = testTitle.Group?.Name,
                OrderBy = (int)testTitle.OrderBy
            };
        }

    }
}
