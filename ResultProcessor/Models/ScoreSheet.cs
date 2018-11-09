using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultProcessor.Models
{
    public class ScoreSheet
    {
        public int Id { get; set; }
        [Required()]
        [Display(Name ="Reg No")]
        public string RegNo { get; set; }
        [Required]
        [Display(Name ="Course")]
        public int CourseId { get; set; }
        public float Score { get; set; }
        public Semester Semester { get; set; }
        public Level Level { get; set; }
        public Grade Grade { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        public virtual Course Course { get; set; }
        //public virtual Student Student { get; set; }
        
    }
}
