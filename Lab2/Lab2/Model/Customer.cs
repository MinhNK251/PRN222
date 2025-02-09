using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Model
{
    public class Customer : ICustomer
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }

        public void DisplayInformationToScreen()
        {
            Console.WriteLine($"\nCustomer: {Name}\nAddress: {Address}\nEmail: {Email}\nPhone: {Phone}");
        }

        public void DisplayInformationWithTypeToScreen()
        {
            DisplayInformationToScreen();
            Console.WriteLine($"Customer Type: {Type}\n");
        }
    }
}
