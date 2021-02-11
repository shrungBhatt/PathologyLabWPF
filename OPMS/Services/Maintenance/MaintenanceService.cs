using OPMS.Models.Response;
using OPMS.Services.RequestProvider;
using OPMS.Utility;
using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Services.Maintenance
{
    public class MaintenanceService : IMaintenanceService
    {
        readonly IRequestProvider requestProvider;

        public MaintenanceService(IRequestProvider requestProvider)
        {
            this.requestProvider = requestProvider;
        }

        public async Task<Abbrevations> AddNewAbbrevation(Abbrevations abbrevation)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_ABBREVATION_INSERT);
            return await requestProvider.PostAsync(url, abbrevation);
        }

        public async Task<HdlRegistration> AddNewDhlRegistration(HdlRegistration registration)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_DHL_REGISTRATION_INSERT);
            return await requestProvider.PostAsync(url, registration);
        }

        public async Task<Employees> AddNewEmployee(Employees employees)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_EMPLOYEE_INSERT);
            return await requestProvider.PostAsync(url, employees);
        }

        public async Task<FieldOptions> AddNewFieldOption(FieldOptions fieldOption)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_FIELD_OPTIONS_INSERT);
            return await requestProvider.PostAsync(url, fieldOption);
        }

        public async Task<Inventories> AddNewInventory(Inventories inventory)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_INVENTORY_INSERT);
            return await requestProvider.PostAsync(url, inventory);
        }

        public async Task<RateListModel> AddNewRateList(RateListModel rateListModel)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_RATE_LIST_INSERT);
            return await requestProvider.PostAsync(url, rateListModel);
        }

        public async Task<Salary> AddNewSalary(Salary salary)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_SALARY_INSERT);
            return await requestProvider.PostAsync(url, salary);
        }

        public async Task<SignaturePrototypes> AddNewSignature(SignaturePrototypes signature)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_SIGNATURE_INSERT);
            return await requestProvider.PostAsync(url, signature);
        }

        public async Task<Abbrevations> DeleteAbbrevation(Abbrevations abbrevation)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_ABBREVATION_DELETE);
            return await requestProvider.DeleteAsync(url, abbrevation, new Dictionary<string, string> { ["id"] = abbrevation.Id.ToString() });
        }

        public async Task<HdlRegistration> DeleteDhlRegistration(HdlRegistration registration)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_DHL_REGISTRATION_DELETE);
            return await requestProvider.DeleteAsync(url, registration, new Dictionary<string, string> { ["id"] = registration.Id.ToString() });
        }

        public async Task<Employees> DeleteEmployee(Employees employees)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_EMPLOYEE_DELETE);
            return await requestProvider.DeleteAsync(url, employees, new Dictionary<string, string> { ["id"] = employees.Id.ToString() });
        }

        public async Task<FieldOptions> DeleteFieldOption(FieldOptions fieldOption)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_FIELD_OPTIONS_DELETE);
            return await requestProvider.DeleteAsync(url, fieldOption, new Dictionary<string, string> { ["id"] = fieldOption.Id.ToString() });
        }

        public async Task<Inventories> DeleteInventory(Inventories inventory)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_INVENTORY_DELETE);
            return await requestProvider.DeleteAsync(url, inventory, new Dictionary<string, string> { ["id"] = inventory.Id.ToString() });
        }

        public async Task<HdlRegistration> DeleteRateList(HdlRegistration hdlRegistration)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_RATE_LIST_DELETE);
            return await requestProvider.DeleteAsync(url, hdlRegistration, new Dictionary<string, string> { ["id"] = hdlRegistration.Id.ToString() });
        }

        public async Task<Salary> DeleteSalary(Salary salary)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_SALARY_DELETE);
            return await requestProvider.DeleteAsync(url, salary, new Dictionary<string, string> { ["id"] = salary.Id.ToString() });
        }

        public async Task<SignaturePrototypes> DeleteSignature(SignaturePrototypes signature)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_SIGNATURE_DELETE);
            return await requestProvider.DeleteAsync(url, signature, new Dictionary<string, string> { ["id"] = signature.Id.ToString() });
        }

        public async Task<AbbrevationsResponseModel> GetAbbrevations()
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_ABBREVATION_GET);
            return await requestProvider.GetAsync<AbbrevationsResponseModel>(url);
        }

        public async Task<DhlRegistrationResponseModel> GetDhlRegistrations()
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_DHL_REGISTRATION_GET);
            return await requestProvider.GetAsync<DhlRegistrationResponseModel>(url);
        }

        public async Task<EmployeeResponseModel> GetEmployees()
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_EMPLOYEE_GET);
            return await requestProvider.GetAsync<EmployeeResponseModel>(url);
        }

        public async Task<FieldOptionsResponseModel> GetFieldOptions()
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_FIELD_OPTIONS_GET);
            return await requestProvider.GetAsync<FieldOptionsResponseModel>(url);
        }

        public async Task<InventoryResponseModel> GetInventories()
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_INVENTORY_GET);
            return await requestProvider.GetAsync<InventoryResponseModel>(url);
        }

        public async Task<RateListResponseModel> GetRateLists()
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_RATE_LIST_GET);
            return await requestProvider.GetAsync<RateListResponseModel>(url);
        }

        public async Task<SalaryResponseModel> GetSalaries()
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_SALARY_GET);
            return await requestProvider.GetAsync<SalaryResponseModel>(url);
        }

        public async Task<SignatureResponseModel> GetSignatures()
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_SIGNATURE_GET);
            return await requestProvider.GetAsync<SignatureResponseModel>(url);
        }

        public async Task<Abbrevations> UpdateAbbrevation(Abbrevations abbrevation)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_ABBREVATION_UPDATE);
            return await requestProvider.PutAsync(url, abbrevation);
        }

        public async Task<HdlRegistration> UpdateDhlRegistration(HdlRegistration registration)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_DHL_REGISTRATION_UPDATE);
            return await requestProvider.PutAsync(url, registration);
        }

        public async Task<Employees> UpdateEmployee(Employees employees)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_EMPLOYEE_UPDATE);
            return await requestProvider.PutAsync(url, employees);
        }

        public async Task<FieldOptions> UpdateFieldOption(FieldOptions fieldOption)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_FIELD_OPTIONS_UPDATE);
            return await requestProvider.PutAsync(url, fieldOption);
        }

        public async Task<Inventories> UpdateInventory(Inventories inventory)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_INVENTORY_UPDATE);
            return await requestProvider.PutAsync(url, inventory);
        }

        public async Task<RateListModel> UpdateRatelist(RateListModel rateListModel)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_RATE_LIST_UPDATE);
            return await requestProvider.PutAsync(url, rateListModel);
        }

        public async Task<Salary> UpdateSalary(Salary salary)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_SALARY_UPDATE);
            return await requestProvider.PutAsync(url, salary);
        }

        public async Task<SignaturePrototypes> UpdateSignature(SignaturePrototypes signature)
        {
            var url = URLBuilder.GetURL(Controllers.MAINTENANCE, EndPoint.MAINTENANCE_SIGNATURE_UPDATE);
            return await requestProvider.PutAsync(url, signature);
        }
    }
}
