using PayRoll_System.Models;
using PayRoll_System.Delegates;

namespace PayRoll_System.Services
{
    /// <summary>
    /// Provides functionality to manage employees, process payroll, and generate payroll reports for an organization.
    /// </summary>
    /// <remarks>The PayrollProcessor class maintains a collection of employees and their associated pay
    /// slips. It supports adding employees, processing payroll calculations, generating summary reports, and notifying
    /// subscribers when a salary is processed. This class is not thread-safe; if accessed from multiple threads,
    /// callers should implement their own synchronization.</remarks>
    public class PayrollProcessor
    {
        private static Dictionary<int, Employee> employees = new Dictionary<int, Employee>();
        private static List<PaySlip> paySlips = new List<PaySlip>();
        private static Dictionary<int, PaySlip> paySlipIndex = new Dictionary<int, PaySlip>();

        public SalaryProcessedDelegate? SalaryProcessed;

        public int EmployeeCount => employees.Count;

        // Adds a new employee to the payroll system after validating the employee data.
        public void AddEmployee(Employee employee)
        {
            if (employee != null && employee.Validate())
            {
                employees[employee.EmployeeId] = employee;
            }
            else
            {
                Console.WriteLine($"[Validation Error] Employee '{employee?.Name}' (ID: {employee?.EmployeeId}) is invalid and will be skipped.");
            }
        }

        // Processes payroll for all employees, calculating gross salary, deductions, and net salary.
        public void ProcessPayroll()
        {
            Console.WriteLine("\nProcessing payroll...");
            foreach (var employee in employees.Values)
            {
                try
                {
                    decimal grossSalary = employee.CalculateSalary();
                    decimal deductions = employee.CalculateDeductions(grossSalary);
                    decimal netSalary = grossSalary - deductions;

                    var paySlip = new PaySlip
                    {
                        EmployeeId = employee.EmployeeId,
                        EmployeeName = employee.Name ?? string.Empty,
                        EmployeeType = employee.EmployeeType ?? string.Empty,
                        GrossSalary = grossSalary,
                        Deductions = deductions,
                        NetSalary = netSalary,
                        GeneratedOn = DateTime.Now
                    };

                    paySlips.Add(paySlip);
                    paySlipIndex[employee.EmployeeId] = paySlip;

                    Console.WriteLine($"Processed: ID={paySlip.EmployeeId}, Name={paySlip.EmployeeName}, Type={paySlip.EmployeeType}, Gross={paySlip.GrossSalary:C}, Deductions={paySlip.Deductions:C}, Net={paySlip.NetSalary:C}");

                    // Invoke the delegate to notify subscribers
                    SalaryProcessed?.Invoke(paySlip);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Error] Failed processing employee ID {employee.EmployeeId}: {ex.Message}");
                }
            }
        }

        // Generates a summary report of the payroll processing, including totals and per-employee breakdowns.
        public void GenerateReport()
        {
            Console.WriteLine("\n================ Payroll Summary Report ================");
            Console.WriteLine($"Total Employees Processed: {paySlips.Count}");

            var totalPayout = paySlips.Sum(p => p.NetSalary);
            Console.WriteLine($"Total Payout: {totalPayout:C}");

            var byType = paySlips.GroupBy(p => p.EmployeeType)
                                  .Select(g => new { Type = g.Key, Count = g.Count(), TotalNet = g.Sum(p => p.NetSalary) });
            foreach (var t in byType)
            {
                Console.WriteLine($"Type: {t.Type}, Count: {t.Count}, Total Net: {t.TotalNet:C}");
            }

            var highest = paySlips.OrderByDescending(p => p.NetSalary).FirstOrDefault();
            if (highest != null)
            {
                Console.WriteLine($"Highest Net Salary: {highest.NetSalary:C} (ID={highest.EmployeeId}, Name={highest.EmployeeName}, Type={highest.EmployeeType})");
            }

            Console.WriteLine("\nPer-Employee Breakdown:");
            foreach (var paySlip in paySlips)
            {
                Console.WriteLine($"ID={paySlip.EmployeeId}, Name={paySlip.EmployeeName}, Type={paySlip.EmployeeType}, Gross={paySlip.GrossSalary:C}, Deductions={paySlip.Deductions:C}, Net={paySlip.NetSalary:C}");
            }
            Console.WriteLine("=======================================================\n");
        }

        // Subscribes a notification method to be called when a salary is processed.
        public void SubscribeNotifications(SalaryProcessedDelegate notificationMethod)
        {
            SalaryProcessed += notificationMethod;
        }
    }
}