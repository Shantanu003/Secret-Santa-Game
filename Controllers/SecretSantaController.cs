using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Secret_Santa_Game.Models;
using Secret_Santa_Game.BusinessLogic;
using CsvHelper;
using System.Globalization;

namespace Secret_Santa_Game.Controllers
{
    [Route("/")]
    [Route("[controller]")]
    public class SecretSantaController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
        
        [HttpPost] 
        public IActionResult UploadFiles(IFormFile employeeFile, IFormFile previousAssignmentsFile)
        {
            if (employeeFile == null || previousAssignmentsFile == null)
            {
                ViewBag.ErrorMessage = "Please upload both files.";
                return View("Index");
            }

            try
            {
                List<Employee> employees = ParseEmployeesFile(employeeFile);
                List<(string employeeName, string secretChildName)> previousAssignments = ParsePreviousAssignmentsFile(previousAssignmentsFile);

                var secretSantaAssigner = new SecretSantaAssignmentLogic();
                secretSantaAssigner.AssignSecretSantas(employees, previousAssignments);

                return View("Results", employees); 
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Index");
            }
        }

        private List<Employee> ParseEmployeesFile(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Employee>().ToList();
        }

        private List<(string employeeName, string secretChildName)> ParsePreviousAssignmentsFile(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<dynamic>()
                .Select(record => ((string)record.Employee_Name, (string)record.Secret_Child_Name))
                .ToList();
        }
    }
}
