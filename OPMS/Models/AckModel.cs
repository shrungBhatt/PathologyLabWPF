﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Models
{
    public class AckModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }

    }
}
