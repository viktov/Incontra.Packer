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
    public class PackageUtils
    {

        private static List<int> GetPossibleRows(int boxesCount)
        {
            List<int> possibleRows = new List<int>();
            for (int i = 1; i <= boxesCount; i++)
            {
                if (boxesCount % i == 0)
                {
                    possibleRows.Add(i);
                }
            }
            return possibleRows;
        }

        private static Tuple<int, int, int, int> GetPackageRowsAndOrientation(List<int> possibleRows, int boxesCount, Box box, Container container)
        {
            List<Tuple<int, int, int, int>> possiblePackageRowsAndOrientation = new List<Tuple<int, int, int, int>>();
            for (int y = 0; y < possibleRows.Count; y++)
            {
                for (int x = 0; x < possibleRows.Count; x++)
                {
                    for (int z = 0; z < possibleRows.Count; z++)
                    {
                        int? orientation = null;
                        if (possibleRows[x] * possibleRows[y] * possibleRows[z] == boxesCount)
                        {
                            if (possibleRows[x] * box.Width <= container.Width
                                && possibleRows[y] * box.Height <= container.Height
                                && possibleRows[z] * box.Depth <= container.Depth)
                            {
                                orientation = 0;
                            }

                            else if (possibleRows[x] * box.Width <= container.Width
                                && possibleRows[y] * box.Depth <= container.Height
                                && possibleRows[z] * box.Height <= container.Depth)
                            {
                                orientation = 1;
                            }

                            else if (possibleRows[x] * box.Height <= container.Width
                                && possibleRows[y] * box.Width <= container.Height
                                && possibleRows[z] * box.Depth <= container.Depth)
                            {
                                orientation = 2;
                            }

                            else if (possibleRows[x] * box.Height <= container.Width
                                && possibleRows[y] * box.Depth <= container.Height
                                && possibleRows[z] * box.Width <= container.Depth)
                            {
                                orientation = 3;
                            }

                            else if (possibleRows[x] * box.Depth <= container.Width
                                && possibleRows[y] * box.Width <= container.Height
                                && possibleRows[z] * box.Height <= container.Depth)
                            {
                                orientation = 4;
                            }

                            else if (possibleRows[x] * box.Depth <= container.Width
                                && possibleRows[y] * box.Height <= container.Height
                                && possibleRows[z] * box.Width <= container.Depth)
                            {
                                orientation = 5;
                            }
                            if (orientation.HasValue)
                                possiblePackageRowsAndOrientation.Add(new Tuple<int, int, int, int>(possibleRows[x], possibleRows[y], possibleRows[z], orientation.Value));
                        }
                    }
                }
            }
            return possiblePackageRowsAndOrientation.Any() ? possiblePackageRowsAndOrientation.OrderByDescending(t => t.Item3).ThenByDescending(t => t.Item1).First() : null;
        }

        private static Box GetBoxOrientedForPackage(Box box, int orientation)
        {
            int width = box.Width;
            int height = box.Height;
            int depth = box.Depth;

            switch (orientation)
            {
                case 0:
                    BoxUtils.Rotate(box, Position.WH);
                    break;
                case 1:
                    BoxUtils.Rotate(box, Position.WD);
                    break;
                case 2:
                    BoxUtils.Rotate(box, Position.HW);
                    break;
                case 3:
                    BoxUtils.Rotate(box, Position.DW);
                    break;
                case 4:
                    BoxUtils.Rotate(box, Position.DH);
                    break;
                case 5:
                    BoxUtils.Rotate(box, Position.HD);
                    break;
            }
            box.Width = width;
            box.Height = height;
            box.Depth = depth;
            return box;
        }

        private static Package CreatePackage(Tuple<int, int, int, int> packageRowsAndOrientation, List<Box> boxes)
        {
            var package = new Package();
            package.Boxes = new List<Box>();

            int rowsX = packageRowsAndOrientation.Item1;
            int rowsY = packageRowsAndOrientation.Item2;
            int rowsZ = packageRowsAndOrientation.Item3;
            int orientation = packageRowsAndOrientation.Item4;
            int index = 0;
            var boxForPackage = GetBoxOrientedForPackage(boxes[0], orientation);

            for (int x = 0; x < rowsX; x++)
            {
                for (int y = 0; y < rowsY; y++)
                {
                    for (int z = 0; z < rowsZ; z++)
                    {
                        var box = new Box(boxForPackage.Width, boxForPackage.Height, boxForPackage.Depth, boxForPackage.Weight);
                        box.R = boxForPackage.R;
                        box.G = boxForPackage.G;
                        box.B = boxForPackage.B;
                        box.X = x * boxForPackage.Width;
                        box.Y = y * boxForPackage.Height;
                        box.Z = z * boxForPackage.Depth;
                        box.ID = boxes[index].ID;
                        package.Boxes.Add(box);
                        index++;
                    }
                }
            }
            package.Width = rowsX * boxForPackage.Width;
            package.Height = rowsY * boxForPackage.Height;
            package.Depth = rowsZ * boxForPackage.Depth;
            package.RowsX = rowsX;
            package.RowsY = rowsY;
            package.RowsZ = rowsZ;
            return package;
        }

        public static List<Package> PreparePackages(Container container, List<Box> boxes, List<Box> unpacked = null)
        {
            List<Package> packages = new List<Package>();
            List<int> possibleRows = GetPossibleRows(boxes.Count);
            Tuple<int, int, int, int> packageRowsAndOrientation = GetPackageRowsAndOrientation(possibleRows, boxes.Count, boxes[0], container);

            if (packageRowsAndOrientation != null)
            {
                if (unpacked != null)
                    packages.AddRange(PreparePackages(container, unpacked));
                packages.Add(CreatePackage(packageRowsAndOrientation, boxes));
                return packages;
            }

            if (boxes.Count > 1)
            {
                if (unpacked == null)
                    unpacked = new List<Box>();
                unpacked.AddRange(boxes.GetRange(boxes.Count - 1, 1));
                packages.AddRange(PreparePackages(container, boxes.GetRange(0, boxes.Count - 1).ToList(), unpacked));
            }
            else
            {
                if (unpacked != null)
                    packages.AddRange(PreparePackages(container, unpacked));
                packages.Add(CreatePackage(new Tuple<int, int, int, int>(1, 1, 1, 0), boxes));
            }
            return packages;
        }


        public static void Repack(Package package, int position)
        {
            var boxes = package.Boxes;
            var totalRows = package.RowsX + package.RowsY + package.RowsZ;
            int rowsX = package.RowsX;
            int rowsY = package.RowsY;
            int rowsZ = package.RowsZ;
            foreach (var box in boxes)
            {
                BoxUtils.PlaceFlat(box);
            }
            switch (position)
            {
                case 1:
                    {
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.DH);
                        }
                        break;
                    }
                case 2:
                    {
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.WD);
                        }
                        break;
                    }
                case 3:
                    {
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.HW);
                        }
                        break;
                    }
                case 4:
                    {
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.HD);
                        }
                        break;
                    }
                case 5:
                    {
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.DW);
                        }
                        break;
                    }
                case 6:
                    {
                        rowsX = package.RowsY;
                        rowsY = package.RowsZ;
                        rowsZ = package.RowsX;
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.WH);
                        }
                        break;
                    }

                case 7:
                    {
                        rowsX = package.RowsY;
                        rowsY = package.RowsZ;
                        rowsZ = package.RowsX;
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.DH);
                        }
                        break;
                    }
                case 8:
                    {
                        rowsX = package.RowsY;
                        rowsY = package.RowsZ;
                        rowsZ = package.RowsX;
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.WD);
                        }
                        break;
                    }
                case 9:
                    {
                        rowsX = package.RowsY;
                        rowsY = package.RowsZ;
                        rowsZ = package.RowsX;
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.HW);
                        }
                        break;
                    }
                case 10:
                    {
                        rowsX = package.RowsY;
                        rowsY = package.RowsZ;
                        rowsZ = package.RowsX;
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.HD);
                        }
                        break;
                    }
                case 11:
                    {
                        rowsX = package.RowsY;
                        rowsY = package.RowsZ;
                        rowsZ = package.RowsX;
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.DW);
                        }
                        break;
                    }

                case 12:
                    {
                        rowsX = package.RowsZ;
                        rowsY = package.RowsX;
                        rowsZ = package.RowsY;
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.WH);
                        }
                        break;
                    }

                case 13:
                    {
                        rowsX = package.RowsZ;
                        rowsY = package.RowsX;
                        rowsZ = package.RowsY;
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.DH);
                        }
                        break;
                    }
                case 14:
                    {
                        rowsX = package.RowsZ;
                        rowsY = package.RowsX;
                        rowsZ = package.RowsY;
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.WD);
                        }
                        break;
                    }
                case 15:
                    {
                        rowsX = package.RowsZ;
                        rowsY = package.RowsX;
                        rowsZ = package.RowsY;
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.HW);
                        }
                        break;
                    }
                case 16:
                    {
                        rowsX = package.RowsZ;
                        rowsY = package.RowsX;
                        rowsZ = package.RowsY;
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.HD);
                        }
                        break;
                    }
                case 17:
                    {
                        rowsX = package.RowsZ;
                        rowsY = package.RowsX;
                        rowsZ = package.RowsY;
                        foreach (var box in boxes)
                        {
                            BoxUtils.Rotate(box, Position.DW);
                        }
                        break;
                    }
            }

            package.Boxes = new List<Box>();
            int index = 0;
            for (int x = 0; x < rowsX; x++)
            {
                for (int y = 0; y < rowsY; y++)
                {
                    for (int z = 0; z < rowsZ; z++)
                    {
                        var box = new Box(0, 0, 0, boxes[index].Width, boxes[index].Height, boxes[index].Depth);
                        box.Weight = boxes[index].Weight;
                        box.R = boxes[index].R;
                        box.G = boxes[index].G;
                        box.B = boxes[index].B;
                        box.X = x * box.Width;
                        box.Y = y * box.Height;
                        box.Z = z * box.Depth;
                        box.ID = boxes[index].ID;
                        package.Boxes.Add(box);
                        index++;
                    }
                }
            }
            package.Width = rowsX * boxes[0].Width;
            package.Height = rowsY * boxes[0].Height;
            package.Depth = rowsZ * boxes[0].Depth;
            package.RowsX = rowsX;
            package.RowsY = rowsY;
            package.RowsZ = rowsZ;
        }

        public static List<Package> Divide(Package package, Container container, int count)
        {
            List<Package> packages = new List<Package>();
            List<Box> boxes = package.Boxes;
            if (count >= boxes.Count)
                throw new ArgumentException("Count cannot be equal or greater than number of boxes in the package");
            if (boxes.Count > 1)
            {
                packages.AddRange(PackageUtils.PreparePackages(container, boxes.GetRange(0, boxes.Count - count).ToList()));
                packages.AddRange(PackageUtils.PreparePackages(container, boxes.GetRange(boxes.Count - count, count).ToList()));
            }
            else
            {
                packages.Add(package);
            }
            return packages;
        }

        public static List<PackageLocation> GetLocationForPackage(Package package, Container container)
        {
            List<PackageLocation> packageLocations = new List<PackageLocation>();
            EmptySpace emptySpace = null;
            var emptySpaces = container.EmptySpaces.OrderBy(e => e.Y).ThenByDescending(e => e.Width * e.Depth).ThenByDescending(e => e.Height);

            List<Package> packagesToPack = new List<Package> { package };
            var boxesToPack = new List<Box>();

            foreach (var es in emptySpaces)
            {
                if (packagesToPack.Count == 0)
                    break;

                foreach (var packageToPack in packagesToPack)
                {
                    boxesToPack.AddRange(packageToPack.Boxes);
                }
                packagesToPack.Clear();

                if (ContainerUtils.IsFreeSpace(package, 0, container, es))
                {
                    emptySpace = es;

                    foreach (var box in package.Boxes)
                    {
                        box.X += package.X;
                        box.Y += package.Y;
                        box.Z += package.Z;
                        container.Boxes.Add(box);
                    }
                    ContainerUtils.CalculateEmptySpaces(package, container, emptySpace);
                    packageLocations.Add(new PackageLocation { Package = package, Location = new Location(package.X, package.Y, package.Z) });
                }
                else
                {
                    packagesToPack.Add(package);
                    continue;
                }
            }

            if (packagesToPack.Count > 0)
            {
                if (packagesToPack.Count == 1 && packagesToPack[0].Boxes.Count == 1)
                {
                    packageLocations.Add(new PackageLocation { Package = package, Location = null });
                }
                else
                {
                    foreach (var packageToPack in packagesToPack)
                    {
                        if (packageToPack.Boxes.Count > 1)
                        {
                            List<Package> ps = PackageUtils.Divide(packageToPack, container, 1);
                            foreach (var partPackage in ps.OrderByDescending(pp => pp.Boxes.Count))
                            {
                                packageLocations.AddRange(PackageUtils.GetLocationForPackage(partPackage, container));
                            }
                        }
                        else
                        {
                            packageLocations.AddRange(PackageUtils.GetLocationForPackage(packageToPack, container));
                        }

                    }
                }
            }
            return packageLocations;
        }

        private static Tuple<decimal, decimal, decimal> GetRandomColor(Random random)
        {
            var r = (decimal)(random.Next(30) + 30) / 100;
            var g = (decimal)(random.Next(30) + 30) / 100;
            var b = (decimal)(random.Next(30) + 30) / 100;
            return new Tuple<decimal, decimal, decimal>(r, g, b);
        }

        public static List<Package> GetPackages(List<Box> boxes, Container container, bool setColors)
        {
            var packages = new List<Package>();
            List<Box> boxesForPackage = new List<Box>();
            Box prevBox = null;
            boxes = BoxUtils.SetCommonOrientation(boxes);
            int volume = 0;
            var random = new Random();
            var color = GetRandomColor(random);
            foreach (var box in boxes.OrderByDescending(bx => Math.Max(bx.Width, Math.Max(bx.Height, bx.Depth))))
            {
                volume += box.Width * box.Height * box.Depth;
                if (prevBox != null && (box.Width != prevBox.Width || box.Height != prevBox.Height || box.Depth != prevBox.Depth))
                {
                    color = GetRandomColor(random);
                    packages.AddRange(PackageUtils.PreparePackages(container, boxesForPackage));
                    boxesForPackage = new List<Box>();
                    volume = 0;
                }
                else if (prevBox != null && (volume >= container.Width * container.Height * container.Depth || boxesForPackage.Count >= 300))
                {
                    packages.AddRange(PackageUtils.PreparePackages(container, boxesForPackage));
                    boxesForPackage = new List<Box>();
                    volume = 0;
                }

                if (setColors)
                {
                    box.R = color.Item1;
                    box.G = color.Item2;
                    box.B = color.Item3;
                }

                boxesForPackage.Add(box);
                prevBox = box;
            }
            if (boxesForPackage.Count > 0)
            {
                packages.AddRange(PackageUtils.PreparePackages(container, boxesForPackage));
            }
            return packages;
        }
    }

    public class PackageLocation
    {
        public Package Package { get; set; }
        public Location Location { get; set; }
    }
}
