using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Utility
{
    public class URLBuilder
    {
#if _DEBUG
        private static string BASE_URL = "http://localhost:56600/api";
#elif IIS
        private static string BASE_URL = "http://localhost:8080/api";
#else
        private static string BASE_URL = "http://localhost:56600/api";
#endif

        public static string GetURL(string controller, string endpoint)
        {
            return $"{BASE_URL}/{controller}/{endpoint}";
        }
    }

    public class Controllers
    {
        public const string USER = "User";
        public const string TEST = "Test";
        public const string PATIENT = "Patient";
        public const string REAGENT = "Reagent";
        public const string MAINTENANCE = "Maintenance";
        public const string FINANCE = "Finance";
    }

    public class EndPoint
    {
        public const string USER_LIST = "UsersList";
        public const string USER_LOGIN = "Login";
        public const string USER_ADD = "NewUser";
        public const string USER_DELETE = "DeleteUser";
        public const string USER_UPDATE = "UpdateUser";

        public const string TEST_GROUPS = "TestGroups";
        public const string TEST_TITLES = "TestTitles";
        public const string TEST_TITLES_BY_GROUP_ID = "TestTitles/GroupId";
        public const string TEST_OTHERS = "OtherTests";
        public const string TEST_GROUP_ADD = "NewTestGroup";
        public const string TEST_TITLE_ADD = "NewTestTitle";
        public const string TEST_OTHER_ADD = "NewOtherTest";
        public const string TEST_GROUP_UPDATE = "UpdateTestGroup";
        public const string TEST_TITLE_UPDATE = "UpdateTestTitle";
        public const string TEST_OTHER_UPDATE = "UpdateOtherTest";
        public const string TEST_GROUP_DELETE = "DeleteTestGroup";
        public const string TEST_TITLE_DELETE = "DeleteTestTitle";
        public const string TEST_OTHER_DELETE = "DeleteOtherTest";
        public const string TEST_FORMULAS = "Formulas";
        public const string TEST_FORMULA_ADD = "NewFormula";
        public const string TEST_FORMULA_UPDATE = "UpdateFormula";
        public const string TEST_FORMULA_DELETE = "DeleteFormula";
        public const string TEST_GET_TEST_TITLES_FOR_OTHER_GROUP = "GetTestTitlesForOtherGroup";
        public const string TEST_ADD_TEST_TITLES_FOR_OTHER_GROUP = "AddTestTitlesForOtherGroup";

        public const string PATIENT_LIST = "GetPatients";
        public const string PATIENT_LIST_TESTS = "GetPatientsWithTests";
        public const string PATIENT_LIST_TESTS_ID = "GetPatientWithTests";
        public const string PATIENT_ADD = "NewPatient";
        public const string PATIENT_UPDATE = "UpdatePatient";
        public const string PATIENT_DELETE = "DeletePatient";
        public const string PATIENT_BILL_GET = "GetPatientsWithBill";
        public const string PATIENT_UPDATE_TEST_RESULTS = "UpdatePatientTestResults";
        public const string PATIENT_GET_WITH_RATE_LIST_CHARGES = "GetPatientWithTestsWithRateListCharges";
        public const string PATIENT_GET_SPECIALIZED_LAB_SAMPLES = "GetSpecializedLabSamples";
        public const string PATIENT_UPDATE_SPECIALIZED_LAB_SAMPLES = "UpdateSpecializedLabSamples";
        public const string PATIENT_GET_SPECIALIZED_LAB_SAMPLES_REPORT = "GetSpecializedLabReport";
        public const string PATIENT_GET_HDL_REF_PAYMENT_REPORT = "GetHdlReferringReport";

        public const string REAGENT_LIST = "Reagents";
        public const string REAGENT_ADD = "NewReagent";
        public const string REAGENT_UPDATE = "UpdateReagent";
        public const string REAGENT_DELETE = "DeleteReagent";
        public const string TEST_REAGENT_RELATIONS = "TestReagentRelations";
        public const string TEST_REAGENT_RELATIONS_ADD = "NewTestReagentRelation";
        public const string TEST_REAGENT_RELATIONS_UPDATE = "UpdateTestReagentRelation";
        public const string TEST_REAGENT_RELATIONS_DELETE = "DeleteTestReagentRelation";
        public const string DEALER_GET = "GetDealers";
        public const string DEALER_UPDATE = "UpdateDealer";
        public const string DEALER_DELETE = "DeleteDealer";
        public const string DEALER_INSERT = "NewDealer";
        public const string REAGENT_ENTRY_GET = "GetReagentEntries";
        public const string REAGENT_ENTRY_UPDATE = "UpdateReagentEntry";
        public const string REAGENT_ENTRY_DELETE = "DeleteReagentEntry";
        public const string REAGENT_ENTRY_INSERT = "NewReagentEntry";

        public const string MAINTENANCE_DHL_REGISTRATION_GET = "GetDhlRegistrations";
        public const string MAINTENANCE_DHL_REGISTRATION_UPDATE = "UpdateDhlRegistration";
        public const string MAINTENANCE_DHL_REGISTRATION_INSERT = "NewDhlRegistration";
        public const string MAINTENANCE_DHL_REGISTRATION_DELETE = "DeleteDhlRegistration";

        public const string MAINTENANCE_RATE_LIST_GET = "GetRateLists";
        public const string MAINTENANCE_RATE_LIST_UPDATE = "UpdateRateList";
        public const string MAINTENANCE_RATE_LIST_INSERT = "NewRateList";
        public const string MAINTENANCE_RATE_LIST_DELETE = "DeleteRateList";

        public const string MAINTENANCE_INVENTORY_GET = "GetInventoryList";
        public const string MAINTENANCE_INVENTORY_UPDATE = "UpdateInventory";
        public const string MAINTENANCE_INVENTORY_INSERT = "NewInventory";
        public const string MAINTENANCE_INVENTORY_DELETE = "DeleteInventory";

        public const string MAINTENANCE_EMPLOYEE_GET = "GetEmployee";
        public const string MAINTENANCE_EMPLOYEE_UPDATE = "UpdateEmployee";
        public const string MAINTENANCE_EMPLOYEE_INSERT = "NewEmployee";
        public const string MAINTENANCE_EMPLOYEE_DELETE = "DeleteEmployee";

        public const string MAINTENANCE_SALARY_GET = "GetSalary";
        public const string MAINTENANCE_SALARY_UPDATE = "UpdateSalary";
        public const string MAINTENANCE_SALARY_INSERT = "NewSalary";
        public const string MAINTENANCE_SALARY_DELETE = "DeleteSalary";

        public const string MAINTENANCE_SIGNATURE_GET = "GetSignature";
        public const string MAINTENANCE_SIGNATURE_UPDATE = "UpdateSignature";
        public const string MAINTENANCE_SIGNATURE_INSERT = "NewSignature";
        public const string MAINTENANCE_SIGNATURE_DELETE = "DeleteSignature";

        public const string MAINTENANCE_ABBREVATION_GET = "GetAbbrInterp";
        public const string MAINTENANCE_ABBREVATION_UPDATE = "UpdateAbbrInterp";
        public const string MAINTENANCE_ABBREVATION_INSERT = "NewAbbrInterp";
        public const string MAINTENANCE_ABBREVATION_DELETE = "DeleteAbbrInterp";

        public const string MAINTENANCE_FIELD_OPTIONS_GET = "GetFieldOptions";
        public const string MAINTENANCE_FIELD_OPTIONS_UPDATE = "UpdateFieldOptions";
        public const string MAINTENANCE_FIELD_OPTIONS_INSERT = "NewFieldOptions";
        public const string MAINTENANCE_FIELD_OPTIONS_DELETE = "DeleteFieldOptions";

        public const string FINANCE_OTHER_INCOME_GET = "GetOtherIncomes";
        public const string FINANCE_OTHER_INCOME_UPDATE = "UpdateOtherIncome";
        public const string FINANCE_OTHER_INCOME_INSERT = "NewOtherIncome";
        public const string FINANCE_OTHER_INCOME_DELETE = "DeleteOtherIncome";

        public const string FINANCE_EXPENSES_GET = "GetExpenses";
        public const string FINANCE_EXPENSES_UPDATE = "UpdateExpense";
        public const string FINANCE_EXPENSES_INSERT = "NewExpense";
        public const string FINANCE_EXPENSES_DELETE = "DeleteExpense";

        public const string FINANCE_SALARY_PAYMENT_GET = "GetSalaryPayments";
        public const string FINANCE_SALARY_PAYMENT_UDPATE = "UpdateSalaryPayment";
        public const string FINANCE_SALARY_PAYMENT_INSERT = "NewSalaryPayment";

        public const string FINANCE_PATIENT_BILL_INSERT = "NewPatientBill";
        public const string FINANCE_PATIENT_BILL_UPDATE = "UpdatePatientBill";
        public const string FINANCE_PATIENT_BILL_DELETE = "DeletePatientBill";

        public const string FINANCE_PATIENT_BILL_PAYMENT_GET = "GetPatientBillPayments";
        public const string FINANCE_PATIENT_BILL_PAYMENT_INSERT = "NewPatientBillPayment";
        public const string FINANCE_PATIENT_BILL_PAYMENT_UPDATE = "UpdatePatientBillPayment";
        public const string FINANCE_PATIENT_BILL_PAYMENT_DELETE = "DeletePatientBillPayment";

        public const string FINANCE_HDL_BILL_GET = "GetHdlBills";
        public const string FINANCE_HDL_BILL_INSERT = "NewHdlBill";
        public const string FINANCE_HDL_BILL_UPDATE = "UpdateHdlBill";
        public const string FINANCE_HDL_BILL_DELETE = "DeleteHdlBill";

        public const string FINANCE_HDL_BILL_PAYMENT_GET = "GetHdlBillPayments";
        public const string FINANCE_HDL_BILL_PAYMENT_INSERT = "NewHdlBillPayment";
        public const string FINANCE_HDL_BILL_PAYMENT_UPDATE = "UpdateHdlBillPayment";
        public const string FINANCE_HDL_BILL_PAYMENT_DELETE = "DeleteHdlBillPayment";

        public const string FINANCE_PATIENT_REVENUE_REPORT = "GetPatientRevenueReport";
        public const string FINANCE_PATIENT_BILL_PAYMENT_REPORT = "GetPatientBillPaymentReport";
    }
}
