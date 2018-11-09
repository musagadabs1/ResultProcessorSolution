using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResultProcessor.Data;
using ResultProcessor.Models;

namespace ResultProcessor.Controllers
{
    public class ScoreSheetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScoreSheetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ScoreSheets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ScoreSheet.Include(s => s.Course).Include(s => s.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ScoreSheets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scoreSheet = await _context.ScoreSheet
                .Include(s => s.Course)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scoreSheet == null)
            {
                return NotFound();
            }

            return View(scoreSheet);
        }

        // GET: ScoreSheets/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Code");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FirstName");
            var Semester = new List<SelectListItem>
                {
                    new SelectListItem {Text = "First", Value = "1"},
                    new SelectListItem {Text = "Second", Value = "2"}
                };

            ViewBag.Semester = Semester;
            var level = new List<SelectListItem>
            {
                new SelectListItem {Text="level I", Value = "1"},
                new SelectListItem {Text="level II", Value = "2"},
                new SelectListItem {Text="level III", Value = "3"},
                new SelectListItem {Text="level IV", Value = "4"},
                new SelectListItem {Text="level V", Value = "5"},
                new SelectListItem {Text="level SPILL I", Value = "6"},
                new SelectListItem {Text="level SPILL II", Value = "7"},
            };
            ViewBag.Level = level;
            var Grades = new List<SelectListItem>
            {
                new SelectListItem {Text="A", Value = "5"},
                new SelectListItem {Text="B", Value = "4"},
                new SelectListItem {Text="C", Value = "3"},
                new SelectListItem {Text="D", Value = "2"},
                new SelectListItem {Text="E", Value = "1"},
                new SelectListItem {Text="F", Value = "0"},

            };
            ViewBag.Grades = Grades;
            return View();
        }

        // POST: ScoreSheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId,Score,Semester,Level,Grade,DateEntered,EnteredBy,ModifiedBy,ModifiedDate")] ScoreSheet scoreSheet)
        {
            if (ModelState.IsValid)
            {
                var createdBy = User.Identity.Name;
                var createdDate = DateTime.Now;
                scoreSheet.EnteredBy = createdBy;
                scoreSheet.DateEntered = createdDate;
                _context.Add(scoreSheet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Code", scoreSheet.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FirstName", scoreSheet.StudentId);
            return View(scoreSheet);
        }

        // GET: ScoreSheets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scoreSheet = await _context.ScoreSheet.FindAsync(id);
            if (scoreSheet == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Code", scoreSheet.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FirstName", scoreSheet.StudentId);
            var Semester = new List<SelectListItem>
                {
                    new SelectListItem {Text = "First", Value = "1"},
                    new SelectListItem {Text = "Second", Value = "2"}
                };

            ViewBag.Semester = Semester;
            var level = new List<SelectListItem>
            {
                new SelectListItem {Text="level I", Value = "1"},
                new SelectListItem {Text="level II", Value = "2"},
                new SelectListItem {Text="level III", Value = "3"},
                new SelectListItem {Text="level IV", Value = "4"},
                new SelectListItem {Text="level V", Value = "5"},
                new SelectListItem {Text="level SPILL I", Value = "6"},
                new SelectListItem {Text="level SPILL II", Value = "7"},
            };
            ViewBag.Level = level;
            var Grades = new List<SelectListItem>
            {
                new SelectListItem {Text="A", Value = "5"},
                new SelectListItem {Text="B", Value = "4"},
                new SelectListItem {Text="C", Value = "3"},
                new SelectListItem {Text="D", Value = "2"},
                new SelectListItem {Text="E", Value = "1"},
                new SelectListItem {Text="F", Value = "0"},
                
            };
            ViewBag.Grades = Grades;
            return View(scoreSheet);
        }

        // POST: ScoreSheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CourseId,Score,Semester,Level,Grade,DateEntered,EnteredBy,ModifiedBy,ModifiedDate")] ScoreSheet scoreSheet)
        {
            if (id != scoreSheet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var createdBy = User.Identity.Name;
                    var createdDate = DateTime.Now;
                    scoreSheet.ModifiedBy = createdBy;
                    scoreSheet.ModifiedDate = createdDate;
                    _context.Update(scoreSheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScoreSheetExists(scoreSheet.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Code", scoreSheet.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FirstName", scoreSheet.StudentId);
            return View(scoreSheet);
        }

        // GET: ScoreSheets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scoreSheet = await _context.ScoreSheet
                .Include(s => s.Course)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scoreSheet == null)
            {
                return NotFound();
            }

            return View(scoreSheet);
        }

        // POST: ScoreSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scoreSheet = await _context.ScoreSheet.FindAsync(id);
            _context.ScoreSheet.Remove(scoreSheet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScoreSheetExists(int id)
        {
            return _context.ScoreSheet.Any(e => e.Id == id);
        }
    }
}
