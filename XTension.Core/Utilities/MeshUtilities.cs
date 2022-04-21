using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Geometry;
using XTensionCore.Domain;

namespace XTensionCore.Utilities
{
    public static class MeshUtilities
    {
        public static List<Line> GetEdgeLines(Mesh mesh)
        {
            return GetEdges(mesh).ToLines();
        }

        public static HashSet<FaceEdge> GetEdges(Mesh mesh)
        {
            HashSet<FaceEdge> edges = new HashSet<FaceEdge>();
            foreach (MeshFace face in GetFaces(mesh))
            {
                edges.AddRange(face.GetEdges());
            }
            return edges;
        }

        public static List<MeshFace> GetFaces(Mesh mesh)
        {
            List<MeshFace> faces = new List<MeshFace>();
            foreach (IndexGroup indexGroup in mesh.FaceIndices)
            {
                if (indexGroup.Count == 3)
                {
                    Point a = mesh.VertexPositions[indexGroup.A];
                    Point b = mesh.VertexPositions[indexGroup.B];
                    Point c = mesh.VertexPositions[indexGroup.C];

                    faces.Add(new MeshFace(a, b, c));
                }
                else if (indexGroup.Count == 4)
                {
                    Point a = mesh.VertexPositions[indexGroup.A];
                    Point b = mesh.VertexPositions[indexGroup.B];
                    Point c = mesh.VertexPositions[indexGroup.C];
                    Point d = mesh.VertexPositions[indexGroup.D];

                    faces.Add(new MeshFace(a, b, c, d));
                }
            }
            return faces;
        }

        public static void AddRange(this HashSet<FaceEdge> edges, List<FaceEdge> edgesToAdd)
        {
            foreach (FaceEdge edge in edgesToAdd)
            {
                edges.Add(edge);
            }
        }
    }
}
