using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Geometry;
using XTensionCore.Utilities;

namespace XTensionCore.Domain
{
    public class FaceEdge
    {
        public Point startPoint;
        public Point endPoint;

        public FaceEdge(Point startPoint, Point endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }

        public static FaceEdge ByStartPointEndPoint(Point startPoint, Point endPoint)
        {
            return new FaceEdge(startPoint, endPoint);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            FaceEdge edge = (FaceEdge)obj;

            bool pointsAreCoincident = (PointUtilities.Equals(startPoint, edge.startPoint)
                                        && PointUtilities.Equals(endPoint, edge.endPoint));

            bool pointsAreSameNotCoincident = (PointUtilities.Equals(startPoint, edge.endPoint)
                                        && PointUtilities.Equals(endPoint, edge.startPoint));

            return pointsAreCoincident || pointsAreSameNotCoincident;
        }

        public override int GetHashCode()
        {
            int result = 17;
            long x = Convert.ToInt64(startPoint.X + endPoint.X);
            long y = Convert.ToInt64(startPoint.Y + endPoint.Y);
            long z = Convert.ToInt64(startPoint.Z + endPoint.Z);
            result = 37 * result + (int)(x ^ (long)((ulong)x >> 32));
            result = 37 * result + (int)(y ^ (long)((ulong)y >> 32));
            result = 37 * result + (int)(z ^ (long)((ulong)z >> 32));
            return result;
        }

        public Line ToLine()
        {
            return Line.ByStartPointEndPoint(startPoint, endPoint);
        }
    }
}
