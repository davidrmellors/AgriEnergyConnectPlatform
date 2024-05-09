using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        // Foreign Keys

        // many to one relationship with Product
        public int ProductID { get; set; }
        public Product Product { get; set; }

        // many to one relationship with User
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
