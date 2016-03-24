using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data.Model
{
    public class PackagePrice
    {
        public int ID { get; set; }
        public int PackageID { get; set; }
        public int PeriodID { get; set; }
        public int CurrencyID { get; set; }
        public decimal PriceTotal { get; set; }
        public decimal PricePerRequest { get; set; }
    }
}
