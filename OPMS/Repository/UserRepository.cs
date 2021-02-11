using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Repository
{
    public class UserRepository
    {
        public Users CurrentUser { get; set; }
        public string UserName { get => CurrentUser?.UserName; }
        public DateTime LoginTime { get; set; }

    }
}
