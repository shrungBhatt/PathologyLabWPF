using OPMS.Models;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class BillResponseModel : ModelBase
    {
        public List<Patient> Patients { get; set; }
    }
}
