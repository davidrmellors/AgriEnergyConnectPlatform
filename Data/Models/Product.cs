using System;
using System.Collections.Generic;

namespace Data.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ProductImagePath { get; set; }
        public DateTime ProductionDate { get; set; }

        // Foreign key to associate product with user
        public string UserId { get; set; }

        // Navigation property for the user
        public virtual User User { get; set; }

        // One-to-many relationship with Review
        public virtual ICollection<Review> Reviews { get; set; }

        // One-to-many relationship with Transaction
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
