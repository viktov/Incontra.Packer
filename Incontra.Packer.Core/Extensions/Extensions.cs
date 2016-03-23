using Incontra.Packer.Core.Enums;
using Incontra.Packer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Core.Extensions
{
    public static class Extensions
    {
        
        public static Position CurrentPosition(this Box box)
        {
            if (box.Width >= box.Height)
            {
                if (box.Width >= box.Depth)
                    return box.Depth <= box.Height ? Position.WH : Position.WD;
            }
            else if (box.Height >= box.Width)
            {
                if (box.Height >= box.Depth)
                    return box.Depth <= box.Width ? Position.HW : Position.HD;
            }
            else if (box.Depth >= box.Height)
            {
                if (box.Depth >= box.Width)
                    return box.Width <= box.Height ? Position.DH : Position.DW;
            }
            return Position.WH;
        }
    }
}
