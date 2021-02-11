using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Response
{
    public class AbbrevationsResponseModel : ModelBase
    {
        public List<Abbrevations> Abbrevations { get; set; }
        public List<OtherTests> OtherTests { get; set; }
    }
}
