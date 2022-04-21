using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.DesignScript.Geometry;

namespace XTensionCore.Utilities
{
    public static class PointUtilities
    {
        public static bool Equals(Point point, Point other)
        {
            return point.X == other.X && point.Y == other.Y && point.Z == other.Z;
        }
    }
}
