using OPMS.Models.Patient;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class PatientResponseModel
    {
        public List<PatientDto> PatientDetails { get; set; }
        public List<TestGroups> TestGroups { get; set; }
        public List<TestTitles> TestTitles { get; set; }
        public List<Initials> Initials { get; set; }
        public List<Genders> Genders { get; set; }
        public List<TestResults> TestResults { get; set; }
        public List<RegistrationTypes> RegistrationTypes { get; set; }
        public List<HdlRegistration> HdlRegistrations { get; set; }
        public List<FieldOptions> FieldOptions { get; set; }

    }
}
