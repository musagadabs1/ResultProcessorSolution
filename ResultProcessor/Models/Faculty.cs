using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultProcessor.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Faculty Name is Required")]
        [Display(Name ="Faculty Name")]
        public string FacultyName { get; set; }
        public DateTime? DateCreated { get; set; }
        //[Required(ErrorMessage = "Faculty Name is Required")]
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
