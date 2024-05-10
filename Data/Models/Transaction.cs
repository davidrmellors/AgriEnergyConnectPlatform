using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime TimeStamp { get; set; }

        // Foreign Keys

        // many to one relationship with Product
        public int ProductID { get; set; }
        public Product Product { get; set; }

        // many to one relationship with User

        public string UserID { get; set; }
        public User User { get; set; }
    }
}
