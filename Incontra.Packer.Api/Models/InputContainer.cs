using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Incontra.Packer.Api.Models
{
    public class InputContainer
    {
        public int w { get; set; }
        public int h { get; set; }
        public int d { get; set; }
        public double wt { get; set; }
        public string spaceUsage { get; set; }
        public string weight { get; set; }
        public List<InputBox> boxes { get; set; }
        public List<InputBox> spaces { get; set; }
        public string view3d { get; set; }
    }
}