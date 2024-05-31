using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data.Models
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name { get; set; }

        // Navigation properties

        /// <summary>
        /// Gets or sets the posts made by the user.
        /// </summary>
        public virtual ICollection<Post> Posts { get; set; }

        /// <summary>
        /// Gets or sets the comments made by the user.
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// Gets or sets the reviews made by the user.
        /// </summary>
        public virtual ICollection<Review> Reviews { get; set; }

        /// <summary>
        /// Gets or sets the transactions made by the user.
        /// </summary>
        public virtual ICollection<Transaction> Transactions { get; set; }

        /// <summary>
        /// Gets or sets the courses associated with the user.
        /// </summary>
        public virtual ICollection<Course> Courses { get; set; }

        /// <summary>
        /// Gets or sets the projects associated with the user.
        /// </summary>
        public virtual ICollection<Project> Projects { get; set; }

        /// <summary>
        /// Gets or sets the products associated with the user.
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//