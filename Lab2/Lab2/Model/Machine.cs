using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Model
{
    public class Machine
    {
        public string Name { get; set; }
        public Machine(string name)
        {
            Name = name;
        }
        public void UseMachine(IPerson user)
        {
            if (Name == "Fax Machine" || Name == "Printer")
            {
                if (user is IEmployee)
                    Console.WriteLine($"{user.Name} can use {Name}");
                else
                    Console.WriteLine($"{user.Name} cannot use {Name}");
            }
            else
            {
                Console.WriteLine($"{user.Name} can use {Name}");
            }
        }
    }
}
