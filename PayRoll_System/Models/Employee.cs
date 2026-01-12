namespace PayRoll_System.Models
{
    /// <summary>
    /// Represents an abstract base class for employees, providing common properties and methods for employee management
    /// and payroll calculations.
    /// </summary>
    /// <remarks>Inherit from this class to implement specific employee types with customized salary and
    /// deduction calculations. The class includes common validation logic and exposes properties for identification,
    /// contact information, and employment status. Derived classes must implement salary and deduction calculation
    /// methods appropriate to their employee type.</remarks>
    public abstract class Employee
    {
        public int EmployeeId { get; set; } // Unique identifier for the employee
        public string? Name { get; set; } // Full name of the employee
        public string? Department { get; set; } // Department where the employee works
        public string? Email { get; set; } // Contact email of the employee
        public string? EmployeeType { get; set; } // Type of employee (e.g., FullTime, PartTime, Contractor)
        public bool IsActive { get; set; }   // Employment status of the employee
        protected decimal BaseSalary { get; set; } // Base salary amount for calculations
        protected int WorkingDays { get; set; } // Number of working days in the pay period


        // Calculate gross salary using runtime polymorphism
        public abstract decimal CalculateSalary();

        // Calculate deductions (tax, insurance, admin fees, etc.) per employee type
        public abstract decimal CalculateDeductions(decimal gross);

        // Common validation across all employees
        public virtual bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return false;
            }

            // Salary cannot be negative; 0 allowed (e.g., 0 days worked)
            if (BaseSalary < 0)
            {
                return false;
            }

            // Working days must be 0–31 inclusive
            if (WorkingDays < 0 || WorkingDays > 31)
            {
                return false;
            }

            return true;
        }
    }
}