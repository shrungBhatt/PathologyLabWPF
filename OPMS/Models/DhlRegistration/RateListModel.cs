using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class RateListModel : BaseEntity
    {
        public HdlRegistration HdlRegistration { get; set; }
        public List<SpecializedLabRateList> SpecializedLabRateLists { get; set; }
        public List<ReferringRateList> ReferredRateLists { get; set; }
        public List<MonthlyRateList> MonthlyRateLists { get; set; }

        public override object Clone()
        {

            var clone = new RateListModel();
            clone.HdlRegistration = this.HdlRegistration;
            clone.SpecializedLabRateLists = new List<SpecializedLabRateList>();
            clone.SpecializedLabRateLists.AddRange(this.SpecializedLabRateLists.ToList());
            clone.MonthlyRateLists = new List<MonthlyRateList>();
            clone.MonthlyRateLists.AddRange(this.MonthlyRateLists.ToList());
            clone.ReferredRateLists = new List<ReferringRateList>();
            clone.ReferredRateLists.AddRange(this.ReferredRateLists);

            return clone;
        }
    }
}
