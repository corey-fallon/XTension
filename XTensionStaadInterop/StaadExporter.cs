using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using OpenSTAADUI;
using Autodesk.DesignScript.Geometry;

namespace XTensionStaadInterop
{
    public static class StaadExporter
    {
        public static void Export(Mesh mesh)
        {
            OpenSTAAD openStaad = (OpenSTAAD)Marshal.GetActiveObject("StaadPro.OpenSTAAD");
            OSGeometryUI geometry = openStaad.Geometry;

            Dictionary<int, int> staadNodeIdByVertexPositionIndex = new Dictionary<int, int>();

            Point point;
            for (int i = 0; i < mesh.VertexPositions.Length; i++)
            {
                point = mesh.VertexPositions[i];
                int staadNodeId = AddNode(geometry, point.X, point.Y, point.Z);
                staadNodeIdByVertexPositionIndex.Add(i, staadNodeId);
            }

            foreach (IndexGroup indexGroup in mesh.FaceIndices)
            {
                if (indexGroup.Count == 3)
                {
                    int staadNodeIdA = staadNodeIdByVertexPositionIndex[(int)indexGroup.A];
                    int staadNodeIdB = staadNodeIdByVertexPositionIndex[(int)indexGroup.B];
                    int staadNodeIdC = staadNodeIdByVertexPositionIndex[(int)indexGroup.C];
                    AddPlate(geometry, staadNodeIdA, staadNodeIdB, staadNodeIdC, null);
                }

                if (indexGroup.Count == 4)
                {
                    int staadNodeIdA = staadNodeIdByVertexPositionIndex[(int)indexGroup.A];
                    int staadNodeIdB = staadNodeIdByVertexPositionIndex[(int)indexGroup.B];
                    int staadNodeIdC = staadNodeIdByVertexPositionIndex[(int)indexGroup.C];
                    int staadNodeIdD = staadNodeIdByVertexPositionIndex[(int)indexGroup.D];
                    AddPlate(geometry, staadNodeIdA, staadNodeIdB, staadNodeIdC, staadNodeIdD);
                }
            }

            openStaad = null;
        }

        public static int AddNode(OSGeometryUI geometry, double coordX, double coordY, double coordZ)
        {
            dynamic retval = geometry.AddNode(coordX, coordY, coordZ);
            return retval;
        }

        public static int AddPlate(OSGeometryUI geometry, int nodeA, int nodeB, int nodeC, int? nodeD)
        {
            dynamic retval = geometry.AddPlate(nodeA, nodeB, nodeC, nodeD);
            return retval;
        }
    }
}
