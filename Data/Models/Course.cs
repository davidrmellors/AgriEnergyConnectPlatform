using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public string ContentURL { get; set; }

        // Foreign Keys
        public virtual ICollection<User> Users { get; set; }
    }
}
