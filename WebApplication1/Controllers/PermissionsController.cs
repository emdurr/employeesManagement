﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly EmployeeContext _context;

        public PermissionsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Permissions
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.Permissions.Include(p => p.Employee);
            return View(await employeeContext.ToListAsync());
        }

        // GET: Permissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissions = await _context.Permissions
                .Include(p => p.Employee)
                .SingleOrDefaultAsync(m => m.EmployeeID == id);
            if (permissions == null)
            {
                return NotFound();
            }

            return View(permissions);
        }

        // GET: Permissions/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "ID", "FullName");
            ViewData["Permissions"] = new SelectList(_context.Permissions, "Type");
            return View();
        }

        // POST: Permissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,Type,DateAdded,DateRemoved")] Permissions permissions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permissions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "ID", "FirstMidName", permissions.EmployeeID);
            return View(permissions);
        }

        // GET: Permissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissions = await _context.Permissions.SingleOrDefaultAsync(m => m.EmployeeID == id);
            if (permissions == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "ID", "FirstMidName", permissions.EmployeeID);
            return View(permissions);
        }

        // POST: Permissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,Type,DateAdded,DateRemoved")] Permissions permissions)
        {
            if (id != permissions.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permissions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionsExists(permissions.EmployeeID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "ID", "FirstMidName", permissions.EmployeeID);
            return View(permissions);
        }

        // GET: Permissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissions = await _context.Permissions
                .Include(p => p.Employee)
                .SingleOrDefaultAsync(m => m.EmployeeID == id);
            if (permissions == null)
            {
                return NotFound();
            }

            return View(permissions);
        }

        // POST: Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permissions = await _context.Permissions.SingleOrDefaultAsync(m => m.EmployeeID == id);
            _context.Permissions.Remove(permissions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PermissionsExists(int id)
        {
            return _context.Permissions.Any(e => e.EmployeeID == id);
        }
    }
}
