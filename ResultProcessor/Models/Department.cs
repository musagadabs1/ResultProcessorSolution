using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultProcessor.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DeptName { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public int FacultyId { get; set; }

        public virtual Faculty Faculty { get; set; }
        //public virtual ICollection<Student> Students { get; set; }
        //public virtual ICollection<ScoreSheet> ScoreSheets { get; set; }
    }
}
