namespace PayRoll_System.Models
{
    /// <summary>
    /// Represents an employee who is compensated on a contract basis with a daily rate.
    /// </summary>
    /// <remarks>Use this class to model contract-based employees whose salary is determined by the number of
    /// days worked and a fixed daily rate. ContractEmployee extends Employee and provides specific logic for salary
    /// calculation and validation relevant to contract workers.</remarks>
    public class ContractEmployee : Employee
    {
        public decimal DailyRate { get; set; } // Payment per day for contract employees
        public int TotalDaysWorked { get; set; } // Total days worked in the pay period

        // Constructor
        public ContractEmployee(int employeeId, string name, decimal dailyRate, int totalDaysWorked)
        {
            EmployeeId = employeeId;
            Name = name;
            EmployeeType = "Contract";
            IsActive = true;

            DailyRate = dailyRate;
            TotalDaysWorked = totalDaysWorked;

            BaseSalary = dailyRate * totalDaysWorked;
            WorkingDays = totalDaysWorked;
        }

        // Calculate gross salary using runtime polymorphism
        public override decimal CalculateSalary()
        {
            var gross = DailyRate * TotalDaysWorked;
            return gross;
        }

        // Calculate deductions (e.g., admin fees) per employee type
        public override decimal CalculateDeductions(decimal gross)
        {
            // Example: 5% admin fee for contractors
            return gross * 0.05m;
        }

        // Specific validation for contract employees
        public override bool Validate()
        {
            if (!base.Validate())
            {
                return false;
            }

            if (DailyRate < 0)
            {
                return false;
            }

            // allow 0–31 inclusive
            if (TotalDaysWorked < 0 || TotalDaysWorked > 31)
            {
                return false;
            }

            return true;
        }
    }
}