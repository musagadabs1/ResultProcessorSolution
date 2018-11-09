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
    [Authorize(Roles="Admin")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Course.Include(c => c.Programme).Where(c => c.IsActive==true);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.Programme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "ProgrammeName");

            var Semester = new List<SelectListItem>
                {
                    new SelectListItem {Text = "First", Value = "First"},
                    new SelectListItem {Text = "Second", Value = "Second"}
                };

            ViewBag.Semester = Semester;
            var level = new List<SelectListItem>
            {
                new SelectListItem {Text="level I", Value = "level I"},
                new SelectListItem {Text="level II", Value = "level II"},
                new SelectListItem {Text="level III", Value = "level III"},
                new SelectListItem {Text="level IV", Value = "level IV"},
                new SelectListItem {Text="level V", Value = "level V"},
                new SelectListItem {Text="level SPILL I", Value = "level SPILL I"},
                new SelectListItem {Text="level SPILL II", Value = "level SPILL II"},
            };
            ViewBag.Level = level;
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Code,Unit,ProgrammeId,Semester,Level,IsActive,DateCreated,CreatedBy,ModifiedBy,ModifiedDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                var createdBy = User.Identity.Name;
                var createdDate = DateTime.Now;
                course.CreatedBy = createdBy;
                course.DateCreated = createdDate;
                course.Code = course.Code.ToUpper();
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "ProgrammeName", course.ProgrammeId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            var Semester = new List<SelectListItem>
                {
                    new SelectListItem {Text = "First", Value = "First"},
                    new SelectListItem {Text = "Second", Value = "Second"}
                };

            ViewBag.Semester = Semester;
            var level = new List<SelectListItem>
            {
                new SelectListItem {Text="level I", Value = "level I"},
                new SelectListItem {Text="level II", Value = "level II"},
                new SelectListItem {Text="level III", Value = "level III"},
                new SelectListItem {Text="level IV", Value = "level IV"},
                new SelectListItem {Text="level V", Value = "level V"},
                new SelectListItem {Text="level SPILL I", Value = "level SPILL I"},
                new SelectListItem {Text="level SPILL II", Value = "level SPILL II"},
            };
            ViewBag.Level = level;
            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "ProgrammeName", course.ProgrammeId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Code,Unit,ProgrammeId,Semester,Level,IsActive,DateCreated,CreatedBy,ModifiedBy,ModifiedDate")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var createdBy = User.Identity.Name;
                    var createdDate = DateTime.Now;
                    course.ModifiedBy = createdBy;
                    course.ModifiedDate = createdDate;
                    course.Code = course.Code.ToUpper();
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "ProgrammeName", course.ProgrammeId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.Programme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }
    }
}
