using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class EmployeeAnalyzer
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        if (employees == null || !employees.Any())
        {
            return "{}";
        }

        var filtered = employees
            .Where(e =>
                e.Age >= 25 && e.Age <= 40 &&
                (e.Department == "IT" || e.Department == "Finance") &&
                e.Salary >= 5000 && e.Salary <= 9000 &&
                e.HireDate > new DateTime(2017, 1, 1))
            .OrderByDescending(e => e.Name.Length)
            .ThenBy(e => e.Name)
            .ToList();

        if (!filtered.Any())
        {
            return JsonSerializer.Serialize(new
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MinSalary = 0,
                MaxSalary = 0,
                Count = 0
            });
        }

        var result = new
        {
            Names = filtered.Select(e => e.Name).ToList(),
            TotalSalary = filtered.Sum(e => e.Salary),
            AverageSalary = Math.Round(filtered.Average(e => e.Salary), 2),
            MinSalary = filtered.Min(e => e.Salary),
            MaxSalary = filtered.Max(e => e.Salary),
            Count = filtered.Count
        };

        return JsonSerializer.Serialize(result);
    }

    static void Main()
    {
        var employees1 = new List<(string, int, string, decimal, DateTime)>
        {
            ("Ali", 30, "IT", 6000m, new DateTime(2018, 5, 1)),
            ("Ayse", 35, "Finance", 8500m, new DateTime(2019, 3, 15)),
            ("Veli", 28, "IT", 7000m, new DateTime(2020, 1, 1))
        };
        Console.WriteLine(EmployeeAnalyzer.FilterEmployees(employees1));

        var employees2 = new List<(string, int, string, decimal, DateTime)>
        {
            ("Mehmet", 26, "Finance", 5000m, new DateTime(2021, 7, 1)),
            ("Zeynep", 39, "IT", 9000m, new DateTime(2018, 11, 20))
        };
        Console.WriteLine(EmployeeAnalyzer.FilterEmployees(employees2));

        var employees3 = new List<(string, int, string, decimal, DateTime)>
        {
            ("Burak", 41, "IT", 6000m, new DateTime(2018, 6, 1))
        };
        Console.WriteLine(EmployeeAnalyzer.FilterEmployees(employees3));

        var employees4 = new List<(string, int, string, decimal, DateTime)>
        {
            ("Canan", 29, "Finance", 8000m, new DateTime(2019, 9, 1)),
            ("Okan", 35, "IT", 7500m, new DateTime(2020, 5, 10))
        };
        Console.WriteLine(EmployeeAnalyzer.FilterEmployees(employees4));

        var employees5 = new List<(string, int, string, decimal, DateTime)>
        {
            ("Elif", 27, "Finance", 6500m, new DateTime(2017, 12, 31))
        };
        Console.WriteLine(EmployeeAnalyzer.FilterEmployees(employees5));
    }
}