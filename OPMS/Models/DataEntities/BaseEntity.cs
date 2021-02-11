using Newtonsoft.Json;
using OPMS.Models;
using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.DataEntities
{
    public class BaseEntity : ModelBase
    {
        [JsonIgnore]
        [IgnoreColumn]
        public int Id { get; set; }
    }
}
