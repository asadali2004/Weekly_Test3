using Order_Processing.Models;

namespace Order_Processing.Data
{
    public static class OrderRepository
    {
        private static readonly Dictionary<int, Order> _orders = new Dictionary<int, Order>();

        public static void Clear() => _orders.Clear();

        public static void Add(Order order)
        {
            _orders[order.OrderId] = order;
        }

        public static IReadOnlyDictionary<int, Order> GetAll() => _orders;
    }
}