using OPMS.Models;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class SignatureResponseModel : ModelBase
    {
        public List<SignaturePrototypes> Signatures { get; set; }
    }
}
