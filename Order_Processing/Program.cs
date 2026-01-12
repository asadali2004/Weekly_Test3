using Order_Processing.Data;
using Order_Processing.Services;
using Order_Processing.Notifications;
using Order_Processing.Reports;
using Order_Processing.Enums;
using Order_Processing.Models;

namespace Order_Processing
{
    // Main program to demonstrate order processing workflow
	public class Program
    {
        // Entry point of the application
		public static void Main(string[] args)
		{
			SampleDataSeeder.SeedCatalog(); // Seed static repositories
			SampleDataSeeder.SeedOrdersFromRepos(); // Build orders using static repos

			var service = new OrderService(); // Initialize order service

			// Subscribe notifications (multicast)
			service.Subscribe(new CustomerNotification());
			service.Subscribe(new LogisticsNotification());

			// Delegate subscription example (optional)
			service.StatusChanged += (orderId, oldStatus, newStatus) =>
			{
				Console.WriteLine($"[Delegate] Order {orderId}: {oldStatus} -> {newStatus}");
			};

			// Register seeded orders (with items) in service from static repository
			foreach (var o in OrderRepository.GetAll().Values)
			{
				service.AddOrder(o);
			}

			// Valid workflow: Created→Paid→Packed→Shipped→Delivered
			service.ChangeOrderStatus(1001, OrderStatus.Paid);
			service.ChangeOrderStatus(1001, OrderStatus.Packed);
			service.ChangeOrderStatus(1001, OrderStatus.Shipped);
			service.ChangeOrderStatus(1001, OrderStatus.Delivered);

			// Demonstrate invalid transition: Ship before Paid
			service.ChangeOrderStatus(1002, OrderStatus.Shipped); // should print invalid

			// Cancel example
			service.ChangeOrderStatus(1003, OrderStatus.Paid);
			service.ChangeOrderStatus(1003, OrderStatus.Cancelled);
			service.ChangeOrderStatus(1003, OrderStatus.Shipped); // should be blocked

			// Pack and ship another order
			service.ChangeOrderStatus(1004, OrderStatus.Paid);
			service.ChangeOrderStatus(1004, OrderStatus.Packed);
			service.ChangeOrderStatus(1004, OrderStatus.Shipped);

			// Print report
			OrderReportPrinter.PrintSummary(service.Orders.Values);
		}
	}
}
