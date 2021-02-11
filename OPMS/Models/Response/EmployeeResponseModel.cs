using OPMS.Models;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class EmployeeResponseModel : ModelBase
    {
        public List<Employees> Employees { get; set; }
        public List<Genders> Genders { get; set; }
        public List<FieldOptions> CivilStatuses { get; set; }
        public List<EmployeeCategories> EmployeeCategories { get; set; }
        public List<EmployeeRoles> EmployeeRoles { get; set; }
    }
}
