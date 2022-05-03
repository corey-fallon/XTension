using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSTAADWrapper;

namespace OpenSTAADWrapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (OpenStaad os = new OpenStaad())
            {
                OpenStaadGeometry geometry = os.Geometry;
                int nodeA = geometry.AddNode(0, 0, 0);
                int nodeB = geometry.AddNode(1, 0, 0);
                int nodeC = geometry.AddNode(0, 1, 0);
                int plateID = geometry.AddPlate(1, 2, 3);
            }
        }
    }
}
