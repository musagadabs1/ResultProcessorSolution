using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResultProcessor.Data;
using ResultProcessor.Models;

namespace ResultProcessor.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Department.Include(d => d.Faculty).Where(d => d.IsActive==true);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .Include(d => d.Faculty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            //ViewBag.FacultyId
            //ViewData["FacultyId"] = new SelectList(_context.Faculty, "Id", "FacultyName");
            ViewBag.FacultyId = new SelectList(_context.Faculty, "Id", "FacultyName");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DeptName,DateCreated,CreatedBy,IsActive,FacultyId,ModifiedBy,ModifiedDate")] Department department)
        {
            if (ModelState.IsValid)
            {
                var createdDate = DateTime.Now;
                var createdUser = User.Identity.Name;
                department.CreatedBy = createdUser;
                department.DateCreated = createdDate;
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["FacultyId"] = new SelectList(_context.Faculty, "Id", "FacultyName", department.Faculty.FacultyName);
            ViewBag.FacultyId = new SelectList(_context.Faculty, "Id", "FacultyName", department.FacultyId);
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            //ViewData["FacultyId"] = new SelectList(_context.Faculty, "Id", "FacultyName", department.Faculty.FacultyName);
            ViewBag.FacultyId = new SelectList(_context.Faculty, "Id", "FacultyName", department.FacultyId);
            //ViewBag.FacultyId
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DeptName,DateCreated,CreatedBy,IsActive,FacultyId,ModifiedBy,ModifiedDate")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var createdDate = DateTime.Now;
                    var createdUser = User.Identity.Name;
                    department.ModifiedBy = createdUser;
                    department.ModifiedDate = createdDate;
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            //ViewData["FacultyId"] = new SelectList(_context.Faculty, "Id", "FacultyName", department.Faculty.FacultyName);
            ViewBag.FacultyId = new SelectList(_context.Faculty, "Id", "FacultyName", department.FacultyId);
            //ViewBag.FacultyId
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .Include(d => d.Faculty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Department.FindAsync(id);
            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.Id == id);
        }
    }
}
