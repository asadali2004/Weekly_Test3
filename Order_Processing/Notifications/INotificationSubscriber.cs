using Order_Processing.Enums;

namespace Order_Processing.Notifications
{
    // Interface for notification subscribers
    public interface INotificationSubscriber
    {
        // Method to receive notifications about order status changes
        void Notify(int orderId, OrderStatus newStatus);
    }
}