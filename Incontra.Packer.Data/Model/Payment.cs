using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data.Model
{
    public class Payment
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int CurrencyID { get; set; }
        public int LicenseID { get; set; }        
        public DateTime PaymentDate { get; set; }        
        public double Amount { get; set; }
    }
}
