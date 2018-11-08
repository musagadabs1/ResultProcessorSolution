using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultProcessor.Models
{
    public class Programme
    {
        public int Id { get; set; }
        [Display(Name ="Department")]
        [Required]
        public int DepartmentId { get; set; }
        [Display(Name = "Programme")]
        [Required]
        public string ProgrammeName { get; set; }
        [Display(Name = "Is Active?")]
        [Required]
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<ScoreSheet> ScoreSheets { get; set; }



    }
}
