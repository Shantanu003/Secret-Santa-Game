using System;
using System.Collections.Generic;
using System.Linq;
using Secret_Santa_Game.Models;

namespace Secret_Santa_Game.BusinessLogic
{
    public class SecretSantaAssignmentLogic
    {
        public void AssignSecretSantas(List<Employee> employees, List<(string employeeName, string secretChildName)> previousAssignments)
        {
            var random = new Random();
            var availableEmployees = new List<Employee>(employees);

            foreach (var employee in employees)
            {
                Employee assignedChild;
                do
                {
                    assignedChild = availableEmployees[random.Next(availableEmployees.Count)];
                }
                while (assignedChild == employee || previousAssignments.Contains((employee.Employee_Name, assignedChild.Employee_Name)));

                employee.SecretChild = assignedChild;
                availableEmployees.Remove(assignedChild);
            }
        }
    }
}
