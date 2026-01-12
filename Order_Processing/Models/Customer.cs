namespace Order_Processing.Models
{
    // Model representing a customer in the order processing system
    public class Customer
    {
        public int Id { get; } // Unique identifier for the customer
        public string Name { get; } // Name of the customer
        public string Email { get; } // Email address of the customer

        // Constructor
        public Customer(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}