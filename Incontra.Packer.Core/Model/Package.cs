using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Incontra.Packer.Core.Enums;

namespace Incontra.Packer.Core.Model
{
    public class Package: Box, IComparable
    {
        public int RowsX { get; set; }
        public int RowsY { get; set; }
        public int RowsZ { get; set; }

        public List<Box> Boxes { get; set; }

        public Package()
        {
        }
        
        public int CompareTo(object obj)
        {
            var package = (Package)obj;
            var thisVolume = this.Width * this.Height * this.Depth;
            var packageVolume = package.Width * package.Height * package.Depth;
            if (thisVolume > packageVolume)
                return 1;
            else if (thisVolume < packageVolume)
                return -1;
            return 0;
        }
    }
}
