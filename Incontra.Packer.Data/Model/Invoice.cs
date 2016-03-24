using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data.Model
{
    public class Invoice
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int CurrencyID { get; set; }
        public int LicenseID { get; set; }
        public int PaymentID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public double Amount { get; set; }

    }
}
