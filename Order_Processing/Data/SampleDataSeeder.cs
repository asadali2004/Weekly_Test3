using Order_Processing.Models;
using Order_Processing.Enums;

namespace Order_Processing.Data
{
    // Static class to seed sample data for products, customers, and orders
	public static class SampleDataSeeder
    {
        // Method to seed products and customers
		public static (Dictionary<int, Product> products, List<Customer> customers) SeedCatalog()
		{
			var products = new Dictionary<int, Product>
			{
				[1] = new Product(1, "Laptop", 900m),
				[2] = new Product(2, "Mouse", 20m),
				[3] = new Product(3, "Keyboard", 35m),
				[4] = new Product(4, "Monitor", 160m),
				[5] = new Product(5, "USB-C Cable", 12m)
			};

			var customers = new List<Customer>
			{
				new Customer(1, "Alice Johnson", "alice@example.com"),
				new Customer(2, "Bob Smith", "bob@example.com"),
				new Customer(3, "Carol Lee", "carol@example.com"),
			};

			return (products, customers);
		}

		// Method to seed orders with items
		public static List<Order> SeedOrders(Dictionary<int, Product> products, List<Customer> customers)   
		{
			var orders = new List<Order>
			{
				new Order(1001, customers[0]),
				new Order(1002, customers[1]),
				new Order(1003, customers[2]),
				new Order(1004, customers[0])
			};

			// Add items
			orders[0].AddItem(new OrderItem(products[1], 1));
			orders[0].AddItem(new OrderItem(products[2], 2));

			orders[1].AddItem(new OrderItem(products[4], 1));
			orders[1].AddItem(new OrderItem(products[5], 2));

			orders[2].AddItem(new OrderItem(products[3], 1));
			orders[2].AddItem(new OrderItem(products[2], 1));

			orders[3].AddItem(new OrderItem(products[1], 1));
			orders[3].AddItem(new OrderItem(products[4], 2));

			return orders;
		}
	}
}
