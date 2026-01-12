using Order_Processing.Enums;

namespace Order_Processing.Models
{
    // Class representing a log entry for order status changes
    public class OrderStatusLog
    {
        public OrderStatus Status { get; private set; } // Status after the change
        public DateTime Timestamp { get; private set; } // Time of the status change

        // Constructor
        public OrderStatusLog(OrderStatus status, DateTime timestamp)
        {
            Status = status;
            Timestamp = timestamp;
        }
    }
}