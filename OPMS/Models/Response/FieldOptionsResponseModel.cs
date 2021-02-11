using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Response
{
    public class FieldOptionsResponseModel : ModelBase
    {
        public List<FieldOptions> FieldOptions { get; set; }
        public List<Fields> Fields { get; set; }
    }
}
