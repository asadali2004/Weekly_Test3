namespace PayRoll_System.Models
{
    /// <summary>
    /// Represents a full-time employee with a fixed monthly salary and optional bonus compensation.
    /// </summary>
    /// <remarks>This class extends the Employee type to provide salary and deduction calculations specific to
    /// full-time employees. Use FullTimeEmployee for staff members who receive a regular monthly salary rather than
    /// hourly or contract-based compensation.</remarks>
    public class FullTimeEmployee : Employee
    {
        public decimal MonthlySalary { get; set; } // Fixed monthly salary for full-time employees
        public decimal Bonus { get; set; } // Additional bonus for full-time employees

        // Constructor
        public FullTimeEmployee(int employeeId, string name, decimal monthlySalary, decimal bonus = 0)
        {
            EmployeeId = employeeId;
            Name = name;
            EmployeeType = "FullTime";
            IsActive = true;

            MonthlySalary = monthlySalary;
            Bonus = bonus;

            BaseSalary = monthlySalary;
            WorkingDays = 22; // default typical working days
        }

        // Calculate gross salary using runtime polymorphism
        public override decimal CalculateSalary()
        {
            var gross = MonthlySalary + Bonus;
            return gross;
        }

        // Calculate deductions (tax, insurance, etc.) per employee type
        public override decimal CalculateDeductions(decimal gross)
        {
            // Progressive tax example for full-time employees
            if (gross <= 3000m)
            {
                return gross * 0.10m;
            }

            if (gross <= 7000m)
            {
                return gross * 0.20m;
            }

            return gross * 0.30m;
        }

        // Specific validation for full-time employees
        public override bool Validate()
        {
            if (!base.Validate())
            {
                return false;
            }

            if (MonthlySalary < 0)
            {
                return false;
            }

            if (Bonus < 0)
            {
                return false;
            }

            return true;
        }
    }

}