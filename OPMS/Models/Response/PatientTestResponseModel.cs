using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Response
{
    public class PatientTestResponseModel
    {
        public int PatientId { get; set; }
        public List<TestResultsView> TestResults { get; set; }
    }
}
