using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

using OpenSTAADWrapper;
using Autodesk.DesignScript.Geometry;

namespace XTensionStaadInterop
{
    public static class StaadExporter
    {
        public static void Export(Mesh mesh)
        {
            using (OpenStaad os = new OpenStaad())
            {
                OpenStaadGeometry geometry = os.Geometry;

                Dictionary<int, int> staadNodeIdByVertexPositionIndex = new Dictionary<int, int>();

                Point point;
                for (int i = 0; i < mesh.VertexPositions.Length; i++)
                {
                    point = mesh.VertexPositions[i];
                    int staadNodeId = geometry.AddNode(point.X, point.Y, point.Z);
                    staadNodeIdByVertexPositionIndex.Add(i, staadNodeId);
                }

                foreach (IndexGroup indexGroup in mesh.FaceIndices)
                {
                    int staadNodeIdA = staadNodeIdByVertexPositionIndex[(int)indexGroup.A];
                    int staadNodeIdB = staadNodeIdByVertexPositionIndex[(int)indexGroup.B];
                    int staadNodeIdC = staadNodeIdByVertexPositionIndex[(int)indexGroup.C];
                    if (indexGroup.Count == 3)
                    {
                        geometry.AddPlate(staadNodeIdA, staadNodeIdB, staadNodeIdC);
                    }

                    if (indexGroup.Count == 4)
                    {
                        int staadNodeIdD = staadNodeIdByVertexPositionIndex[(int)indexGroup.D];
                        geometry.AddPlate(staadNodeIdA, staadNodeIdB, staadNodeIdC, staadNodeIdD);
                    }
                }
            }
        }
    }
}
