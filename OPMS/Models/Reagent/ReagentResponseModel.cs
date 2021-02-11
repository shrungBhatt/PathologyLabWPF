using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models
{
    public class ReagentResponseModel
    {
        public List<Reagents> Reagents { get; set; }
        public List<TestReagentRelation> TestReagentRelations { get; set; }
        public List<OtherTests> Tests { get; set; }

    }
}
