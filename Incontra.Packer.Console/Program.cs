using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Incontra.Packer.Core;
using Incontra.Packer.Core.Model;
using Incontra.Packer.Core.Exceptions;

namespace Incontra.Packer.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var random = new Random();

            int totalContainers = 0;
            int totalBoxes = 0;

            long totalContainersVolume = 0;
            long totalBoxesVolume = 0;
            decimal totalSpace = 0;

            for (int j = 0; j < 1000000; j++)
            {
                var containers = new List<Container>();
                var boxes = new List<Box>();

                for (int i = 0; i < random.Next(20) + 1; i++)
                {
                    var container = new Container(random.Next(90) + 40, random.Next(90) + 40, random.Next(90) + 40);
                    containers.Add(container);
                }
                
                for (int i = 0; i < random.Next(100) + 1; i++)
                {
                    var box = new Box(random.Next(80) + 1, random.Next(80) + 1, random.Next(80) + 1, 0);
                    for (int k = 1; k < random.Next(20) + 2; k++)
                    {
                        var b = new Box(box.Width, box.Height, box.Depth, 1);
                        boxes.Add(b);
                    }
                }

                var packer = new Incontra.Packer.Core.Packer();
                packer.InputContainers = containers;

                try
                {                    
                    //string entryInfo = @"{""containers"":[";
                    //foreach (var container in containers)
                    //{
                    //    entryInfo += string.Format(@",{{""w"":{0},""h"":{1},""d"":{2},""wt"":1}}", container.Width, container.Height, container.Depth);
                    //}
                    //entryInfo += @"],""boxes"":[";
                    //foreach (var container in containers)
                    //{                        
                    //    foreach (var box in boxes)
                    //    {
                    //        entryInfo += string.Format(@",{{""w"":{0},""h"":{1},""d"":{2},""wt"":1,""q"":1}}", box.Width, box.Height, box.Depth);
                    //    }
                    //}
                    //var logFile = @"C:\\inetpub\\wwwroot\\Incontra.Packer\\Incontra.Packer.Console\\EntryLog.txt";
                    //System.IO.File.Delete(logFile);
                    //System.IO.File.AppendAllText(logFile, entryInfo);
                    
                    var packedContainers = packer.Pack(boxes);

                    decimal space = 0;
                    int boxesVolume = 0;
                    int containersVolume = (from packedContainer in packedContainers select packedContainer.Width * packedContainer.Height * packedContainer.Depth).Sum();
                    foreach (var c in packedContainers)
                    {
                        boxesVolume += (from bs in c.Boxes select bs.Width * bs.Height * bs.Depth).Sum();
                        totalBoxes += c.Boxes.Count;
                    }

                    totalBoxesVolume += boxesVolume;
                    totalContainersVolume += containersVolume;
                    totalContainers += packedContainers.Count;

                    space = Math.Round((decimal)boxesVolume / (decimal)containersVolume * 100, 2);

                    System.Console.WriteLine(string.Format("{0}. Containers: {1} Boxes: {2} Time: {3} Space: {4}%",
                        j + 1
                        , packedContainers.Count
                        , (from packedBoxes in (from packedContainer in packedContainers select packedContainer.Boxes) select packedBoxes.Count).Sum()
                        , packer.LastExecutionTime
                        , space
                        ));
                }                
                catch (SizeException e)
                {
                    System.Console.WriteLine(string.Format("ERROR: Boxes: {0} Containers: {1} ", boxes.Count, containers.Count) + e.Message);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(string.Format("ERROR: Boxes: {0} Containers: {1} ", boxes.Count, containers.Count) + e.ToString());
                    System.Console.ReadLine();
                }
            }

            totalSpace = Math.Round((decimal)totalBoxesVolume / (decimal)totalContainersVolume * 100, 2);

            System.Console.WriteLine(string.Format("\nTotal: Containers: {0} Boxes: {1} Space: {2}%", totalContainers, totalBoxes, totalSpace));
            System.Console.ReadLine();
        }
    }
}
