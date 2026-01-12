using Order_Processing.Enums;

namespace Order_Processing.Models
{
    // Class representing an order in the order processing system
    public class Order
    {
        public int OrderId { get; } // Unique identifier for the order
        public Customer Customer { get; } // Customer who placed the order
        private List<OrderItem> Items { get; } = new List<OrderItem>(); // List of items in the order
        public OrderStatus CurrentStatus { get; private set; } // Current status of the order
        private List<OrderStatusLog> StatusHistory { get; } = new List<OrderStatusLog>(); // History of status changes

        // Constructor
        public Order(int orderId, Customer customer)
        {
            OrderId = orderId;
            Customer = customer;
            CurrentStatus = OrderStatus.Created; // Default status
            StatusHistory.Add(new OrderStatusLog(CurrentStatus, DateTime.Now));
        }

        // Method to add an item to the order
        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        // Method to calculate the total price of the order
        public decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.GetTotal();
            }
            return total;
        }

        // Method to change the status of the order
        public void ChangeStatus(OrderStatus newStatus)
        {
            // Status change validation should be handled externally
            CurrentStatus = newStatus;
            StatusHistory.Add(new OrderStatusLog(newStatus, DateTime.Now));
        }

        // Method to get the history of status changes
        public IReadOnlyList<OrderStatusLog> GetHistory()
        {
            return StatusHistory.AsReadOnly();
        }

        public IReadOnlyList<OrderItem> GetItems() => Items.AsReadOnly();
    }
}