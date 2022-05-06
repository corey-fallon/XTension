using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.DesignScript.Geometry;

namespace XTension
{
    public class PolygonXTension
    {
        private PolygonXTension()
        {

        }

        public Point GetCentroid(Polygon polygon)
        {
            return Point.ByCoordinates(0, 0, 0);
        }
    }
}
