# Order_Processing

A console app demonstrating Online Order Processing with OOP, generic collections, and delegate-based multicast notifications.

## Features
- Order workflow: Created → Paid → Packed → Shipped → Delivered; Cancel allowed from Created/Paid.
- Encapsulation and composition: `Order` has `OrderItem`s; `Customer`, `Product` models.
- Collections: `Dictionary<int, Order>` for orders, `Dictionary<int, Product>` for catalog, `List<OrderStatusLog>` history.
- Delegates: `StatusChangedDelegate` plus subscriber classes (`CustomerNotification`, `LogisticsNotification`).
- Reporting: Prints per-order items, totals, current status, and full status timeline.

## Quick Start
```powershell
# From repository root
cd Order_Processing
dotnet run
# Or auto-reload during edits
dotnet watch
```

## Structure
```
Order_Processing/
	Program.cs
	Enums/
		OrderStatus.cs
	Models/
		Customer.cs
		Product.cs
		OrderItem.cs
		Order.cs
		OrderStatusLog.cs
	Services/
		OrderService.cs
	Notifications/
		INotificationSubscriber.cs
		CustomerNotification.cs
		LogisticsNotification.cs
	Data/
		SampleDataSeeder.cs
	Reports/
		OrderReportPrinter.cs
```

## Design Highlights
- Status validation in `OrderService.IsValidTransition()`; `Order.ChangeStatus()` logs status and timestamp.
- Multicast notifications: subscribers via `OrderService.Subscribe(INotificationSubscriber)` and a delegate callback.
- Seed data: 5 products, 3 customers, 4 orders with multiple items.

## Business Rules
- Cannot ship before paid; cannot deliver before shipped.
- Cancelled orders cannot progress further.
- Invalid transitions are printed and ignored.

## Extensibility
- Add new statuses by extending `OrderStatus` and updating `IsValidTransition`.
- New notifications: implement `INotificationSubscriber` and call `Subscribe()`.
- Pricing strategies: introduce a `PricingService` or use a delegate to plug alternate calculators.

## Sample Output
- Delegate and subscriber notifications for each status change.
- Summary report per order with items, totals, and history timeline.

## Notes
- All logic runs in-memory; no databases or external APIs.
- Nullable reference types enabled; prefer initializing strings and guarding nulls.
