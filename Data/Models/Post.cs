using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    /// <summary>
    /// Represents a post in the system.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Gets or sets the unique identifier for the post.
        /// </summary>
        public int PostID { get; set; }

        /// <summary>
        /// Gets or sets the content of the post.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the post was created.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        // Foreign Keys

        /// <summary>
        /// Gets or sets the unique identifier for the user who made the post.
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the user who made the post.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Navigation property for the comments associated with the post.
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//