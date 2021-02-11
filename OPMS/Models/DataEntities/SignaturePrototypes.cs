using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class SignaturePrototypes : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public string Degree { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
