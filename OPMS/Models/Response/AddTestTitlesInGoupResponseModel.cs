using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Response
{
    public class AddTestTitlesInGoupResponseModel : ModelBase
    {
        public int SelectedGroupId { get; set; }
        public List<TestTitles> TestTitles { get; set; } = new List<TestTitles>();
        public List<TestTitles> SelectedTestTitles { get; set; } = new List<TestTitles>();
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
