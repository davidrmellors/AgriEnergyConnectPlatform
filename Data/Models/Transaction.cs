using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    /// <summary>
    /// Represents a transaction in the system.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the unique identifier for the transaction.
        /// </summary>
        public int TransactionID { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in the transaction.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the total price of the transaction.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the transaction was made.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        // Foreign Keys

        /// <summary>
        /// Gets or sets the unique identifier for the product associated with the transaction.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Navigation property for the product associated with the transaction.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user who made the transaction.
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Navigation property for the user who made the transaction.
        /// </summary>
        public User User { get; set; }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//