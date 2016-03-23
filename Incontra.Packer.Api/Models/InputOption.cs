using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Incontra.Packer.Api.Models
{
    public class InputOption
    {
        public string weightUnit { get; set; }
        public string lengthUnit { get; set; }
        public bool showExecutionTime { get; set; }
        public bool showTotalWeight { get; set; }
        public bool showSpaceUsage { get; set; }
        public bool showView3d { get; set; }
        public bool showView2d { get; set; }
    }
}