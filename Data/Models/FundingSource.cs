using System;
using System.Collections.Generic;

namespace Data.Models
{
    /// <summary>
    /// Represents a funding source in the system.
    /// </summary>
    public class FundingSource
    {
        /// <summary>
        /// Gets or sets the unique identifier for the funding source.
        /// </summary>
        public int FundingSourceID { get; set; }

        /// <summary>
        /// Gets or sets the amount of funding provided by the source.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the name of the funding source.
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// Navigation property for the projects associated with the funding source.
        /// This represents a many-to-many relationship with the Project entity.
        /// </summary>
        public virtual ICollection<Project> Projects { get; set; }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//