using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class User
    {

        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        // Foreign Keys
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
