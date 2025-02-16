using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secret_Santa_Game.Models
{
    public class Employee
    {
        public string Employee_Name { get; set; }
        public string Employee_EmailID { get; set; }
        public Employee? SecretChild { get; set; } 
    }


}