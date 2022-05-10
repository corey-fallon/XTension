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
            return StaadCalculator.MemberDisplayValues(new VirtualWorkByVolume(realLoadCaseID, virtualLoadCaseID));
        }

        public static List<double> GetAxialForce(int loadCaseID)
        {
            return StaadCalculator.MemberDisplayValues(new SignInvarientMaximumAxialForce(loadCaseID));
        }
    }
}
