using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string sortOrder, string search)
        {
            if (search != null && sortOrder == null)
                return View(await _context.Employee.Where(x => x.LastName.StartsWith(search)).ToListAsync());
            else
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                ViewBag.EndDateSortParm = sortOrder == "EndDate" ? "end_date_desc" : "EndDate";
            var employees = from e in _context.Employee
                            select e;
                switch (sortOrder)
                {
                    case "name_desc":
                        employees = employees.OrderByDescending(e => e.LastName);
                        break;
                    case "Date":
                        employees = employees.OrderBy(e => e.StartDate);
                        break;
                    case "date_desc":
                        employees = employees.OrderByDescending(e => e.StartDate);
                        break;
                    case "EndDate":
                        employees = employees.OrderBy(e => e.EndDate);
                        break;
                    case "end_date_desc":
                        employees = employees.OrderByDescending(e => e.EndDate);
                        break;
                    default:
                        employees = employees.OrderBy(e => e.LastName);
                        break;
                }
                return View(await employees.ToListAsync());
        }

        // GET: Employees/Filter
        public async Task<IActionResult> Filter(string status, string department, string shift)
        {

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Status", Value = "" });

            items.Add(new SelectListItem { Text = "Hired", Value = "0" });

            items.Add(new SelectListItem { Text = "Quit", Value = "1" });

            items.Add(new SelectListItem { Text = "Contract", Value = "2" });

            ViewBag.Status = items;

            List<SelectListItem> deptItems = new List<SelectListItem>();

            deptItems.Add(new SelectListItem { Text = "Department", Value = "" });

            deptItems.Add(new SelectListItem { Text = "IT", Value = "0" });

            deptItems.Add(new SelectListItem { Text = "Marketing", Value = "1" });

            deptItems.Add(new SelectListItem { Text = "Customer Service", Value = "2" });

            deptItems.Add(new SelectListItem { Text = "Sales", Value = "3" });

            deptItems.Add(new SelectListItem { Text = "Human Resources", Value = "4" });

            deptItems.Add(new SelectListItem { Text = "Facilities", Value = "5" });

            deptItems.Add(new SelectListItem { Text = "Training", Value = "6" });

            ViewBag.Department = deptItems;

            List<SelectListItem> shiftItems = new List<SelectListItem>();

            shiftItems.Add(new SelectListItem { Text = "Shift", Value = "" });

            shiftItems.Add(new SelectListItem { Text = "Day", Value = "0" });

            shiftItems.Add(new SelectListItem { Text = "Swing", Value = "1" });

            shiftItems.Add(new SelectListItem { Text = "Grave", Value = "2" });

            ViewBag.Shift = shiftItems;

            if (status == null && shift == null && department == null)
                return View(await _context.Employee.ToListAsync());
            else if (status != null && shift == null && department == null)
                switch (status)
                {
                    case "0":
                        return View(await _context.Employee.Where(x => x.EmployeeStatus == "Hired").ToListAsync());
                    case "1":
                        return View(await _context.Employee.Where(x => x.EmployeeStatus == "Quit").ToListAsync());
                    case "2":
                        return View(await _context.Employee.Where(x => x.EmployeeStatus == "Contract").ToListAsync());
                    default:
                        return View(await _context.Employee.Where(x => x.EmployeeStatus == status).ToListAsync());
                }
            else if (status == null && shift == null && department != null)
                switch (department)
                {
                    case "0":
                        return View(await _context.Employee.Where(x => x.Department == "IT").ToListAsync());
                    case "1":
                        return View(await _context.Employee.Where(x => x.Department == "Marketing").ToListAsync());
                    case "2":
                        return View(await _context.Employee.Where(x => x.Department == "Customer Service").ToListAsync());
                    case "3":
                        return View(await _context.Employee.Where(x => x.Department == "Sales").ToListAsync());
                    case "4":
                        return View(await _context.Employee.Where(x => x.Department == "Human Resources").ToListAsync());
                    case "5":
                        return View(await _context.Employee.Where(x => x.Department == "Facilities").ToListAsync());
                    case "6":
                        return View(await _context.Employee.Where(x => x.Department == "Training").ToListAsync());
                    default:
                        return View(await _context.Employee.Where(x => x.Department == department).ToListAsync());
                }
            
            else if (status == null && shift != null && department == null)
                switch (shift)
                {
                    case "0":
                        return View(await _context.Employee.Where(x => x.Shift == "Day").ToListAsync());
                    case "1":
                        return View(await _context.Employee.Where(x => x.Shift == "Swing").ToListAsync());
                    case "2":
                        return View(await _context.Employee.Where(x => x.Shift == "Graveyard").ToListAsync());
                    default:
                        return View(await _context.Employee.Where(x => x.Shift == shift).ToListAsync());
                }
            else
                return View(await _context.Employee.ToListAsync());

        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .SingleOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,StartDate,EndDate,Address,EmailAddress,PhoneNumber,EmployeeStatus,Department,Shift,Manager,ImageFileName,FavoriteColor")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.SingleOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMidName,StartDate,EndDate,Address,EmailAddress,PhoneNumber,EmployeeStatus,Department,Shift,Manager,ImageFileName,FavoriteColor")] Employee employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .SingleOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.SingleOrDefaultAsync(m => m.ID == id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.ID == id);
        }
    }
}
