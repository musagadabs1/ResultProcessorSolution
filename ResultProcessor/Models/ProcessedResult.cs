using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultProcessor.Models
{
    public class ProcessedResult
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        public string Semester { get; set; }
        public string Session { get; set; }
        public string Level { get; set; }
        public float GPA { get; set; }
        public float CGPA { get; set; }
        public string ProcessedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateProcessed { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
