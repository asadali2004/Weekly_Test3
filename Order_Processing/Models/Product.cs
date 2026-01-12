namespace Order_Processing.Models
{
    // Model representing a product in the order processing system
    public class Product
    {
        public int Id { get; set; } // Unique identifier for the product
        public string Name { get; set; } // Name of the product
        public decimal Price { get; set; } // Price of the product

        // Constructor
        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        // Method to get the price of the product
        public decimal GetPrice()
        {
            return Price;
        }
    }
}