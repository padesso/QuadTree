using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTreeTDD
{
    class Program
    {
        static void Main(string[] args)
        {
            //Testing the quadtree
            QuadTree tree = new QuadTree(new AxisAlignedBoundingBox(new Vector(50, 50), 100, 100));
            //Vector tempVect = new Vector(20, 20);
            //tree.Insert(tempVect);
            //Vector tempVect1 = new Vector(40, 40);
            //tree.Insert(tempVect1);

            Random rand = new Random(DateTime.Now.Millisecond);
            for (int vectorIndex = 0; vectorIndex < 100; vectorIndex++)
            {
                Vector tempVect = new Vector(rand.Next(0, 100), rand.Next(0,100));
                tree.Insert(tempVect);
            }

            List<Vector> allPositions = tree.QueryBounds(new AxisAlignedBoundingBox(new Vector(50, 50), 100, 100));

            //Tight main loop - Escape key quits
            do
            {
                while (!Console.KeyAvailable)
                {
                    // Do something
                    Console.WriteLine("Tick");
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
