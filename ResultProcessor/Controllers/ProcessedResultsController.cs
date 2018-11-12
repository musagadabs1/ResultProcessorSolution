using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResultProcessor.Data;
using ResultProcessor.Models;
using ResultProcessor.ViewModels;

namespace ResultProcessor.Controllers
{
    public class ProcessedResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProcessedResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProcessedResults
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProcessedResult.ToListAsync());
        }

        // GET: ProcessedResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processedResult = await _context.ProcessedResult
                .FirstOrDefaultAsync(m => m.Id == id);
            if (processedResult == null)
            {
                return NotFound();
            }

            return View(processedResult);
        }

        // GET: ProcessedResults/Create
        public IActionResult Create()
        {
            return View();
        }
        private async Task<List<Processed>> GetScoreSheets(string semester, string session, string level)
        {
            try
            {
                List<Processed> processeds = new List<Processed>();

                var scoresheet = await _context.ScoreSheet.Include("Course").Where(s => s.Session == session || s.Semester == semester || s.Level == level).ToListAsync();
                foreach (var item in scoresheet)
                {
                    processeds.Add(new Processed
                    {
                        RegNo = item.RegNo,
                        Grade = item.Grade,
                        Score = item.Score,
                        CourseUnit=item.Course.Unit
                        
                    });
                }
                return processeds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        // POST: ProcessedResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegNo,Semester,Session,Level,GPA,CGPA,ProcessedBy,DateProcessed")] ProcessedResult processedResult)
        {
            if (ModelState.IsValid)
            {


                var processedBy = User.Identity.Name;
                var processedDate = DateTime.Now;
                processedResult.DateProcessed = processedDate;
                processedResult.ProcessedBy = processedBy;
                _context.Add(processedResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(processedResult);
        }

        //private 

        // GET: ProcessedResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processedResult = await _context.ProcessedResult.FindAsync(id);
            if (processedResult == null)
            {
                return NotFound();
            }
            return View(processedResult);
        }

        // POST: ProcessedResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegNo,Semester,Session,Level,GPA,CGPA,ModifiedBy,ModifiedDate")] ProcessedResult processedResult)
        {
            if (id != processedResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(processedResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcessedResultExists(processedResult.Id))
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
            return View(processedResult);
        }

        // GET: ProcessedResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processedResult = await _context.ProcessedResult
                .FirstOrDefaultAsync(m => m.Id == id);
            if (processedResult == null)
            {
                return NotFound();
            }

            return View(processedResult);
        }

        // POST: ProcessedResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var processedResult = await _context.ProcessedResult.FindAsync(id);
            _context.ProcessedResult.Remove(processedResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcessedResultExists(int id)
        {
            return _context.ProcessedResult.Any(e => e.Id == id);
        }
    }
}
