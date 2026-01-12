using Order_Processing.Models;

namespace Order_Processing.Data
{
    public static class CustomerRepository
    {
        public static readonly Dictionary<int, Customer> Customers = new Dictionary<int, Customer>();

        public static void Clear() => Customers.Clear();

        public static void Add(Customer customer)
        {
            Customers[customer.Id] = customer;
        }

        public static IReadOnlyDictionary<int, Customer> GetAll() => Customers;
    }
}