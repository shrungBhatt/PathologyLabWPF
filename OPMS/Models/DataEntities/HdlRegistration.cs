using Newtonsoft.Json;
using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class HdlRegistration : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [IgnoreColumn]
        public int RegistrationTypeId { get; set; }
        [IgnoreColumn]
        public int RegistrationCategoryId { get; set; }

        public string Type
        {
            get => RegistrationType?.RegistrationType;
        }

        public string Category
        {
            get => RegistrationCategory?.RegistrationCategory;
        }

        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string AdditionalNotes { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        [IgnoreColumn]
        public HdlBill Bill { get; set; }

        [IgnoreColumn]
        public virtual RegistrationCategories RegistrationCategory { get; set; }

        [IgnoreColumn]
        public virtual RegistrationTypes RegistrationType { get; set; }
    }
}
