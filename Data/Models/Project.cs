using System;
using System.Collections.Generic;

namespace Data.Models
{
    /// <summary>
    /// Represents a project in the system.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets or sets the unique identifier for the project.
        /// </summary>
        public int ProjectID { get; set; }

        /// <summary>
        /// Gets or sets the title of the project.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the project.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the start date of the project.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the project.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the budget for the project.
        /// </summary>
        public decimal Budget { get; set; }

        /// <summary>
        /// Navigation property for the users associated with the project.
        /// This represents a many-to-many relationship with the User entity.
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// Navigation property for the funding sources associated with the project.
        /// This represents a many-to-many relationship with the FundingSource entity.
        /// </summary>
        public virtual ICollection<FundingSource> FundingSources { get; set; }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//