using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class SalaryPayment : BaseEntity
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public DateTime SalaryDate { get; set; }
        public string MonthName { get; set; }
        public int YearName { get; set; }
        public int WorkDays { get; set; }
        public decimal? ProfessionalTax { get; set; }
        public decimal? ProvidentFund { get; set; }
        public decimal? LoanDeduction { get; set; }
        public decimal? SalaryPayable { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool? CashMode { get; set; } = false;
        public bool? ChequeMode { get; set; } = false;
        public bool? CardMode { get; set; } = false;
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string ChequeNo { get; set; }
        public string CardNo { get; set; }
        public DateTime? ChequeDate { get; set; }

        public virtual Employees Emp { get; set; }
    }
}
