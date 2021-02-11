using OPMS.Models.Response;
using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Services.Finance
{
    public interface IFinanceService
    {
        Task<OtherIncomeResponseModel> GetOtherIncomes();
        Task<OtherIncome> UpdateOtherIncome(OtherIncome otherIncome);
        Task<OtherIncome> InsertOtherIncome(OtherIncome otherIncome);
        Task<OtherIncome> DeleteOtherIncome(OtherIncome otherIncome);

        Task<ExpensesResponseModel> GetExpenses();
        Task<Expenses> UpdateExpense(Expenses expense);
        Task<Expenses> InsertExpense(Expenses expense);
        Task<Expenses> DeleteExpense(Expenses expense);

        Task<SalaryPaymentResponseModel> GetSalaryPayment();
        Task<SalaryPayment> UpdateSalaryPayment(SalaryPayment salaryPayment);
        Task<SalaryPayment> InsertSalaryPayment(SalaryPayment salaryPayment);

        Task<PatientBill> InsertPatientBill(PatientBill patientBill);
        Task<PatientBill> UpdatePatientBill(PatientBill patientBill);
        Task<PatientBill> DeletePatientBill(PatientBill patientBill);

        Task<PatientBillPaymentResponseModel> GetBillPayments();
        Task<PatientBillPayment> InsertPatientBillPayment(PatientBillPayment patientBillPayment);
        Task<PatientBillPayment> UpdatePatientBillPayment(PatientBillPayment patientBillPayment);
        Task<PatientBillPayment> DeletePatientBillPayment(PatientBillPayment patientBillPayment);

        Task<HdlBillResponseModel> GetHdlBills();
        Task<HdlBill> UpdateHdlBill(HdlBill bill);
        Task<HdlBill> InsertHdlBill(HdlBill bill);
        Task<HdlBill> DeleteHdlBill(HdlBill bill);

        Task<HdlBillPaymentResponseModel> GetHdlBillPayments();
        Task<HdlBillPayment> InsertHdlBillPayment(HdlBillPayment hdlBillPayment);
        Task<HdlBillPayment> UpdateHdlBillPayment(HdlBillPayment hdlBillPayment);
        Task<HdlBillPayment> DeleteHdlBillPayment(HdlBillPayment hdlBillPayment);

        Task<PatientRevenueReportResponseModel> GetPatientRevenueResponseModel(PatientRevenueReportResponseModel model);
        Task<PatientBillPaymentReportResponeModel> GetPatientBillPaymentReport(PatientBillPaymentReportResponeModel model);
    }
}
