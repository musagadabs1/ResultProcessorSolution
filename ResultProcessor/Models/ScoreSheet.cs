using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultProcessor.Models
{
    public class ScoreSheet
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int DeptId { get; set; }
        public int CourseId { get; set; }
        public float Score { get; set; }
        public Semester Semester { get; set; }
        public Level Level { get; set; }
        public Grade Grade { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public int ProgrammeId { get; set; }


        public virtual Programme Programme { get; set; }
        public virtual Department Department { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
