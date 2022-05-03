using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSTAADWrapper
{
    public class OpenStaadGeometry
    {
        private dynamic OSGeometryUI;

        public OpenStaadGeometry(dynamic geometry)
        {
            OSGeometryUI = geometry;
        }

        public int AddNode(double coordX, double coordY, double coordZ)
        {
            return OSGeometryUI.AddNode(coordX, coordY, coordZ);
        }

        public int AddPlate(double nodeA, double nodeB, double nodeC, double nodeD)
        {
            return OSGeometryUI.AddPlate(nodeA, nodeB, nodeC, nodeD);
        }

        public int AddPlate(double nodeA, double nodeB, double nodeC)
        {
            return OSGeometryUI.AddPlate(nodeA, nodeB, nodeC, null);
        }
    }
}
