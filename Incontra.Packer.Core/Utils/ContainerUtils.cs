using Incontra.Packer.Core.Exceptions;
using Incontra.Packer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Core.Utils
{
    public class ContainerUtils
    {
        public static void CalculateEmptySpaces(Package package, Container container, EmptySpace emptySpace)
        {
            List<EmptySpace> newEmptySpaces = new List<EmptySpace>();
            EmptySpace newEmptySpace = null;
            var x = package.X + package.Width;
            var y = package.Y + package.Height;
            var z = package.Z + package.Depth;

            newEmptySpace = new Container(container.Width - x, container.Height - emptySpace.Y, container.Depth - emptySpace.Z);
            newEmptySpace.X = x;
            newEmptySpace.Y = emptySpace.Y;
            newEmptySpace.Z = emptySpace.Z;
            if (newEmptySpace.Width > 0 && newEmptySpace.Height > 0 && newEmptySpace.Depth > 0)
                newEmptySpaces.Add(newEmptySpace);

            newEmptySpace = new Container(emptySpace.Width, container.Height - y, emptySpace.Depth);
            newEmptySpace.X = package.X;
            newEmptySpace.Y = y;
            newEmptySpace.Z = package.Z;
            if (newEmptySpace.Width > 0 && newEmptySpace.Height > 0 && newEmptySpace.Depth > 0)
                newEmptySpaces.Add(newEmptySpace);

            newEmptySpace = new Container(container.Width - emptySpace.X, container.Height - emptySpace.Y, container.Depth - z);
            newEmptySpace.X = emptySpace.X;
            newEmptySpace.Y = emptySpace.Y;
            newEmptySpace.Z = z;
            if (newEmptySpace.Width > 0 && newEmptySpace.Height > 0 && newEmptySpace.Depth > 0)
                newEmptySpaces.Add(newEmptySpace);

            container.EmptySpaces.Remove(emptySpace);
            container.EmptySpaces.AddRange(newEmptySpaces);
        }

        public static bool BoxFitsInContainer(Box box, Container container)
        {
            return (container.Width >= box.Width && container.Height >= box.Height && container.Depth >= box.Depth)
                || (container.Width >= box.Width && container.Height >= box.Depth && container.Depth >= box.Height)
                || (container.Width >= box.Height && container.Height >= box.Width && container.Depth >= box.Depth)
                || (container.Width >= box.Height && container.Height >= box.Depth && container.Depth >= box.Width)
                || (container.Width >= box.Depth && container.Height >= box.Height && container.Depth >= box.Width)
                || (container.Width >= box.Depth && container.Height >= box.Width && container.Depth >= box.Height);
        }        

        public static Container GetContainerForBoxes(List<Box> boxes, List<Container> inputContainers)
        {
            int maxDimension = 0;
            Box maxBox = boxes.First();
            int volume = 0;
            foreach(var box in boxes)
            {
                if(box.Width > maxDimension) 
                {
                    maxDimension = box.Width;
                    maxBox = box;
                }
                if(box.Height > maxDimension)
                {
                    maxDimension = box.Height;
                    maxBox = box;
                }
                if(box.Depth > maxDimension)
                {
                    maxDimension = box.Depth;
                    maxBox = box;
                }
                volume += box.Width * box.Height * box.Depth;
            }

            Container selectedContainer = null;

            foreach (var inputContainer in inputContainers.OrderBy(i => i.Width * i.Height * i.Depth))
            {
                selectedContainer = inputContainer;
                if (volume <= inputContainer.Width * inputContainer.Height * inputContainer.Depth)
                {                    
                    break;
                }
            }

            if (BoxFitsInContainer(maxBox, selectedContainer))
            {
                return new Container(selectedContainer.Width, selectedContainer.Height, selectedContainer.Depth, selectedContainer.ID, selectedContainer.MaxWeight);
            }
            else
            {
                foreach (var inputContainer in inputContainers.OrderBy(i => i.Width * i.Height * i.Depth))
                {
                    if (BoxFitsInContainer(maxBox, inputContainer))
                    {
                        return new Container(inputContainer.Width, inputContainer.Height, inputContainer.Depth, inputContainer.ID, inputContainer.MaxWeight);                      
                    }
                }
            }       
            throw new SizeException("Box size is greater than the largest container");
        }

        public static bool IsDifferent(Container c1, Container c2)
        {
            return c1.Width != c2.Width || c1.Height != c2.Height || c1.Depth != c2.Depth;
        }

        public static bool IsFreeSpace(Package package, int rotations, Container container, EmptySpace emptySpace, int counter = 0)
        {            
            if (rotations > 0 && rotations <= 17)
            {
                PackageUtils.Repack(package, rotations);
            }

            if (rotations > 17)
            {
                return false;
            }            

            if (package.Width <= emptySpace.Width && package.Height <= emptySpace.Height && package.Depth <= emptySpace.Depth)
            {
                package.X = emptySpace.X;
                package.Y = emptySpace.Y;
                package.Z = emptySpace.Z;

                if (container.Boxes.Count > 0)
                {
                    var collidingBoxes = container.Boxes.Where(b =>
                        (b.X < package.X + package.Width && b.X + b.Width > package.X && b.Y < package.Y + package.Height && b.Y + b.Height > package.Y && b.Z < package.Z + package.Depth && b.Z + b.Depth > package.Z)).OrderBy(b => b.Y).ThenBy(b => b.X).ThenBy(b => b.Z);
                    if (collidingBoxes.Any())
                    {
                        return IsFreeSpace(package, ++rotations, container, emptySpace, counter);
                    }
                    else
                    {
                        if (package.X + package.Width <= emptySpace.X + emptySpace.Width && package.Y + package.Height <= emptySpace.Y + emptySpace.Height && package.Z + package.Depth <= emptySpace.Z + emptySpace.Depth)
                        {
                            if (package.Y > 0)
                            {
                                var belowBoxes = container.Boxes.Where(b => b.X < package.X + package.Width && b.X + b.Width > package.X && b.Z < package.Z + package.Depth && b.Z + b.Depth > package.Z && b.Y + b.Height <= package.Y).OrderByDescending(b => b.Y + b.Height);
                                if (belowBoxes.Any())
                                {
                                    package.Y = belowBoxes.First().Y + belowBoxes.First().Height;
                                }
                                else
                                    package.Y = 0;
                            }
                            return true;
                        }
                        return IsFreeSpace(package, ++rotations, container, emptySpace, counter);
                    }
                }
                else
                {
                    return true;
                }
            }
            return IsFreeSpace(package, ++rotations, container, emptySpace, counter);
        }     
    }
}
