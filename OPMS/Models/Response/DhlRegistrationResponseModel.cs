using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Response
{
    public class DhlRegistrationResponseModel : ModelBase
    {
        public List<HdlRegistration> HdlRegistrations { get; set; }
        public List<RegistrationCategories> Categories { get; set; }
        public List<RegistrationTypes> RegistrationTypes { get; set; }
    }
}
