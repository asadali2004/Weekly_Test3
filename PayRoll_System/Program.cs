using PayRoll_System.Models;
using PayRoll_System.Services;
using PayRoll_System.Delegates;

namespace PayRoll_System
{
    /// <summary>
    /// Provides the entry point for the payroll processing application.
    /// </summary>
    /// <remarks>The Program class initializes the payroll processor, subscribes notification services, seeds
    /// sample employee data, and coordinates the payroll processing and reporting workflow. This class is intended to
    /// be executed as the application's main entry point.</remarks>
    public class Program
    {
        /// <summary>
        /// Serves as the entry point for the payroll processing application.
        /// </summary>
        /// <remarks>This method initializes the payroll processor, subscribes notification services,
        /// seeds sample employee data, and triggers payroll processing and reporting. It is intended to be called by
        /// the runtime and should not be invoked directly.</remarks>
        /// <param name="args">An array of command-line arguments supplied to the application. Not used by this implementation.</param>
        public static void Main(string[] args)
        {
            var processor = new PayrollProcessor(); // Initialize payroll processor

            // Subscribe notifications
            processor.SubscribeNotifications(NotificationService.NotifyHR);
            processor.SubscribeNotifications(NotificationService.NotifyFinance);

            // Seed sample employees
            var employees = new List<Employee>
                {
                    new FullTimeEmployee(employeeId: 1, name: "Alice Johnson", monthlySalary: 8000m, bonus: 500m),
                    new FullTimeEmployee(employeeId: 2, name: "Carlos Diaz", monthlySalary: 6500m, bonus: 0m),
                    new FullTimeEmployee(employeeId: 3, name: "Emily Clark", monthlySalary: 9200m, bonus: 1200m),
                    new ContractEmployee(employeeId: 4, name: "Bob Smith", dailyRate: 300m, totalDaysWorked: 20),
                    new ContractEmployee(employeeId: 5, name: "Dana Lee", dailyRate: 280m, totalDaysWorked: 22),
                    new ContractEmployee(employeeId: 6, name: "Frank Miller", dailyRate: 350m, totalDaysWorked: 18),
                    // Invalid sample to demonstrate validation (days > 31)
                    new ContractEmployee(employeeId: 7, name: "Invalid Input", dailyRate: 250m, totalDaysWorked: 45),
                };

                foreach (var e in employees)
                {
                    processor.AddEmployee(e);
                }

                Console.WriteLine($"\nAdded {processor.EmployeeCount} valid employees to the system.");

                // Process and report
                processor.ProcessPayroll();
                processor.GenerateReport();
        }
    }
}