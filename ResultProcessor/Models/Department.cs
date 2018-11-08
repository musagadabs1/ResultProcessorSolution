using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultProcessor.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Department Name is Required")]
        [Display(Name ="Department")]
        public string DeptName { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Display(Name ="Is Active?")]
        public bool IsActive { get; set; }
        [Display(Name ="Faculty")]
        [Required]
        public int FacultyId { get; set; }

        public virtual Faculty Faculty { get; set; }
        public virtual ICollection<Programme> Programmes { get; set; }
        //public virtual ICollection<Student> Students { get; set; }
        //public virtual ICollection<ScoreSheet> ScoreSheets { get; set; }
    }
}
