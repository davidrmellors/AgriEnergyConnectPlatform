using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data.Models
{
    public class User : IdentityUser
    {
        // Additional properties specific to your application
        public string Name { get; set; }

        // Navigation properties
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        // Constructor to initialize collections
        //public User()
        //{
        //    Posts = new HashSet<Post>();
        //    Comments = new HashSet<Comment>();
        //    Reviews = new HashSet<Review>();
        //    Transactions = new HashSet<Transaction>();
        //    Courses = new HashSet<Course>();
        //    Projects = new HashSet<Project>();
        //}
    }
}
