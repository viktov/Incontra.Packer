using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data.Model
{
    public class PackRequest
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ResponseDate { get; set; }
        public double ResponseTime { get; set; }
        public double CalculationTime { get; set; }
        public int Status { get; set; }
    }
}
