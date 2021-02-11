using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Models
{
    public class ResponseModel
    {

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }

    }
}
