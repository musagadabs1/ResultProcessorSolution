using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultProcessor.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string FacultyName { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
