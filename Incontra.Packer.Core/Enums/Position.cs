using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Incontra.Packer.Core.Enums
{
    public enum Position
    {
        WH = 0, //Front view by Width - Height
        HW = 1, //Front view by Height - Width
        WD = 2, //Front view by Width - Depth
        DW = 3, //Front view by Depth - Width
        HD = 4, //Front view by Height - Depth
        DH = 5  //Front view by Depth - Height
    }
}
