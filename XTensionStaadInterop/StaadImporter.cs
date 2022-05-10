using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenSTAADWrapper;
using Autodesk.DesignScript.Geometry;

namespace XTensionStaadInterop
{
    public class StaadImporter
    {
        //public static List<Line> ImportBeams()
        //{
        //    List<Line> lines = new List<Line>();
        //    using (OpenStaad os = new OpenStaad())
        //    {
        //        foreach (var beam in os.GetMembers())
        //        {
        //            Node startNode = beam.StartNode;
        //            Node endNode = beam.EndNode;
        //            Point startPoint = Point.ByCoordinates(startNode.ZCoordinate, startNode.XCoordinate, startNode.YCoordinate);
        //            Point endPoint = Point.ByCoordinates(endNode.ZCoordinate, endNode.XCoordinate, endNode.YCoordinate);
        //            lines.Add(Line.ByStartPointEndPoint(startPoint, endPoint));
        //            startPoint.Dispose();
        //            endPoint.Dispose();
        //        }
        //    }
        //    return lines;
        //}
    }
}
