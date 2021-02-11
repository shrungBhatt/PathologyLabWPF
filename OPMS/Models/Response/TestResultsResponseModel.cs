using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Models.Response
{
    public class TestResultsResponseModel : ModelBase
    {
        public List<OPSMBackend.DataEntities.Patient> Patients { get; set; }
        public List<SignaturePrototypes> Signatures { get; set; }
    }
}
