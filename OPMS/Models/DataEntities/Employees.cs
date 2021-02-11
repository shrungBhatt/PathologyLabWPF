using Newtonsoft.Json;
using OPMS.Utility.Attributes;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Employees : BaseEntity
    {
        public Employees()
        {
            Salaries = new HashSet<Salary>();
            SalaryPayment = new HashSet<SalaryPayment>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [IgnoreColumn]
        public int GenderId { get; set; }
        public string GenderType
        {
            get => Gender?.Gender;
        }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        [IgnoreColumn]
        public int EmpCategoryId { get; set; }
        public string Category
        {
            get => EmpCategory?.EmployeeCategory;
        }

        [IgnoreColumn]
        public int FieldOptionsId { get; set; }
        public string CivilStatus
        {
            get => FieldOptions?.Name;
        }
        [IgnoreColumn]
        public int EmployeeRoleId { get; set; }

        public string Role
        {
            get => EmployeeRole?.EmployeeRole;
        }
        public int Supervisor { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Salary Salary { get; set; }

        [IgnoreColumn]
        public virtual EmployeeCategories EmpCategory { get; set; }

        [IgnoreColumn]
        public virtual EmployeeRoles EmployeeRole { get; set; }

        [IgnoreColumn]
        public virtual FieldOptions FieldOptions { get; set; }

        [IgnoreColumn]
        public virtual Genders Gender { get; set; }

        [IgnoreColumn]
        public virtual ICollection<Salary> Salaries { get; set; }
        [JsonIgnore]
        public virtual ICollection<SalaryPayment> SalaryPayment { get; set; }
    }
}
