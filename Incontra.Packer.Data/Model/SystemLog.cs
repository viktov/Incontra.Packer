using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data.Model
{
    public class SystemLog
    {
        public int ID { get; set; }
        public int? UserID { get; set; }        
        public DateTime LogDate { get; set; }
        public string Message { get; set; }
        public int LogType { get; set; }
    }
}
