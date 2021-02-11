using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Response
{
    public class HdlBillResponseModel : ModelBase
    {
        public List<HdlRegistration> HdlRegistrations { get; set; } = new List<HdlRegistration>();
    }
}
