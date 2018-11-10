using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResultProcessor.Data;
using ResultProcessor.Models;

namespace ResultProcessor.Controllers
{
    [Authorize(Roles="Admin,Lecturer,User")]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentsController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {

            if (_signInManager.IsSignedIn(User))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var isAdmin = currentUser != null && await _userManager.IsInRoleAsync(currentUser, Constants.Administrator);

                if (isAdmin)
                {
                    var students = _context.Student.Include(s => s.Programme).Where(s => s.IsActive == true );
                    return View(await students.ToListAsync());
                }
                else
                {
                    var user = HttpContext.User.Identity.Name;

                    var applicationDbContext = _context.Student.Include(s => s.Programme).Where(s => s.IsActive == true && (s.EnteredBy == user || s.ModifiedBy == user));
                    return View(await applicationDbContext.ToListAsync());
                }

                
            }
            return View();
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Programme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "ProgrammeName");
            var Genders = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Male", Value = "Male"},
                    new SelectListItem {Text = "Female", Value = "Female"}
                };

            ViewBag.Genders = Genders;
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,RegNo,DOB,DOAdmission,Gender,PhoneNumber,Email,DateEntered,EnteredBy,IsActive,ProgrammeId,ModifiedBy,ModifiedDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                var dateEntered = DateTime.Now;
                var enteredBy = User.Identity.Name;

                student.EnteredBy = enteredBy;
                student.DateEntered = dateEntered;
                student.RegNo = student.RegNo.ToUpper();
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "ProgrammeName", student.ProgrammeId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "ProgrammeName", student.ProgrammeId);
            var Genders = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Male", Value = "Male"},
                    new SelectListItem {Text = "Female", Value = "Female"}
                };

            ViewBag.Genders = Genders;
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,RegNo,DOB,DOAdmission,Gender,PhoneNumber,Email,DateEntered,EnteredBy,IsActive,ProgrammeId,ModifiedBy,ModifiedDate")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dateEntered = DateTime.Now;
                    var enteredBy = User.Identity.Name;
                    if (enteredBy == null) 
                    {
                        Challenge();
                    }

                    student.ModifiedBy = enteredBy;
                    student.ModifiedDate = dateEntered;

                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "ProgrammeName", student.ProgrammeId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Programme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
