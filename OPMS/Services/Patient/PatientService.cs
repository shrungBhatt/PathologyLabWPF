using OPMS.Models.Dto;
using OPMS.Models.Patient;
using OPMS.Models.Request;
using OPMS.Models.Response;
using OPMS.Services.RequestProvider;
using OPMS.Utility;
using OPSMBackend.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Services.Patient
{
    public class PatientService : IPatientService
    {
        private readonly IRequestProvider requestProvider;

        public PatientService(IRequestProvider requestProvider)
        {
            this.requestProvider = requestProvider;
        }

        public async Task<PatientDto> DeletePatient(PatientDto patient)
        {
            var url = URLBuilder.GetURL(Controllers.PATIENT, EndPoint.PATIENT_DELETE);

            return await requestProvider.DeleteAsync(url, patient, new Dictionary<string, string> { ["id"] = patient.Patient.Id.ToString() });
        }

        public async Task<PatientResponseModel> GetAllPatients()
        {
            var url = URLBuilder.GetURL(Controllers.PATIENT, EndPoint.PATIENT_LIST);

            return await requestProvider.GetAsync<PatientResponseModel>(url);
        }

        public async Task<TestResultsResponseModel> GetAllPatientWithTests()
        {
            var url = URLBuilder.GetURL(Controllers.PATIENT, EndPoint.PATIENT_LIST_TESTS);

            return await requestProvider.GetAsync<TestResultsResponseModel>(url);
        }

        public async Task<PatientTestResponseModel> GetPatientWithTests(PatientTestResponseModel model)
        {
            var url = URLBuilder.GetURL(Controllers.PATIENT, EndPoint.PATIENT_LIST_TESTS_ID);

            return await requestProvider.PostAsync(url, model);
        }

        public async Task<HdlReferringCutReportResponseModel> GetHdlReferringCutPaymentReport(HdlReferringCutReportResponseModel model)
        {
            var url = URLBuilder.GetURL(Controllers.PATIENT, EndPoint.PATIENT_GET_HDL_REF_PAYMENT_REPORT);

            return await requestProvider.PostAsync(url, model);
        }

        public async Task<BillResponseModel> GetPatientBills()
        {
            var url = URLBuilder.GetURL(Controllers.PATIENT, EndPoint.PATIENT_BILL_GET);
            return await requestProvider.GetAsync<BillResponseModel>(url);
        }

        public async Task<PatientWithRateListChargesRequest> GetPatientWithRateListCharges(PatientWithRateListChargesRequest model)
        {
            var url = URLBuilder.GetURL(Controllers.PATIENT, EndPoint.PATIENT_GET_WITH_RATE_LIST_CHARGES);

            return await requestProvider.PostAsync(url, model);
        }

        public async Task<SpecializedLabReportResponseModel> GetSpecializedLabReport(SpecializedLabReportResponseModel model)
        {
            var url = URLBuilder.GetURL(Controllers.PATIENT, EndPoint.PATIENT_GET_SPECIALIZED_LAB_SAMPLES_REPORT);

            return await requestProvider.PostAsync(url, model);
        }

        public async Task<SpecializedLabSampleDto> GetSpecializedLabSamples(SpecializedLabSampleDto model)
        {
            var url = URLBuilder.GetURL(Controllers.PATIENT, EndPoint.PATIENT_GET_SPECIALIZED_LAB_SAMPLES);

            return await requestProvider.PostAsync(url, model);
        }

        public async Task<PatientDto> InsertPatient(PatientDto patient)
        {
            var url = URLBuilder.GetURL(Controllers.PATIENT, EndPoint.PATIENT_ADD);

            return await requestProvider.PostAsync(url, patient);
        }

        public async Task<PatientDto> UpdatePatient(PatientDto patient)
        {
            var url = URLBuilder.GetURL(Controllers.PATIENT, EndPoint.PATIENT_UPDATE);

            return await requestProvider.PutAsync(url, patient);
        }

        public async Task<OPSMBackend.DataEntities.Patient> UpdatePatientTestResults(OPSMBackend.DataEntities.Patient patient)
        {
            var url = URLBuilder.GetURL(Controllers.PATIENT, EndPoint.PATIENT_UPDATE_TEST_RESULTS);

            return await requestProvider.PutAsync(url, patient);
        }

        public async Task<SpecializedLabSampleDto> UpdateSpecializedSamples(SpecializedLabSampleDto model)
        {
            var url = URLBuilder.GetURL(Controllers.PATIENT, EndPoint.PATIENT_UPDATE_SPECIALIZED_LAB_SAMPLES);

            return await requestProvider.PutAsync(url, model);
        }
    }
}
