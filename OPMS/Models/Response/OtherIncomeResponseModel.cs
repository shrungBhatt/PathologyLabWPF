using OPMS.Models;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class OtherIncomeResponseModel : ModelBase
    {
        public List<AccountHead> AccountHeads { get; set; }
        public List<OtherIncome> OtherIncomes { get; set; }
    }
}
