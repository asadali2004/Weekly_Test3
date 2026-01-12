using PayRoll_System.Models;
namespace PayRoll_System.Delegates
{
    /// <summary>
    /// Represents a method that is called when a salary has been processed and a pay slip is available.
    /// </summary>
    /// <param name="paySlip">The pay slip containing details of the processed salary. Cannot be null.</param>
    public delegate void SalaryProcessedDelegate(PaySlip paySlip);
}