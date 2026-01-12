**Week 3 — OOPS + Generics + Delegates**

- Two small .NET console apps built to demonstrate core OOP, generic collections, and delegates via two case studies: a Payroll System and an Online Order Processing workflow.

**Projects**
- **Payroll System**: [PayRoll_System](PayRoll_System)
- **Order Processing**: [Order_Processing](Order_Processing)

**How To Run**
- Prereq: .NET SDK installed.
- From the repo root, run each project independently:

```powershell
cd .\PayRoll_System
dotnet run

cd ..\Order_Processing
dotnet run
```

**Use Case 1: Payroll System**
- **Goal**: Process employees, apply polymorphic deductions, and notify via delegates while printing a summary report.
- **Key Types**:
	- Base `Employee` with validation and abstract APIs ([PayRoll_System/Models/Employee.cs](PayRoll_System/Models/Employee.cs)).
	- `FullTimeEmployee`, `ContractEmployee` override `CalculateSalary()` and `CalculateDeductions(...)` ([PayRoll_System/Models/FullTimeEmployee.cs](PayRoll_System/Models/FullTimeEmployee.cs), [PayRoll_System/Models/ContractEmployee.cs](PayRoll_System/Models/ContractEmployee.cs)).
	- `PaySlip` output model ([PayRoll_System/Models/PaySlip.cs](PayRoll_System/Models/PaySlip.cs)).
	- `PayrollProcessor` orchestrates processing and reporting; exposes multicast delegate for notifications ([PayRoll_System/Services/PayrollProcessor.cs](PayRoll_System/Services/PayrollProcessor.cs)).
	- `NotificationService` provides HR/Finance handlers ([PayRoll_System/Services/NotificationService.cs](PayRoll_System/Services/NotificationService.cs)).
	- Static stub repo `EmpData` seeds and serves employees ([PayRoll_System/Data/EmpData.cs](PayRoll_System/Data/EmpData.cs)).
- **Collections**: `List<Employee>` for roster, `List<PaySlip>` for results, simple `Dictionary` aggregations for reporting.
- **Delegates**: `SalaryProcessedDelegate(PaySlip)` is multicast (HR + Finance subscribers).
- **Business Rules**: Validation on salary inputs; deductions calculated polymorphically per employee type.
- **Output**: Per-employee breakdown, totals, counts by type, highest net, and notification lines.

**Use Case 2: Online Order Processing & Notifications**
- Based on the learner task sheet in [Order_Processing/UseCase.txt](Order_Processing/UseCase.txt).
- **Goal**: Manage products/customers/orders, enforce valid status transitions, record history, and notify multiple subscribers using delegates. Print order summary and full timeline.
- **Key Types**:
	- Domain: `Product`, `Customer`, `OrderItem`, `Order`, `OrderStatusLog` ([Order_Processing/Models](Order_Processing/Models)).
	- Status: `OrderStatus` enum ([Order_Processing/Enums/OrderStatus.cs](Order_Processing/Enums/OrderStatus.cs)).
	- Service: `OrderService` owns status changes, validation, history recording, and delegate invocation ([Order_Processing/Services/OrderService.cs](Order_Processing/Services/OrderService.cs)).
	- Notifications: `INotificationSubscriber`, `CustomerNotification`, `LogisticsNotification` ([Order_Processing/Notifications](Order_Processing/Notifications)).
	- Reporting: `OrderReportPrinter` prints items, totals, and full timeline ([Order_Processing/Reports/OrderReportPrinter.cs](Order_Processing/Reports/OrderReportPrinter.cs)).
	- Static Repos: `CatalogRepository` (products), `CustomerRepository` (customers), `OrderRepository` (orders), with `SampleDataSeeder` ([Order_Processing/Data](Order_Processing/Data)).
- **Collections**: `Dictionary<int, T>` for quick lookups (catalog/customers/orders), `List<OrderItem>` for items, `List<OrderStatusLog>` for history.
- **Delegates**: Multicast notification on status change. Signature: `Notify(int orderId, OrderStatus newStatus)` via `INotificationSubscriber` implementations attached in service.
- **Rules**: Enforces `Created → Paid → Packed → Shipped → Delivered`; no shipping before paid; no delivery before shipped; cancelled orders cannot progress.
- **Output**: For each transition, prints validation result and notifies both subscribers; final report shows items, subtotal/total, current status, and full history timeline.

**Design Highlights**
- **Encapsulation & Polymorphism**:
	- Payroll: `Employee` polymorphic deductions keep `PayrollProcessor` simple and open for new types.
	- Orders: `OrderService` centralizes transition validation; `Order` encapsulates items and total calculations.
- **Composition**:
	- Orders contain multiple `OrderItem` entries; history is a `List<OrderStatusLog>` composed within `Order`.
- **Static Stub Data**:
	- Payroll employees in `EmpData`.
	- Order domain data in `CatalogRepository`, `CustomerRepository`, `OrderRepository` seeded via `SampleDataSeeder`.

**Extensibility Tips**
- Add a new employee type: implement overrides in a new class, seed via `EmpData`.
- Add a new order status: extend `OrderStatus` and update `IsValidTransition` in `OrderService`.
- Add a new notification channel: create a new `INotificationSubscriber` and subscribe it without touching core logic.

**Entry Points**
- Payroll bootstrapping: [PayRoll_System/Program.cs](PayRoll_System/Program.cs)
- Orders bootstrapping: [Order_Processing/Program.cs](Order_Processing/Program.cs)

**Try It Quickly**
- Payroll notifications and report:

```powershell
cd .\PayRoll_System
dotnet run
```

- Orders status changes, notifications, and final report:

```powershell
cd .\Order_Processing
dotnet run
```

**Notes**
- Target framework: net10.0. Nullable reference types enabled.
- Console output demonstrates delegate notifications and rule validations per the task sheets.
