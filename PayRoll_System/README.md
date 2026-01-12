# PayRoll_System

A simple in-memory payroll processing console app demonstrating OOP (inheritance + runtime polymorphism), generic collections, and multicast delegates for notifications.

## Features
- Employee hierarchy: `Employee` (abstract), `FullTimeEmployee`, `ContractEmployee`
- Polymorphic salary and deduction calculations without type checks
- In-memory storage using `Dictionary<int, Employee>` and `List<PaySlip>`
- Multicast delegate `SalaryProcessedDelegate` notifying HR and Finance after each employee is processed
- Summary reporting: totals, counts by type, highest net salary
- Basic validation and graceful handling of invalid input

## Quick Start
Requirements: .NET SDK installed.

```bash
dotnet run
```

## Project Structure
```
PayRoll_System/
  Program.cs
  PayRoll_System.csproj
  Delegates/
    SalaryProcessedDelegate.cs
  Models/
    Employee.cs
    FullTimeEmployee.cs
    ContractEmployee.cs
    PaySlip.cs
  Services/
    PayrollProcessor.cs
    NotificationService.cs
```

## Design Highlights
- Encapsulation: Employee fields include id, name, type, active flag, base salary, working days (and optional department/email).
- Polymorphism: `CalculateSalary()` and `CalculateDeductions(gross)` are overridden per employee type.
- Collections: Employees stored in a dictionary keyed by `EmployeeId`; payslips stored in a list (plus an index dictionary for quick lookup).
- Delegates: `SalaryProcessedDelegate(PaySlip)` with subscribers `NotifyHR` and `NotifyFinance`.

## Validation Rules
- Salary/daily rate must be non-negative
- Working days must be 0–31 inclusive
- Invalid employees are skipped with a console message

## Extensibility
To add a new type (e.g., Intern):
1. Create `InternEmployee` deriving from `Employee`
2. Implement `CalculateSalary()` and `CalculateDeductions(gross)`
3. No changes are needed in `PayrollProcessor` processing loop or delegate wiring

## Sample Output
- Per-employee processing line: gross, deductions, net
- HR and Finance notifications for each processed employee
- Final summary with totals and highest net salary


