using PayRoll_System.Models;

namespace PayRoll_System.Data
{
    // Static in-memory employee repository for testing/stubbing
    public static class EmpData
    {
        private static readonly List<Employee> _employees = new List<Employee>();

        public static IReadOnlyList<Employee> GetAll() => _employees.AsReadOnly();

        public static void Add(Employee employee)
        {
            if (employee != null)
            {
                _employees.Add(employee);
            }
        }

        public static Employee? GetById(int employeeId)
        {
            return _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        }

        public static void Clear() => _employees.Clear();

        // Optional: seed sample data for quick testing
        public static void SeedSample()
        {
            Clear();
            Add(new FullTimeEmployee(employeeId: 1, name: "Asad", monthlySalary: 8000m, bonus: 500m));
            Add(new FullTimeEmployee(employeeId: 2, name: "Abhijeet", monthlySalary: 6500m, bonus: 0m));
            Add(new FullTimeEmployee(employeeId: 3, name: "Harish", monthlySalary: 9200m, bonus: 1200m));
            Add(new ContractEmployee(employeeId: 4, name: "Vikas", dailyRate: 300m, totalDaysWorked: 20));
            Add(new ContractEmployee(employeeId: 5, name: "Avisek", dailyRate: 280m, totalDaysWorked: 22));
            Add(new ContractEmployee(employeeId: 6, name: "Varshit", dailyRate: 350m, totalDaysWorked: 18));
            // Invalid sample to demonstrate validation
            Add(new ContractEmployee(employeeId: 7, name: "Invalid Input", dailyRate: 250m, totalDaysWorked: 45));
        }
    }
}