using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Incontra.Packer.Core.Enums;

namespace Incontra.Packer.Core.Model
{
    public class Container : EmptySpace, IComparable
    {
        public int ID { get; set; }
        public int CalculationID { get; set; }        
        public double Weight { get; set; }
        public double MaxWeight { get; set; }
        public List<Box> Boxes { get; set; }
        public List<EmptySpace> EmptySpaces { get; set; }

        public Container()
        {
        }

        public Container(int width, int height, int depth) : base(width, height, depth)
        {            
            Boxes = new List<Box>();
            EmptySpaces = new List<EmptySpace>() { new EmptySpace(width, height, depth) };
        }

        public Container(int width, int height, int depth, int id, double maxWeight)
            : base(width, height, depth)
        {
            Boxes = new List<Box>();
            EmptySpaces = new List<EmptySpace>() { new EmptySpace(width, height, depth) };
            ID = id;
            MaxWeight = maxWeight;
        }

        public int CompareTo(object obj)
        {
            var container = (Container)obj;
            var thisSize = this.Width * this.Height * this.Depth;
            var containerSize = container.Width * container.Height * container.Depth;
            if (thisSize > containerSize)
                return 1;
            else if (thisSize < containerSize)
                return -1;
            return 0;
        }
    }
}
