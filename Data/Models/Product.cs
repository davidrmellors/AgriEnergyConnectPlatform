using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }

        // One to many relationship with Review
        public virtual ICollection<Review> Reviews { get; set; }

        // One to many Relationship with Transaction
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
