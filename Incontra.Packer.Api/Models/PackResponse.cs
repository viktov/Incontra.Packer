using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Incontra.Packer.Api.Models
{
    public class PackResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public string executionTime { get; set; }
        public string spaceUsage { get; set; }
        public string weight { get; set; }
        public List<InputContainer> containers { get; set; }
    }
}