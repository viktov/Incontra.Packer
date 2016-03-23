using Incontra.Packer.Core.Enums;
using Incontra.Packer.Core.Extensions;
using Incontra.Packer.Core.Exceptions;
using Incontra.Packer.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Incontra.Packer.Core.Utils;

namespace Incontra.Packer.Core
{
    public class Packer
    {
        private Stopwatch _timerTotal;
        private List<Container> _inputContainers;
        private List<Container> _containers;
        public List<Container> InputContainers
        {
            get
            {
                return _inputContainers.OrderBy(s => s.Width * s.Height * s.Depth).ToList();
            }
            set
            {
                _inputContainers = value;
            }
        }

        public double LastExecutionTime
        {
            get
            {
                return _timerTotal.Elapsed.TotalSeconds;
            }
        }          

        public List<Container> Pack(List<Box> boxes)
        {
            _timerTotal = new Stopwatch();
            _timerTotal.Start();

            _containers = new List<Container>();
            boxes = BoxUtils.SetCommonOrientation(boxes);
            var container = ContainerUtils.GetContainerForBoxes(boxes, InputContainers);             
            _containers.Add(container);             
            var packages = PackageUtils.GetPackages(boxes, container, true);
            var list = PackIntoContainer(packages, container);

            _timerTotal.Stop();
            return list;
        }

        private void AddToContainer(Container container, List<Box> boxes)
        {
            _containers.Add(container);
            var packages = PackageUtils.GetPackages(boxes, container, false);
            PackIntoContainer(packages, container);
        }

        private void PackIntoNextContainer(Container container, List<Box> boxes)
        {
            var nextContainer = ContainerUtils.GetContainerForBoxes(boxes, InputContainers);             
            AddToContainer(nextContainer, boxes);
        }         

        private void PackIntoBiggerContainer(Container container, List<Box> boxes)
        {             
            _containers.RemoveAt(_containers.Count - 1);
            var inputContainers = InputContainers.Where(i => i.Width * i.Height * i.Depth > container.Width * container.Height * container.Depth).OrderBy(i => i.Width * i.Height * i.Depth);
            var inputContainer = inputContainers.Any() ? inputContainers.First() : InputContainers.Where(i => i.Width * i.Height * i.Depth == container.Width * container.Height * container.Depth).OrderBy(i => i.Width * i.Height * i.Depth).Last();
            var nextContainer = new Container(inputContainer.Width, inputContainer.Height, inputContainer.Depth);
            AddToContainer(nextContainer, boxes);             
        }

        private List<Package> GetSortedPackages(Box maxBox, List<Package> packages, Container lastContainer)
        {
            return ContainerUtils.BoxFitsInContainer(maxBox, lastContainer) ?
                packages.OrderByDescending(p => p.Width * p.Depth * p.Height).ThenByDescending(p => Math.Max(p.Width, Math.Max(p.Height, p.Depth))).ToList() :
                packages.OrderByDescending(p => Math.Max(p.Boxes[0].Width, Math.Max(p.Boxes[0].Depth, p.Boxes[0].Height))).ThenByDescending(p => p.Width * p.Depth * p.Height).ThenByDescending(p => Math.Max(p.Width, Math.Max(p.Height, p.Depth))).ToList();
        }

        private void PackUnpacked(Container container, Container lastContainer, List<Box> boxes, List<Box> unpacked)
        {
            var maxBox = BoxUtils.GetBoxWithMaxDimension(boxes);
            if (ContainerUtils.IsDifferent(container, lastContainer) && ContainerUtils.BoxFitsInContainer(maxBox, lastContainer))
            {
                PackIntoBiggerContainer(container, boxes);
            }
            else
            {
                PackIntoNextContainer(container, unpacked);
            }
        }

        private List<Box> GetUnpackedBoxes(List<PackageLocation> packageLocations)
        {
            var unpacked = new List<Box>();
            if (packageLocations != null)
            {
                foreach (var pl in packageLocations)
                {
                    if (pl.Location == null)
                    {
                        unpacked.AddRange((from box in pl.Package.Boxes select box).ToList());
                    }
                }
            }
            return unpacked;
        }

        private List<Container> PackIntoContainer(List<Package> packages, Container container)
        {
            var unpacked = new List<Box>();
            var lastContainer = InputContainers[InputContainers.Count - 1];
            var boxes = (from package in packages
                            from box in package.Boxes
                            select box).ToList();
             
            Box maxBox = BoxUtils.GetBoxWithMaxDimension(boxes);
            List<Package> sorted = GetSortedPackages(maxBox, packages, lastContainer);             
            foreach (Package package in sorted)
            {
                var packageLocations = PackageUtils.GetLocationForPackage(package, container);
                unpacked.AddRange(GetUnpackedBoxes(packageLocations));
            }
            
            if (unpacked.Count > 0)
            {
                PackUnpacked(container, lastContainer, boxes, unpacked);
            }
            return _containers;
        }         
    }
}

