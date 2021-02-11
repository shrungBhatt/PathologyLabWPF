using OPMS.Models.Response;
using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Services.Maintenance
{
    public interface IMaintenanceService
    {
        #region Dhl registration
        Task<DhlRegistrationResponseModel> GetDhlRegistrations();
        Task<HdlRegistration> AddNewDhlRegistration(HdlRegistration registration);
        Task<HdlRegistration> UpdateDhlRegistration(HdlRegistration registration);
        Task<HdlRegistration> DeleteDhlRegistration(HdlRegistration registration);
        #endregion

        #region Rate list
        Task<RateListResponseModel> GetRateLists();
        Task<RateListModel> AddNewRateList(RateListModel rateListModel);
        Task<RateListModel> UpdateRatelist(RateListModel rateListModel);
        Task<HdlRegistration> DeleteRateList(HdlRegistration hdlRegistration);
        #endregion

        #region Inventory
        Task<InventoryResponseModel> GetInventories();
        Task<Inventories> AddNewInventory(Inventories inventory);
        Task<Inventories> UpdateInventory(Inventories inventory);
        Task<Inventories> DeleteInventory(Inventories inventory);
        #endregion

        #region Employee
        Task<EmployeeResponseModel> GetEmployees();
        Task<Employees> AddNewEmployee(Employees employees);
        Task<Employees> UpdateEmployee(Employees employees);
        Task<Employees> DeleteEmployee(Employees employees);
        #endregion

        #region Salary
        Task<SalaryResponseModel> GetSalaries();
        Task<Salary> AddNewSalary(Salary salary);
        Task<Salary> UpdateSalary(Salary salary);
        Task<Salary> DeleteSalary(Salary salary);
        #endregion

        #region Signature
        Task<SignatureResponseModel> GetSignatures();
        Task<SignaturePrototypes> AddNewSignature(SignaturePrototypes signature);
        Task<SignaturePrototypes> UpdateSignature(SignaturePrototypes signature);
        Task<SignaturePrototypes> DeleteSignature(SignaturePrototypes signature);
        #endregion

        #region Abbreavation
        Task<AbbrevationsResponseModel> GetAbbrevations();
        Task<Abbrevations> AddNewAbbrevation(Abbrevations abbrevation);
        Task<Abbrevations> UpdateAbbrevation(Abbrevations abbrevation);
        Task<Abbrevations> DeleteAbbrevation(Abbrevations abbrevation);
        #endregion

        #region Field options
        Task<FieldOptionsResponseModel> GetFieldOptions();
        Task<FieldOptions> AddNewFieldOption(FieldOptions fieldOption);
        Task<FieldOptions> UpdateFieldOption(FieldOptions fieldOption);
        Task<FieldOptions> DeleteFieldOption(FieldOptions fieldOption);
        #endregion
    }
}
