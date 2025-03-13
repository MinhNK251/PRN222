using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Models
{
    public class AdminAccountSettings
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}
