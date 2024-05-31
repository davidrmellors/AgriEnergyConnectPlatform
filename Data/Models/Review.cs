using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    /// <summary>
    /// Represents a review in the system.
    /// </summary>
    public class Review
    {
        /// <summary>
        /// Gets or sets the unique identifier for the review.
        /// </summary>
        public int ReviewID { get; set; }

        /// <summary>
        /// Gets or sets the rating of the review.
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets the comment of the review.
        /// </summary>
        public string Comment { get; set; }

        // Foreign Keys

        /// <summary>
        /// Gets or sets the unique identifier for the product associated with the review.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Navigation property for the product associated with the review.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user who made the review.
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Navigation property for the user who made the review.
        /// </summary>
        public User User { get; set; }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//