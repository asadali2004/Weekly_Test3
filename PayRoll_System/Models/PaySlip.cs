namespace PayRoll_System.Models
{
    public class PaySlip
    {
        public int EmployeeId { get; set; } // Employee identifier
        public string EmployeeName { get; set; } = string.Empty; // Employee name
        public string EmployeeType { get; set; } = string.Empty; // Employee type (FullTime, Contract, etc.)
        public decimal GrossSalary { get; set; } // Total gross salary
        public decimal Deductions { get; set; } // Total deductions
        public decimal NetSalary { get; set; } // Net salary after deductions
        public DateTime GeneratedOn { get; set; } // Timestamp of payslip generation
    }
}