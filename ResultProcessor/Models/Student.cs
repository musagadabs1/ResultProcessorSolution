using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultProcessor.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="First Name is Required")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        //[Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Registration Number Name is Required")]
        [Display(Name = "Registration Number")]
        public string RegNo { get; set; }
        [Required(ErrorMessage = "Date of Birth is Required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Date of Admission is Required")]
        [Display(Name = "Date of Admission")]
        [DataType(DataType.Date)]
        public DateTime DOAdmission { get; set; }
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Phone Number is Required")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        //[Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required()]
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }
        //public int DeptId { get; set; }
        [Required()]
        [Display(Name = "Programme")]
        public int ProgrammeId { get; set; }

        //public virtual Department Department { get; set; }
        public virtual Programme Programme { get; set; }

        
    }
}
