using Order_Processing.Enums;

namespace Order_Processing.Notifications
{
    // Class to notify logistics team about order status changes
    public class LogisticsNotification : INotificationSubscriber
    {
        // Method to notify logistics team about order status changes
        public void Notify(int orderId, OrderStatus newStatus)
        {
            Console.WriteLine($"Logistics Notification: Order {orderId} status changed to {newStatus}");
        }
    }
}