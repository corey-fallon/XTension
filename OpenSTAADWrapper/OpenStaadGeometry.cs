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

        internal OpenStaadGeometry(dynamic geometry)
        {
            OSGeometryUI = geometry;
        }

        public void CreateNode(int nodeID, double coordX, double coordY, double coordZ)
        {
            OSGeometryUI.CreateNode(nodeID, coordX, coordY, coordZ);
        }

        public int AddNode(double coordX, double coordY, double coordZ)
        {
            return OSGeometryUI.AddNode(coordX, coordY, coordZ);
        }

        public void DeleteNode(int nodeID)
        {
            OSGeometryUI.DeleteNode(nodeID);
        }

        public int GetLastNodeNo()
        {
            dynamic retval = OSGeometryUI.GetLastNodeNo();
            return retval;
        }

        public int GetNoOfSelectedNodes()
        {
            dynamic retval = OSGeometryUI.GetNoOfSelectedNodes();
            return retval;
        }

        public int[] GetSelectedNodes(int sorted)
        {
            int numberOfNodes = GetNoOfSelectedNodes();
            dynamic nodeIds = new int[numberOfNodes];
            OSGeometryUI.GetSelectedNodes(ref nodeIds, sorted);
            return nodeIds;
        }

        public double[] GetNodeCoordinates(int nodeId)
        {
            dynamic coordX = new double();
            dynamic coordY = new double();
            dynamic coordZ = new double();
            OSGeometryUI.GetNodeCoordinates(nodeId, ref coordX, ref coordY, ref coordZ);
            return new double[] { coordX, coordY, coordZ };
        }

        public int GetNodeCount()
        {
            return OSGeometryUI.GetNodeCount();
        }

        public int[] GetNodeList()
        {
            int nodeCount = GetNodeCount();
            dynamic nodeList = new int[nodeCount];
            OSGeometryUI.GetNodeList(ref nodeList);
            return nodeList;
        }

        public int GetMemberCount()
        {
            return OSGeometryUI.GetMemberCount();
        }

        public int[] GetBeamList()
        {
            int memberCount = GetMemberCount();
            dynamic beamList = new int[memberCount];
            OSGeometryUI.GetBeamList(ref beamList);
            return beamList;
        }

        public int[] GetMemberIncidence(int beamNumber)
        {
            dynamic nodeA = new int();
            dynamic nodeB = new int();
            dynamic retval = OSGeometryUI.GetMemberIncidence(beamNumber, ref nodeA, ref nodeB);
            return new int[] { nodeA, nodeB };
        }

        public double GetBeamLength(int beamID)
        {
            return OSGeometryUI.GetBeamLength(beamID);
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
