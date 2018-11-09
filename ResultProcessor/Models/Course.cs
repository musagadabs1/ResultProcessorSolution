using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultProcessor.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Course Title is Required")]
        [Display(Name ="Course Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Course Code is Required")]
        [Display(Name = "Course Code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Course Unit is Required")]
        [Display(Name = "Course Unit")]
        public int Unit { get; set; }
        [Display(Name ="Programme")]
        public int ProgrammeId { get; set; }
        public string Semester { get; set; }
        public string Level { get; set; }
        [Display(Name ="Is Active?")]
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Programme Programme { get; set; }

        public virtual ICollection<ScoreSheet> ScoreSheets { get; set; }
    }
}
