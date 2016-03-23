using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Incontra.Packer.Core.Enums;

namespace Incontra.Packer.Core.Model
{
    public class Box: IComparable
    {        
        public int ID { get; set; }
        public int? ContainerID { get; set; } 
        public int CalculationID { get; set; }
  
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }

        public double Weight { get; set; }

        public decimal R { get; set; }
        public decimal G { get; set; }
        public decimal B { get; set; }
        
        public Box()
        {
        }
        
        public Box(int width, int height, int depth, double weight)
        {
            Width = width;
            Height = height;
            Depth = depth;
            Weight = weight;        
        }        

        public Box(int x, int y, int z, int width, int height, int depth)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
        }
        
        public int CompareTo(object obj)
        {
            var box = (Box)obj;
            var thisVolume = this.Width * this.Height * this.Depth;
            var boxVolume = box.Width * box.Height * box.Depth;
            if (thisVolume > boxVolume)
                return 1;
            else if (thisVolume < boxVolume)
                return -1;
            return 0;
        }
    }
}
