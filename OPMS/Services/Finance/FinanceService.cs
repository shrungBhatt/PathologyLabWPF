using OPMS.Models.Response;
using OPMS.Services.RequestProvider;
using OPMS.Utility;
using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPMS.Services.Finance
{
    public class FinanceService : IFinanceService
    {
        readonly IRequestProvider requestProvider;

        public FinanceService(IRequestProvider requestProvider)
        {
            this.requestProvider = requestProvider;
        }
        public async Task<Expenses> DeleteExpense(Expenses expense)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_EXPENSES_DELETE);
            return await requestProvider.DeleteAsync(url, expense, new Dictionary<string, string> { ["id"] = expense.Id.ToString() });
        }

        public async Task<HdlBill> DeleteHdlBill(HdlBill bill)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_HDL_BILL_DELETE);
            return await requestProvider.DeleteAsync(url, bill, new Dictionary<string, string> { ["id"] = bill.Id.ToString() });
        }

        public async Task<HdlBillPayment> DeleteHdlBillPayment(HdlBillPayment hdlBillPayment)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_HDL_BILL_PAYMENT_DELETE);
            return await requestProvider.DeleteAsync(url, hdlBillPayment, new Dictionary<string, string> { ["id"] = hdlBillPayment.Id.ToString() });
        }

        public async Task<OtherIncome> DeleteOtherIncome(OtherIncome otherIncome)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_OTHER_INCOME_DELETE);
            return await requestProvider.DeleteAsync(url, otherIncome, new Dictionary<string, string> { ["id"] = otherIncome.Id.ToString() });
        }

        public async Task<PatientBill> DeletePatientBill(PatientBill patientBill)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_PATIENT_BILL_DELETE);
            return await requestProvider.DeleteAsync(url, patientBill, new Dictionary<string, string> { ["id"] = patientBill.Id.ToString() });
        }

        public async Task<PatientBillPayment> DeletePatientBillPayment(PatientBillPayment patientBillPayment)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_PATIENT_BILL_PAYMENT_DELETE);
            return await requestProvider.DeleteAsync(url, patientBillPayment, new Dictionary<string, string> { ["id"] = patientBillPayment.Id.ToString() });
        }

        public async Task<PatientBillPaymentResponseModel> GetBillPayments()
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_PATIENT_BILL_PAYMENT_GET);
            return await requestProvider.GetAsync<PatientBillPaymentResponseModel>(url);
        }

        public async Task<ExpensesResponseModel> GetExpenses()
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_EXPENSES_GET);
            return await requestProvider.GetAsync<ExpensesResponseModel>(url);
        }

        public async Task<HdlBillPaymentResponseModel> GetHdlBillPayments()
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_HDL_BILL_PAYMENT_GET);
            return await requestProvider.GetAsync<HdlBillPaymentResponseModel>(url);
        }

        public async Task<HdlBillResponseModel> GetHdlBills()
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_HDL_BILL_GET);
            return await requestProvider.GetAsync<HdlBillResponseModel>(url);
        }

        public async Task<OtherIncomeResponseModel> GetOtherIncomes()
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_OTHER_INCOME_GET);
            return await requestProvider.GetAsync<OtherIncomeResponseModel>(url);
        }

        public async Task<PatientBillPaymentReportResponeModel> GetPatientBillPaymentReport(PatientBillPaymentReportResponeModel model)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_PATIENT_BILL_PAYMENT_REPORT);
            return await requestProvider.PostAsync(url, model);
        }

        public async Task<PatientRevenueReportResponseModel> GetPatientRevenueResponseModel(PatientRevenueReportResponseModel model)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_PATIENT_REVENUE_REPORT);
            return await requestProvider.PostAsync(url, model);
        }

        public async Task<SalaryPaymentResponseModel> GetSalaryPayment()
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_SALARY_PAYMENT_GET);
            return await requestProvider.GetAsync<SalaryPaymentResponseModel>(url);
        }

        public async Task<Expenses> InsertExpense(Expenses expense)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_EXPENSES_INSERT);
            return await requestProvider.PostAsync(url, expense);
        }

        public async Task<HdlBill> InsertHdlBill(HdlBill bill)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_HDL_BILL_INSERT);
            return await requestProvider.PostAsync(url, bill);
        }

        public async Task<HdlBillPayment> InsertHdlBillPayment(HdlBillPayment hdlBillPayment)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_HDL_BILL_PAYMENT_INSERT);
            return await requestProvider.PostAsync(url, hdlBillPayment);
        }

        public async Task<OtherIncome> InsertOtherIncome(OtherIncome otherIncome)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_OTHER_INCOME_INSERT);
            return await requestProvider.PostAsync(url, otherIncome);
        }

        public async Task<PatientBill> InsertPatientBill(PatientBill patientBill)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_PATIENT_BILL_INSERT);
            return await requestProvider.PostAsync(url, patientBill);
        }

        public async Task<PatientBillPayment> InsertPatientBillPayment(PatientBillPayment patientBillPayment)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_PATIENT_BILL_PAYMENT_INSERT);
            return await requestProvider.PostAsync(url, patientBillPayment);
        }

        public async Task<SalaryPayment> InsertSalaryPayment(SalaryPayment salaryPayment)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_SALARY_PAYMENT_INSERT);
            return await requestProvider.PostAsync(url, salaryPayment);
        }

        public async Task<Expenses> UpdateExpense(Expenses expense)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_EXPENSES_UPDATE);
            return await requestProvider.PutAsync(url, expense);
        }

        public async Task<HdlBill> UpdateHdlBill(HdlBill bill)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_HDL_BILL_UPDATE);
            return await requestProvider.PutAsync(url, bill);
        }

        public async Task<HdlBillPayment> UpdateHdlBillPayment(HdlBillPayment hdlBillPayment)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_HDL_BILL_PAYMENT_UPDATE);
            return await requestProvider.PutAsync(url, hdlBillPayment);
        }

        public async Task<OtherIncome> UpdateOtherIncome(OtherIncome otherIncome)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_OTHER_INCOME_UPDATE);
            return await requestProvider.PutAsync(url, otherIncome);
        }

        public async Task<PatientBill> UpdatePatientBill(PatientBill patientBill)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_PATIENT_BILL_UPDATE);
            return await requestProvider.PutAsync(url, patientBill);
        }

        public async Task<PatientBillPayment> UpdatePatientBillPayment(PatientBillPayment patientBillPayment)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_PATIENT_BILL_PAYMENT_UPDATE);
            return await requestProvider.PutAsync(url, patientBillPayment);
        }

        public async Task<SalaryPayment> UpdateSalaryPayment(SalaryPayment salaryPayment)
        {
            var url = URLBuilder.GetURL(Controllers.FINANCE, EndPoint.FINANCE_SALARY_PAYMENT_UDPATE);
            return await requestProvider.PutAsync(url, salaryPayment);
        }
    }
}
