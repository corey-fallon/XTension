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
                OpenStaadGeometry geometry = os.Geometry;

                int[] beamList = geometry.GetBeamList();

                foreach (int beamId in beamList)
                {
                    int[] incidence = geometry.GetMemberIncidence(beamId);
                    int nodeA = incidence[0];
                    int nodeB = incidence[1];
                    double[] startNodeCoordinates = geometry.GetNodeCoordinates(nodeA);
                    double[] endNodeCoordinates = geometry.GetNodeCoordinates(nodeB);
                    Point startPoint = Point.ByCoordinates(startNodeCoordinates[2], startNodeCoordinates[0], startNodeCoordinates[1]);
                    Point endPoint = Point.ByCoordinates(endNodeCoordinates[2], endNodeCoordinates[0], endNodeCoordinates[1]);
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
                OpenStaadGeometry geometry = os.Geometry;
                OpenStaadOutput output = os.Output;
                int[] beamList = geometry.GetBeamList();

                foreach (int beamID in beamList)
                {
                    double[] minMaxAxialForce = output.GetMinMaxAxialForce(beamID, loadCaseID);
                    double min = minMaxAxialForce[0];
                    double max = minMaxAxialForce[2];
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
