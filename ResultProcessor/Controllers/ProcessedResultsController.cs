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

        //private async 

        //Get Previous CGPA
        public double GetPreviousCGPA(string regNo)
        {
            double previousCGPA = 0.0d;
            try
            {
                var getTotalCreditUnits = _context.ProcessedResult.Where(r => r.RegNo == regNo).Select(r => r.TotalCreditUnit);
                
                var getTotalGradePoints =_context.ProcessedResult.Where(a => a.RegNo == regNo).Select(a => a.TotalGradePoint);

                // Test if there is no previous credit units and Grade Points. Return 0 if not
                if (getTotalCreditUnits.Count() <= 0 || getTotalGradePoints.Count() <= 0)
                {
                    return 0.0d;
                }

                // Find the summation of all credit units and grade points
                int totalCreditUnit = getTotalCreditUnits.Sum();
                double totalGradePoint = getTotalGradePoints.Sum();

                //Determine the previous CGPA by dividing the total Grade Point by Total Credit Units
                previousCGPA = (double)totalGradePoint / totalCreditUnit;
                return Math.Round(previousCGPA, 2);
            }
            catch (Exception enEx)
            {
                throw enEx;
            }

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
            ViewData["RegNo"] = new SelectList(_context.Student, "RegNo", "RegNo");
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
                new SelectListItem {Text="level SPILL II", Value = "level SPILL II"}
            };
            ViewBag.Level = level;
            var Sessions = new List<SelectListItem>
            {
                new SelectListItem {Text="2018/2019", Value = "2018/2019"},
                new SelectListItem {Text="2019/2020", Value = "2019/2020"},
                new SelectListItem {Text="2020/2021", Value = "2020/2021"},
                new SelectListItem {Text="2021/2022", Value = "2021/2022"},
                new SelectListItem {Text="2022/2023", Value = "2022/2023"},
                new SelectListItem {Text="2023/2024", Value = "2023/2024"},
                new SelectListItem {Text="2024/2025", Value = "2024/2025"}
            };
            ViewBag.Sessions = Sessions;

            return View();
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

                try
                {
                    double gradePointAverage = 0.0F;
                    double cummulativeGradePointAverage = 0.0F;
                    var regNo = string.Empty;
                    var sem = processedResult.Semester;
                    var sess = processedResult.Session;
                    var level = processedResult.Level;
                    var totalCreditUnit = 0;
                    var totalGradePoint = 0.0;
                    var enteredRegNo = "";

                    List<string> getReNos= await GetRegNos(sem,sess,level);

                    for (var index=0; index<= getReNos.Count-1;index++)
                    {

                        regNo = getReNos[index];

                        if (regNo==enteredRegNo)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        
                        
                        int id = index + 1;
                        
                        List<ScoreViewModel> scoreViews =await ScoresAndUnit(regNo, sem, sess, level);
                        foreach (var s in scoreViews)
                        {

                            var unit = s.Unit;
                            //var score = s.Score;
                            var grade = s.Grade;
                            
                            totalCreditUnit += unit;
                            var gradePoint = Utility.GradePoint(grade,unit);

                            totalGradePoint += gradePoint;
                        }
                        //Retrieve the previous GPA from database by RegNo
                        double previousGPA = GetPreviousCGPA(regNo);

                        gradePointAverage = totalGradePoint / totalCreditUnit;
                        gradePointAverage = Math.Round(gradePointAverage, 2);

                        // Determine new Semester
                        if (previousGPA == 0.0)
                        {
                            cummulativeGradePointAverage = gradePointAverage;
                        }
                        else
                        {
                            // Compute the Cummulative Grade Point Average by adding the previous GPA to current GPA and divide by 2
                            cummulativeGradePointAverage = (gradePointAverage + previousGPA) / 2;
                        }

                        //cummulativeGradePointAverage = gpa;

                        var processedBy = User.Identity.Name;
                        var processedDate = DateTime.Now;

                        processedResult.GPA = gradePointAverage;
                        processedResult.CGPA = cummulativeGradePointAverage;

                        processedResult.DateProcessed = processedDate;
                        processedResult.ProcessedBy = processedBy;
                        processedResult.TotalCreditUnit = totalCreditUnit;
                        processedResult.TotalGradePoint =Math.Round(totalGradePoint);
                        processedResult.RegNo = regNo;
                        processedResult.Id = id;
                        _context.Add(processedResult);
                        await _context.SaveChangesAsync();
                        enteredRegNo = regNo;
                        totalCreditUnit = 0;
                        totalGradePoint = 0.0;

                    }
                   
                    
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Something happened. " + Environment.NewLine + ex.Message;

                    throw ex;
                }
            }
            return View(processedResult);
        }

        private async Task<List<ScoreViewModel>> ScoresAndUnit(string regNo, string semester, string session, string level)
        {
            List<ScoreViewModel> scoresWithUnit = new List<ScoreViewModel>();
            List<Processed> scores =await GetScoreSheets(semester, session, level);
            scores =scores.Where(s => s.RegNo == regNo).ToList();
            foreach (var item in scores)
            {
                scoresWithUnit.Add(new ScoreViewModel
                {
                    //Score=item.Score,
                    Grade=item.Grade,
                    Unit=item.CourseUnit
                });
            }
            return scoresWithUnit;
        }
        private async Task<List<string>> GetRegNos(string semester,string session, string level)
        {
            try
            {
                List<string> regNoCollection = new List<string>();

                var reg =await (from re in _context.ScoreSheet where re.Session==session && re.Semester==semester && re.Level==level select re.RegNo).Distinct().ToListAsync();
               foreach (var item in reg)
                {
                    regNoCollection.Add(item);
                }
                return regNoCollection;
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
                        CourseUnit = item.Course.Unit

                    });
                }
                return processeds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
