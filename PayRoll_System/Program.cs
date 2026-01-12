using PayRoll_System.Models;
using PayRoll_System.Services;
using PayRoll_System.Delegates;
using PayRoll_System.Data;

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

            // Use static EmpData stub instead of hardcoded list
            EmpData.SeedSample(); // For quick testing you can comment out to control manually

            // Add a new employee dynamically Sample
            EmpData.Add(new FullTimeEmployee(employeeId: 8, name: "New Employee", monthlySalary: 7500m, bonus: 300m));

            #region Add Emp using User Input (UnComment for testing)
                // User Input to add employees for EmpData
                // Console.Write("\nHow many employees do you want to add? ");
                // if (int.TryParse(Console.ReadLine(), out var count) && count > 0)
                // {
                //     for (int i = 1; i <= count; i++)
                //     {
                //         Console.WriteLine($"\nEmployee #{i}");

                //         int employeeId;
                //         while (true)
                //         {
                //             Console.Write("ID: ");
                //             if (int.TryParse(Console.ReadLine(), out employeeId)) break;
                //             Console.WriteLine("Invalid ID. Try again.");
                //         }

                //         Console.Write("Name: ");
                //         var name = Console.ReadLine();
                //         if (string.IsNullOrWhiteSpace(name)) name = $"Employee_{employeeId}";

                //         decimal monthlySalary;
                //         while (true)
                //         {
                //             Console.Write("Monthly Salary: ");
                //             if (decimal.TryParse(Console.ReadLine(), out monthlySalary)) break;
                //             Console.WriteLine("Invalid salary. Try again.");
                //         }

                //         decimal bonus;
                //         while (true)
                //         {
                //             Console.Write("Bonus (0 if none): ");
                //             if (decimal.TryParse(Console.ReadLine(), out bonus)) break;
                //             Console.WriteLine("Invalid bonus. Try again.");
                //         }

                //         EmpData.Add(new FullTimeEmployee(
                //             employeeId: employeeId,
                //             name: name,
                //             monthlySalary: monthlySalary,
                //             bonus: bonus));

                //         Console.WriteLine("Employee added.");
                //     }
                // }
                // else
                // {
                //     Console.WriteLine("No employees added.");
                // }
                #endregion

            // Load employees from EmpData
            foreach(var e in EmpData.GetAll())
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