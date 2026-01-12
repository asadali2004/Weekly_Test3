using Order_Processing.Enums;

namespace Order_Processing.Notifications
{
    // Class to notify customers about order status changes
    public class CustomerNotification : INotificationSubscriber
    {
        // Method to notify customer about order status changes
        public void Notify(int orderId, OrderStatus newStatus)
        {
            Console.WriteLine($"Customer Notification: Order {orderId} status changed to {newStatus}");
        }
    }
}
