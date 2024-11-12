using System;
using System.Collections.Generic;

public abstract class OrganizationComponent
{
    public string Name { get; set; }
    public string Position { get; set; }
    public double Salary { get; set; }

    public abstract void Add(OrganizationComponent component);
    public abstract void Remove(OrganizationComponent component);
    public abstract void DisplayHierarchy(int indent = 0);
    public abstract int GetEmployeeCount();
    public abstract double CalculateBudget();
}

public class Employee : OrganizationComponent
{
    public Employee(string name, string position, double salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
    }

    public override void Add(OrganizationComponent component)
    {
        Console.WriteLine("Невозможно добавить элемент к сотруднику.");
    }

    public override void Remove(OrganizationComponent component)
    {
        Console.WriteLine("Невозможно удалить элемент у сотрудника.");
    }

    public override void DisplayHierarchy(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + $"{Position} - {Name}, Зарплата: {Salary} тенге");
    }

    public override int GetEmployeeCount()
    {
        return 1;
    }

    public override double CalculateBudget()
    {
        return Salary;
    }
}

public class Department : OrganizationComponent
{
    private List<OrganizationComponent> _components = new List<OrganizationComponent>();

    public Department(string name)
    {
        Name = name;
    }

    public override void Add(OrganizationComponent component)
    {
        _components.Add(component);
    }

    public override void Remove(OrganizationComponent component)
    {
        _components.Remove(component);
    }

    public override void DisplayHierarchy(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + "Отдел: " + Name);
        foreach (var component in _components)
        {
            component.DisplayHierarchy(indent + 2);
        }
    }

    public override int GetEmployeeCount()
    {
        int count = 0;
        foreach (var component in _components)
        {
            count += component.GetEmployeeCount();
        }
        return count;
    }

    public override double CalculateBudget()
    {
        double totalBudget = 0;
        foreach (var component in _components)
        {
            totalBudget += component.CalculateBudget();
        }
        return totalBudget;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Employee айдос = new Employee("Айдос", "Мастер", 120000);
        Employee асем = new Employee("Бауыржан", "Инженер", 100000);
        Employee нурлан = new Employee("Нурлан", "Технический отдел", 85000);

        Department Factory = new Department("Завод");
        Factory.Add(айдос);
        Factory.Add(асем);
        Factory.Add(нурлан);

        Department mainOffice = new Department("Команда");
        mainOffice.Add(Factory);

        mainOffice.DisplayHierarchy();
        Console.WriteLine($"Общее количество сотрудников: {mainOffice.GetEmployeeCount()}");
        Console.WriteLine($"Общий бюджет: {mainOffice.CalculateBudget()} тенге");
    }
}
