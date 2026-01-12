using PayRoll_System.Models;
using PayRoll_System.Delegates;
namespace PayRoll_System.Services
{
    /// <summary>
    /// Provides methods for sending notifications to HR and Finance departments regarding processed salary information.
    /// </summary>
    /// <remarks>This static class is intended to be used as a utility for notifying relevant departments
    /// after salary processing. All methods are thread-safe and can be called from any context.</remarks>
    public static class NotificationService
    {
        
        public static void NotifyHR(PaySlip paySlip)
        {
            // Simulate notifying HR about the processed salary
            Console.WriteLine($"[HR Notification] ID={paySlip.EmployeeId}, Name={paySlip.EmployeeName}, Type={paySlip.EmployeeType}, Net={paySlip.NetSalary:C}");
        }

        public static void NotifyFinance(PaySlip paySlip)
        {
            // Simulate notifying Finance about the processed salary
            Console.WriteLine($"[Finance Notification] ID={paySlip.EmployeeId}, Name={paySlip.EmployeeName}, Type={paySlip.EmployeeType}, Gross={paySlip.GrossSalary:C}, Deductions={paySlip.Deductions:C}");
        }
    }
}