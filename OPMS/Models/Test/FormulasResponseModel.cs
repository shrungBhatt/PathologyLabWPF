using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Test
{
    public class FormulasResponseModel
    {
        public List<Formulas> Formulas { get; set; }
        public List<TestGroups> Groups { get; set; }
        public List<TestTitles> Titles { get; set; }
        public List<OtherTests> OtherTests { get; set; }

    }
}
