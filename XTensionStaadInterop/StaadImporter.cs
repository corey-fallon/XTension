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
        public static List<Line> ImportBeams()
        {
            List<Line> lines = new List<Line>();
            using (OpenStaad os = new OpenStaad())
            {
                foreach (var beam in os.GetBeams())
                {
                    OpenStaadNode startNode = beam.StartNode;
                    OpenStaadNode endNode = beam.EndNode;
                    Point startPoint = Point.ByCoordinates(startNode.ZCoordinate, startNode.XCoordinate, startNode.YCoordinate);
                    Point endPoint = Point.ByCoordinates(endNode.ZCoordinate, endNode.XCoordinate, endNode.YCoordinate);
                    lines.Add(Line.ByStartPointEndPoint(startPoint, endPoint));
                    startPoint.Dispose();
                    endPoint.Dispose();
                }
            }
            return lines;
        }

        public static List<double> GetAxialForce(int loadCaseID)
        {
            List<double> axialForce = new List<double>();
            using (OpenStaad os = new OpenStaad())
            {
                foreach (var beam in os.GetBeams())
                {
                    double min = beam.GetMinimumAxialForce(loadCaseID);
                    double max = beam.GetMaximumAxialForce(loadCaseID);
                    if (Math.Abs(min) > max)
                    {
                        axialForce.Add(min);
                    }
                    else
                    {
                        axialForce.Add(max);
                    }
                }
            }
            return axialForce;
        }

        public static List<double> NormalizeList(List<double> list)
        {
            List<double> normalizedList = new List<double>();
            double min = list.Min();
            double max = list.Max();
            foreach (double item in list)
            {
                normalizedList.Add((item - min) / (max - min));
            }
            return normalizedList;
        }
    }
}
