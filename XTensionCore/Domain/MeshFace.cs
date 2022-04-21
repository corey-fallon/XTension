using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.DesignScript.Geometry;

namespace XTensionCore.Domain
{
    public class MeshFace
    {
        private Point[] verticies;

        public int NumberOfEdges => verticies.Length;

        public MeshFace(Point a, Point b, Point c)
        {
            verticies = new Point[] { a, b, c };
        }

        public MeshFace(Point a, Point b, Point c, Point d)
        {
            verticies = new Point[] { a, b, c, d };
        }

        internal List<FaceEdge> GetEdges()
        {
            List<FaceEdge> edges = new List<FaceEdge>();
            for (int i = 0; i < NumberOfEdges; i++)
            {
                bool isLastEdge = (i == NumberOfEdges - 1);
                if (isLastEdge)
                {
                    edges.Add(FaceEdge.ByStartPointEndPoint(verticies[i], verticies[0]));
                }
                else
                {
                    edges.Add(FaceEdge.ByStartPointEndPoint(verticies[i], verticies[i + 1]));
                }
            }
            return edges;
        }
    }
}
