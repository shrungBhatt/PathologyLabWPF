using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class TestResults : BaseEntity
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int TitleId { get; set; }
        public int TestId { get; set; }
        public string TestResult { get; set; }
        public string LargeTestResult { get; set; }
        public int? PatientId { get; set; }
        public string PatientCode { get; set; }

        [JsonIgnore]
        public virtual Patient Patient { get; set; }

        [JsonIgnore]
        public virtual TestGroups Group { get; set; }

        [JsonIgnore]
        public virtual OtherTests Test { get; set; }

        [JsonIgnore]
        public virtual TestTitles Title { get; set; }

        public TestResults GetTestResult(OtherTests otherTests, Patient patient)
        {
            var result = new TestResults();
            result.GroupId = otherTests.TestGroupId;
            result.TitleId = otherTests.TestTitleId;
            result.TestId = otherTests.Id;
            result.PatientId = patient.Id;
            result.PatientCode = patient.PatientCode;

            return result;
        }
    }
}
