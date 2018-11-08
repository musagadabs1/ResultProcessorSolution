using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultProcessor.Models
{
    public class Programme
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string ProgrammeName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual Department Department { get; set; }
        //public virtual ICollection<ScoreSheet> ScoreSheets { get; set; }



    }
}
