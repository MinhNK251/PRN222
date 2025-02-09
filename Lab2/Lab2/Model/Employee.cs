using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Model
{
    public class Employee : IEmployee
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Degree { get; set; }

        public void DisplayInformationToScreen()
        {
            Console.WriteLine($"\nEmployee: {Name}\nAddress: {Address}\nEmail: {Email}\nPhone: {Phone}");
        }

        public void DisplayInformationWithDegreeToScreen()
        {
            DisplayInformationToScreen();
            Console.WriteLine($"Degree: {Degree}\n");
        }
    }
}