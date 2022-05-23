using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenStaadSharp.DataObjects;
using OpenStaadSharp.Exceptions;

namespace OpenStaadSharp
{
    public class OpenStaadGeometry
    {
        private dynamic OSGeometryUI;

        internal OpenStaadGeometry(dynamic geometry)
        {
            OSGeometryUI = geometry;
        }

        public void AddMultipleNodes(double[,] coordinates)
        {
            OSGeometryUI.AddMultipleNodes(coordinates);
        }
        public void CreateMultipleNodes(int[] nodeIDs, double[,] coordinates)
        {
            OSGeometryUI.CreateMultipleNodes(nodeIDs, coordinates);
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
        public void DeleteMultipleNodes(int[] nodeIDs)
        {
            foreach (int nodeID in nodeIDs)
            {
                DeleteNode(nodeID);
            }
        }
        public void DeleteAllNodes()
        {
            int[] nodeIDs = GetNodeList();
            DeleteMultipleNodes(nodeIDs);
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
        public Coordinates GetNodeCoordinates(int nodeId)
        {
            dynamic coordX = new double();
            dynamic coordY = new double();
            dynamic coordZ = new double();
            OSGeometryUI.GetNodeCoordinates(nodeId, ref coordX, ref coordY, ref coordZ);
            return new Coordinates()
            {
                XCoordinate = coordX,
                YCoordinate = coordY,
                ZCoordinate = coordZ
            };
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
        public int AddMember(int nodeA, int nodeB)
        {
            dynamic retval = OSGeometryUI.AddBeam(nodeA, nodeB);
            return retval;
        }
        public int GetMemberCount()
        {
            return OSGeometryUI.GetMemberCount();
        }
        public int[] GetMemberList()
        {
            int memberCount = GetMemberCount();
            dynamic beamList = new int[memberCount];
            OSGeometryUI.GetBeamList(ref beamList);
            return beamList;
        }
        public void DeleteMember(int memberID)
        {
            OSGeometryUI.DeleteBeam(memberID);
        }
        public void DeleteMultipleMembers(int[] memberIDs)
        {
            foreach (int memberID in memberIDs)
            {
                DeleteMember(memberID);
            }
        }
        public void DeleteAllMembers()
        {
            int[] memberIDs = GetMemberList();
            DeleteMultipleMembers(memberIDs);
        }
        public int[] GetMemberIncidence(int memberID)
        {
            dynamic startNodeID = new int();
            dynamic endNodeID = new int();
            dynamic retval = OSGeometryUI.GetMemberIncidence(memberID, ref startNodeID, ref endNodeID);
            if (retval == 0)
            {
                throw new MemberNotFoundException($"Member {memberID} not found");
            }
            return new int[] { startNodeID, endNodeID };
        }
        public double GetMemberLength(int memberID)
        {
            dynamic retval = OSGeometryUI.GetBeamLength(memberID);
            if (retval == 0.0)
            {
                throw new MemberNotFoundException($"Member {memberID} not found");
            }
            return retval;
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
