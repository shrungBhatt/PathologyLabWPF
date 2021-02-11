using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Response
{
    public class DealersResponseModel : ModelBase
    {
        public List<Dealers> Dealers { get; set; }
    }
}
