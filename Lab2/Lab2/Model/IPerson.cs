using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Model
{
    public interface IPerson
    {
        string Name { get; set; }
        string Address { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        void DisplayInformationToScreen();
    }

    public interface ICustomer : IPerson
    {
        string Type { get; set; }
        void DisplayInformationWithTypeToScreen();
    }

    public interface IEmployee : IPerson
    {
        string Degree { get; set; }
        void DisplayInformationWithDegreeToScreen();
    }
}