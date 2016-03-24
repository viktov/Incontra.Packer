using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data.Model
{
    public class License
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int PackagePriceID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateExpire { get; set; }
        public string Key { get; set; }

    }
}
