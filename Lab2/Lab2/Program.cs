using Lab2.Model;
using System;

public class Program
{
    static void Main()
    {
        List<IPerson> users = new List<IPerson>
        {
            new Customer { Name = "Customer", Address = "S1.01", Email = "example1@gmail.com", Phone = "123456789", Type = "Regular" },
            new Employee { Name = "Employee", Address = "S2.02", Email = "example2@gmail.com", Phone = "987654321", Degree = "Technology" }
        };

        List<Machine> machines = new List<Machine>
        {
            new Machine("Photocopy Machine"),
            new Machine("Printer"),
            new Machine("Fax Machine")
        };

        Console.Write("Enter your name (customer, employee): ");
        string userName = Console.ReadLine();
        IPerson loggedInUser = users.Find(user => user.Name.Equals(userName, StringComparison.OrdinalIgnoreCase));
        if (loggedInUser != null)
        {
            if (loggedInUser is Customer customer)
            {
                customer.DisplayInformationWithTypeToScreen();
            }
            else if (loggedInUser is Employee employee)
            {
                employee.DisplayInformationWithDegreeToScreen();
            }
            foreach (var machine in machines)
            {
                machine.UseMachine(loggedInUser);
            }
        }
        else
        {
            Console.WriteLine("Invalid username");
        }
    }
}