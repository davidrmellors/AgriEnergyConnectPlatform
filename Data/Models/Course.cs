using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    /// <summary>
    /// Represents a course in the system.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Gets or sets the unique identifier for the course.
        /// </summary>
        public int CourseID { get; set; }

        /// <summary>
        /// Gets or sets the title of the course.
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// Gets or sets the description of the course.
        /// </summary>
        public string CourseDescription { get; set; }

        /// <summary>
        /// Gets or sets the URL of the course content.
        /// </summary>
        public string ContentURL { get; set; }

        /// <summary>
        /// Navigation property for the users associated with the course.
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//