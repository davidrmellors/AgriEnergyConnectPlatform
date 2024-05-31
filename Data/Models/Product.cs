using System;
using System.Collections.Generic;

namespace Data.Models
{
    /// <summary>
    /// Represents a product in the system.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string ProductDescription { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity of the product.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the path of the product image.
        /// </summary>
        public string ProductImagePath { get; set; }

        /// <summary>
        /// Gets or sets the production date of the product.
        /// </summary>
        public DateTime ProductionDate { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user associated with the product.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Navigation property for the user associated with the product.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Navigation property for the reviews associated with the product.
        /// </summary>
        public virtual ICollection<Review> Reviews { get; set; }

        /// <summary>
        /// Navigation property for the transactions associated with the product.
        /// </summary>
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//