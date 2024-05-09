using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class FundingSource
    {
        public int FundingSourceID { get; set; }
        public decimal Amount { get; set; }
        public string SourceName { get; set; }

        // many to many relationship with Project
        public virtual ICollection<Project> Projects { get; set; }
    }
}
