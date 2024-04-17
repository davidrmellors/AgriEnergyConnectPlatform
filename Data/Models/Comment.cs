using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    internal class Comment
    {
        public int CommentID { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }

        // Foreign Keys
        
        // Many to one relationship with User
        public int UserID { get; set; }
        public User User { get; set; }

        // Many to one relationship with Post
        public int PostID { get; set; }
        public Post Post { get; set; }
        

    }
}
