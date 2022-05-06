using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XTensionStaadInterop;

using Autodesk.DesignScript.Geometry;

namespace XTension
{
    public class MemberXTension
    {
        private MemberXTension()
        {
        }

        public static List<Line> ImportStaadMembers()
        {
            return StaadImporter.ImportBeams();
        }

        public static List<double> GetVirtualWork(int realLoadCaseID, int virtualLoadCaseID)
        {
            return StaadCalculator.GetVirtualWork(realLoadCaseID, virtualLoadCaseID);
        }

        public static double[] GetAxialForces(int loadCaseID)
        {
            return StaadImporter.GetAxialForce(loadCaseID).ToArray();
        }

        public static List<double> NormalizeList(List<double> list)
        {
            return StaadImporter.NormalizeList(list);
        }
    }
}
