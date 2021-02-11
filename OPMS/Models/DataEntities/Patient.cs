using Newtonsoft.Json;
using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Patient : BaseEntity
    {
        [IgnoreColumn]
        public int Id { get; set; }
        public string PatientCode { get; set; }
        [IgnoreColumn]
        public int? InitialId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? AgeInYears { get; set; }
        public int? AgeInMonths { get; set; }
        public int? AgeInDays { get; set; }
        [IgnoreColumn]
        [JsonProperty("Gender")]
        public int? GenderId { get; set; }
        [JsonIgnore]
        public string Gender { get => GenderNavigation?.Gender; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        [IgnoreColumn]
        public string SimpleRegistrationDate { get => RegistrationDate.ToString("dd-MMM-yyyy"); }

        [IgnoreColumn]
        [JsonIgnore]
        public string ReportDate { get => DateTime.Now.ToString("dd-MMM-yyyy"); }

        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        [IgnoreColumn]
        [JsonProperty("CivilStatus")]
        public int? CivilStatusId { get; set; }
        [JsonIgnore]
        public string CivilStatus { get => CivilStatusNavigation?.CivilStatus; }
        [IgnoreColumn]
        [JsonProperty("Occupation")]
        public int? OccupationId { get; set; }
        public string ModifiedBy { get; set; } = "";
        public DateTime ModifiedDate { get; set; }
        [IgnoreColumn]
        public int? ReferredBy { get; set; }
        public string ReferredByName { get => ReferredByNavigation?.Name; }
        public string Address { get; set; }

        [IgnoreColumn]
        public bool IsFinished { get; set; }

        [JsonIgnore]
        private List<TestResultsView> testResults;

        [IgnoreColumn]
        public List<TestResultsView> TestResults
        {
            get => testResults;
            set
            {
                testResults = value;
                RaisePropertyChanged(nameof(TestResults));
            }
        }

        [IgnoreColumn]
        public PatientBill Bill { get; set; }

        [IgnoreColumn]
        public List<TestTitles> TestTitles { get; set; }

        [IgnoreColumn]
        public virtual CivilStatuses CivilStatusNavigation { get; set; }

        [IgnoreColumn]
        public virtual Genders GenderNavigation { get; set; }

        [IgnoreColumn]
        public virtual Initials Initial { get; set; }

        [IgnoreColumn]
        public virtual FieldOptions OccupationNavigation { get; set; }

        [IgnoreColumn]
        public virtual ICollection<TestResults> TestResultsCollection { get; set; }

        [IgnoreColumn]
        public HdlRegistration ReferredByNavigation { get; set; }

        [IgnoreColumn]
        [JsonIgnore]
        public bool ShowViewBillButton { get => Bill != null; }

        [IgnoreColumn]
        [JsonIgnore]
        public bool ShowReceiptButton { get => Bill != null && !Bill.IsAmountPaid; }

        [IgnoreColumn]
        public string PatientFullName { get; set; }
        [IgnoreColumn]
        public string ReferredByFullName { get; set; }
    }
}
