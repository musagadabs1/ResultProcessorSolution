using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultProcessor.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOAdmission { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public int DeptId { get; set; }
        public int ProgrammeId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Programme Programme { get; set; }

        //public virtual ICollection<ScoreSheet> ScoreSheets { get; set; }
    }
}
