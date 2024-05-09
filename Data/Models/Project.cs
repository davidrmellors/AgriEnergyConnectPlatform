using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Budget { get; set; }

        // many to many relationship with User
        public virtual ICollection<User> Users { get; set; }

        // many to many relationship with FundingSource
        public virtual ICollection<FundingSource> FundingSources { get; set; }
    }
}
