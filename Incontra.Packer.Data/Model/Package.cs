using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data.Model
{
    public class Package
    {
        public int ID { get; set; }
        public int Requests { get; set; }        
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
