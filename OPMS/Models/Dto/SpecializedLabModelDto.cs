﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPMS.Models.Dto
{
    public class SpecializedLabModelDto
    {

        #region Lab details
        public int OutGoingLabId { get; set; }
        public string OutGoingLabName { get; set; }
        #endregion

        #region Patient details
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientCode { get; set; }
        public DateTime RegistrationDate { get; set; }
        #endregion

        #region Test details
        public int TestTitleId { get; set; }
        public int TestGroupId { get; set; }
        public string TestName { get; set; }
        public float OriginalCharge { get; set; }
        public float LabCharge { get; set; }
        #endregion

        public float PaymentToLabForPatient { get; set; }

    }
}
