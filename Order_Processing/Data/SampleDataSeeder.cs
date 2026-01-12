using Order_Processing.Models;
using Order_Processing.Enums;
using Order_Processing.Data;

namespace Order_Processing.Data
{
    // Static class to seed sample data for products, customers, and orders
	public static class SampleDataSeeder
    {
        // Method to seed products and customers into static repositories
		public static void SeedCatalog()
		{
			CatalogRepository.Clear();
			CustomerRepository.Clear();

			CatalogRepository.Add(new Product(1, "Laptop", 900m));
			CatalogRepository.Add(new Product(2, "Mouse", 20m));
			CatalogRepository.Add(new Product(3, "Keyboard", 35m));
			CatalogRepository.Add(new Product(4, "Monitor", 160m));
			CatalogRepository.Add(new Product(5, "USB-C Cable", 12m));

			CustomerRepository.Add(new Customer(1, "Alice Johnson", "alice@example.com"));
			CustomerRepository.Add(new Customer(2, "Bob Smith", "bob@example.com"));
			CustomerRepository.Add(new Customer(3, "Carol Lee", "carol@example.com"));
		}

		// Method to seed orders with items
		public static List<Order> SeedOrdersFromRepos()
		{
			var orders = new List<Order>
			{
				new Order(1001, CustomerRepository.Customers[1]),
				new Order(1002, CustomerRepository.Customers[2]),
				new Order(1003, CustomerRepository.Customers[3]),
				new Order(1004, CustomerRepository.Customers[1])
			};

			// Add items using static catalog
			orders[0].AddItem(new OrderItem(CatalogRepository.Products[1], 1));
			orders[0].AddItem(new OrderItem(CatalogRepository.Products[2], 2));

			orders[1].AddItem(new OrderItem(CatalogRepository.Products[4], 1));
			orders[1].AddItem(new OrderItem(CatalogRepository.Products[5], 2));

			orders[2].AddItem(new OrderItem(CatalogRepository.Products[3], 1));
			orders[2].AddItem(new OrderItem(CatalogRepository.Products[2], 1));

			orders[3].AddItem(new OrderItem(CatalogRepository.Products[1], 1));
			orders[3].AddItem(new OrderItem(CatalogRepository.Products[4], 2));

			// Persist into static order repository
			OrderRepository.Clear();
			foreach (var o in orders)
			{
				OrderRepository.Add(o);
			}

			return orders;
		}
	}
}
