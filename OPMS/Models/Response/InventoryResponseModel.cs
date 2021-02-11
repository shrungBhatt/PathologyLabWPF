using OPMS.Models;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class InventoryResponseModel : ModelBase
    {
        public List<Inventories> Inventories { get; set; }
    }
}
