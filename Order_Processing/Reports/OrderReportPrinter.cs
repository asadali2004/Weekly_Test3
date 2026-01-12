using Order_Processing.Models;
using Order_Processing.Enums;

namespace Order_Processing.Reports
{
    // Static class to print order reports
	public static class OrderReportPrinter
    {
        // Method to print a summary report of orders
		public static void PrintSummary(IEnumerable<Order> orders)
		{
			Console.WriteLine("\n================ Orders Summary ================");
			foreach (var o in orders)
			{
				Console.WriteLine($"Order #{o.OrderId} for {o.Customer.Name} - Status: {o.CurrentStatus}");
				Console.WriteLine("Items:");
				foreach (var item in o.GetItems())
				{
					Console.WriteLine($"  - {item.Product.Name} x{item.Quantity} @ {item.Product.Price:C} = {item.GetTotal():C}");
				}
				Console.WriteLine($"Subtotal: {o.CalculateTotal():C}");
				Console.WriteLine("Status Timeline:");
				foreach (var log in o.GetHistory())
				{
					Console.WriteLine($"  {log.Timestamp:yyyy-MM-dd HH:mm} -> {log.Status}");
				}
				Console.WriteLine("------------------------------------------------");
			}
			Console.WriteLine("================================================\n");
		}
	}
}
