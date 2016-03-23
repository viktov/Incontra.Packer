using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Incontra.Packer.Api.Models
{
    public class PackRequest
    {
        public List<InputContainer> containers { get; set; }
        public List<InputBox> boxes { get; set; }
        public InputOption options { get; set; }
    }
}