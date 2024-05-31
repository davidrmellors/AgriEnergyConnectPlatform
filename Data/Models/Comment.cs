using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    /// <summary>
    /// Represents a comment in the system.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Gets or sets the unique identifier for the comment.
        /// </summary>
        public int CommentID { get; set; }

        /// <summary>
        /// Gets or sets the content of the comment.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the comment was created.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        // Foreign Keys

        /// <summary>
        /// Gets or sets the unique identifier for the user who made the comment.
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the user who made the comment.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the post that the comment is associated with.
        /// </summary>
        public int PostID { get; set; }

        /// <summary>
        /// Gets or sets the post that the comment is associated with.
        /// </summary>
        public Post Post { get; set; }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//