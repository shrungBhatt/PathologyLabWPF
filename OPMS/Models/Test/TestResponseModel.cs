using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Test
{
    public class TestResponseModel
    {

        public List<TestGroupDto> TestGroups { get; set; }
        public List<TestTitleDto> TestTitles { get; set; }
        public List<OtherTestDto> OtherTests { get; set; }
        public List<TestComboListTypeDto> Groups { get; set; }
        public List<TestComboListTypeDto> Titles { get; set; }

    }
}
