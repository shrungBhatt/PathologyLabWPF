using OPMS.Models.Dto;
using OPMS.Models.Patient;
using OPMS.Models.Request;
using OPMS.Models.Response;
using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patients = OPSMBackend.DataEntities.Patient;

namespace OPMS.Services.Patient
{
    public interface IPatientService
    {
        Task<PatientResponseModel> GetAllPatients();
        Task<TestResultsResponseModel> GetAllPatientWithTests();
        Task<PatientTestResponseModel> GetPatientWithTests(PatientTestResponseModel model);
        Task<PatientDto> UpdatePatient(PatientDto patient);
        Task<Patients> UpdatePatientTestResults(Patients patient);
        Task<PatientDto> InsertPatient(PatientDto patient);
        Task<PatientDto> DeletePatient(PatientDto patient);
        Task<BillResponseModel> GetPatientBills();
        Task<PatientWithRateListChargesRequest> GetPatientWithRateListCharges(PatientWithRateListChargesRequest model);

        Task<SpecializedLabSampleDto> GetSpecializedLabSamples(SpecializedLabSampleDto model);
        Task<SpecializedLabSampleDto> UpdateSpecializedSamples(SpecializedLabSampleDto model);

        Task<SpecializedLabReportResponseModel> GetSpecializedLabReport(SpecializedLabReportResponseModel model);
        Task<HdlReferringCutReportResponseModel> GetHdlReferringCutPaymentReport(HdlReferringCutReportResponseModel model);
    }
}
