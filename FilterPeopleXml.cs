using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;

public class PersonFilter
{
    public static string FilterPeopleFromXml(string xmlData)
    {
        if (string.IsNullOrWhiteSpace(xmlData))
            return "{}";

        var people = ParseXml(xmlData);

        var filtered = people
            .Where(p => p.Age > 30
                        && p.Department == "IT"
                        && p.Salary > 5000
                        && p.HireDate < new DateTime(2019, 1, 1))
            .OrderBy(p => p.Name)
            .ToList();

        if (!filtered.Any())
            return "{}";

        var result = new
        {
            Names = filtered.Select(p => p.Name).ToList(),
            TotalSalary = filtered.Sum(p => p.Salary),
            AverageSalary = (int)filtered.Average(p => p.Salary),
            MaxSalary = filtered.Max(p => p.Salary),
            Count = filtered.Count
        };

        return JsonSerializer.Serialize(result);
    }

    private static List<Person> ParseXml(string xmlData)
    {
        var xdoc = XDocument.Parse(xmlData);
        return xdoc.Descendants("Person")
            .Select(p => new Person
            {
                Name = p.Element("Name")?.Value ?? string.Empty,
                Age = int.Parse(p.Element("Age")?.Value ?? "0"),
                Department = p.Element("Department")?.Value ?? string.Empty,
                Salary = int.Parse(p.Element("Salary")?.Value ?? "0"),
                HireDate = DateTime.Parse(
                    p.Element("HireDate")?.Value ?? DateTime.MinValue.ToString(),
                    CultureInfo.InvariantCulture)
            })
            .ToList();
    }

    private class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Department { get; set; } = "";
        public int Salary { get; set; }
        public DateTime HireDate { get; set; }
    }
}

class Program
{
    static void Main(string[] args)
    {
        string xml1 = @"
        <People>
          <Person>
            <Name>Ali</Name><Age>35</Age><Department>IT</Department>
            <Salary>6000</Salary><HireDate>2018-06-01</HireDate>
          </Person>
          <Person>
            <Name>Ayşe</Name><Age>28</Age><Department>HR</Department>
            <Salary>4500</Salary><HireDate>2020-04-15</HireDate>
          </Person>
        </People>";

        Console.WriteLine(PersonFilter.FilterPeopleFromXml(xml1));

        string xml2 = @"
        <People>
          <Person>
            <Name>Mehmet</Name><Age>40</Age><Department>IT</Department>
            <Salary>7500</Salary><HireDate>2017-02-01</HireDate>
          </Person>
        </People>";

        Console.WriteLine(PersonFilter.FilterPeopleFromXml(xml2));

        string xml3 = @"
        <People>
            <Person>
                <Name>Zeynep</Name>
                <Age>45</Age>
                <Department>IT</Department>
                <Salary>9000</Salary>
                <HireDate>2010-01-10</HireDate>
            </Person>
            <Person>
                <Name>Ahmet</Name>
                <Age>50</Age>
                <Department>IT</Department>
                <Salary>8000</Salary>
                <HireDate>2015-05-20</HireDate>
            </Person>
        </People>";

        Console.WriteLine(PersonFilter.FilterPeopleFromXml(xml3));

        string xml4 = @"
        <People>
            <Person>
                <Name>Fatma</Name>
                <Age>33</Age>
                <Department>Finance</Department>
                <Salary>6000</Salary>
                <HireDate>2018-11-01</HireDate>
            </Person>
        </People>";

        Console.WriteLine(PersonFilter.FilterPeopleFromXml(xml4));

        string xml5 = @"
        <People>
            <Person>
                <Name>Selim</Name>
                <Age>32</Age>
                <Department>IT</Department>
                <Salary>5500</Salary>
                <HireDate>2018-08-05</HireDate>
            </Person>
        </People>";

        Console.WriteLine(PersonFilter.FilterPeopleFromXml(xml5));
    }
}
