using Incontra.Packer.Core.Enums;
using Incontra.Packer.Core.Exceptions;
using Incontra.Packer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Core.Utils
{
    public class BoxUtils
    {
        public static void Rotate(Box box, Position position)
        {
            int width = box.Width;
            int height = box.Height;
            int depth = box.Depth;

            switch (position)
            {
                case Position.WH:
                    {
                        box.Width = width;
                        box.Height = height;
                        box.Depth = depth;
                        break;
                    }
                case Position.HW:
                    {
                        box.Width = height;
                        box.Height = width;
                        box.Depth = depth;
                        break;
                    }
                case Position.WD:
                    {
                        box.Width = width;
                        box.Height = depth;
                        box.Depth = height;
                        break;
                    }
                case Position.DW:
                    {
                        box.Width = depth;
                        box.Height = width;
                        box.Depth = height;
                        break;
                    }
                case Position.HD:
                    {
                        box.Width = height;
                        box.Height = depth;
                        box.Depth = width;
                        break;
                    }
                case Position.DH:
                    {
                        box.Width = depth;
                        box.Height = height;
                        box.Depth = width;
                        break;
                    }
            }
        }

        public static Box GetBoxWithMaxDimension(List<Box> boxes)
        {
            int maxDimension = 0;
            Box boxWithMaxDimension = boxes.First();
            foreach (var box in boxes)
            {
                if (box.Width > maxDimension)
                {
                    maxDimension = box.Width;
                    boxWithMaxDimension = box;
                }
                if (box.Height > maxDimension)
                {
                    maxDimension = box.Height;
                    boxWithMaxDimension = box;
                }
                if (box.Depth > maxDimension)
                {
                    maxDimension = box.Depth;
                    boxWithMaxDimension = box;
                }
            }
            return boxWithMaxDimension;
        }

        public static void PlaceFlat(Box box)
        {
            if (box.Height >= box.Width && box.Height >= box.Depth)
                Rotate(box, box.Width >= box.Depth ? Position.HD : Position.HW);
            else if (box.Depth >= box.Width && box.Depth >= box.Height)
                Rotate(box, box.Width >= box.Height ? Position.DH : Position.DW);
            else if (box.Width >= box.Height && box.Width >= box.Depth)
                Rotate(box, box.Height >= box.Depth ? Position.WD : Position.WH);
        }

        public static void CheckBoxDimensions(Box box)
        {
            if (box.Width <= 0 || box.Height <= 0 || box.Depth <= 0)
            {
                string msg = "Each box dimension needs to be greater than 0";
                throw new SizeException(msg);
            }
        }

        public static List<Box> SetCommonOrientation(List<Box> boxes)
        {
            var list = new List<Box>();
            foreach (Box box in boxes)
            {
                PlaceFlat(box);
                CheckBoxDimensions(box);
                box.ContainerID = null;
                list.Add(box);
            }
            return list;
        }
    }
}
