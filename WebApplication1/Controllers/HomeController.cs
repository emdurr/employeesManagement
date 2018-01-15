using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeContext _context;

        public HomeController(EmployeeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Reports()
        {
            DayOfWeek weekStart = DayOfWeek.Sunday;
            DateTime startingDate = DateTime.Today;

            while (startingDate.DayOfWeek != weekStart)
                startingDate = startingDate.AddDays(-1);

            DateTime previousWeekStart = startingDate.AddDays(-7);
            DateTime previousWeekEnd = startingDate.AddDays(-1);
            DateTime previous1WeekStart = startingDate.AddDays(-14);
            DateTime previous1WeekEnd = startingDate.AddDays(-8);
            DateTime previous2WeekStart = startingDate.AddDays(-21);
            DateTime previous2WeekEnd = startingDate.AddDays(-15);
            DateTime previous3WeekStart = startingDate.AddDays(-28);
            DateTime previous3WeekEnd = startingDate.AddDays(-22);
            var previousWeekEmp = _context.Employee.Where(x => (x.StartDate >= previousWeekEnd && x.StartDate <= previousWeekStart));
            var previous1WeekEmp = _context.Employee.Where(x => (x.StartDate >= previous1WeekEnd && x.StartDate <= previous1WeekStart));
            var previous2WeekEmp = _context.Employee.Where(x => (x.StartDate >= previous2WeekEnd && x.StartDate <= previous2WeekStart));
            var previous3WeekEmp = _context.Employee.Where(x => (x.StartDate >= previous3WeekEnd && x.StartDate <= previous3WeekStart));
            ViewData["Year"] = startingDate.Year;
            ViewData["TermEmployeeCount"] = _context.Employee.Where(x => (x.EndDate.Year == startingDate.Year && x.EmployeeStatus == "Terminated")).Count();
            ViewData["EmployeeCount"] = previousWeekEmp.Count();
            ViewData["Employee1Count"] = previous1WeekEmp.Count();
            ViewData["Employee2Count"] = previous2WeekEmp.Count();
            ViewData["Employee3Count"] = previous3WeekEmp.Count();
            ViewData["Manager"] = _context.Permissions.Where(x => x.Type == Models.Type.Manager).Count();
            ViewData["TeamLead"] = _context.Permissions.Where(x => x.Type == Models.Type.TeamLead).Count();
            ViewData["Director"] = _context.Permissions.Where(x => x.Type == Models.Type.Director).Count();
            ViewData["HR"] = _context.Permissions.Where(x => x.Type == Models.Type.HR).Count();
            ViewData["EmailAccess"] = _context.Permissions.Where(x => x.Type == Models.Type.EmailAccess).Count();
            ViewData["Key"] = _context.Permissions.Where(x => x.Type == Models.Type.Key).Count();
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
