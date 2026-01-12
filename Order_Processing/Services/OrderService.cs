using Order_Processing.Enums;
using Order_Processing.Models;
using Order_Processing.Notifications;

namespace Order_Processing.Services
{
    // Service class to manage orders and their status changes 
    public class OrderService
    {
        private readonly Dictionary<int, Order> _orders = new Dictionary<int, Order>(); // In-memory storage of orders by ID
        public delegate void StatusChangedDelegate(int orderId, OrderStatus oldStatus, OrderStatus newStatus); // Delegate for status change notifications
        public StatusChangedDelegate? StatusChanged; // Event for status change notifications
        private readonly List<INotificationSubscriber> _subscribers = new List<INotificationSubscriber>(); // List of notification subscribers

        // Method to subscribe a notification subscriber
        public void Subscribe(INotificationSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        // Read-only access to orders
        public IReadOnlyDictionary<int, Order> Orders => _orders;

        // Create a new order
        public Order CreateOrder(int orderId, Customer customer)
        {
            var order = new Order(orderId, customer);
            _orders[orderId] = order;
            return order;
        }

        // Register an existing seeded order (with items/history)
        public void AddOrder(Order order)
        {
            _orders[order.OrderId] = order;
        }

        // Validate and change order status per business rules
        public bool ChangeOrderStatus(int orderId, OrderStatus newStatus)
        {
            if (!_orders.TryGetValue(orderId, out var order))
            {
                Console.WriteLine($"[Error] Order {orderId} not found.");
                return false;
            }

            var old = order.CurrentStatus;
            if (!IsValidTransition(old, newStatus))
            {
                Console.WriteLine($"[Invalid Transition] Order {orderId}: {old} -> {newStatus}");
                return false;
            }

            order.ChangeStatus(newStatus);

            // Delegate notification
            StatusChanged?.Invoke(orderId, old, newStatus);

            // Subscriber notifications (multicast behavior)
            foreach (var s in _subscribers)
            {
                try { s.Notify(orderId, newStatus); } catch { /* continue others */ }
            }

            return true;
        }

        // Validate status transitions based on business rules
        private bool IsValidTransition(OrderStatus oldStatus, OrderStatus newStatus)
        {
            if (oldStatus == OrderStatus.Cancelled)
                return false;

            // Valid chain: Created → Paid → Packed → Shipped → Delivered
            return (oldStatus, newStatus) switch
            {
                (OrderStatus.Created, OrderStatus.Paid) => true,
                (OrderStatus.Paid, OrderStatus.Packed) => true,
                (OrderStatus.Packed, OrderStatus.Shipped) => true,
                (OrderStatus.Shipped, OrderStatus.Delivered) => true,
                // Allow cancel from Created or Paid
                (OrderStatus.Created, OrderStatus.Cancelled) => true,
                (OrderStatus.Paid, OrderStatus.Cancelled) => true,
                // No other transitions allowed
                _ => false
            };
        }
    }
}