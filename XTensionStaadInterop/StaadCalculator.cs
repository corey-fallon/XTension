using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenSTAADWrapper;

namespace XTensionStaadInterop
{
    public class StaadCalculator
    {
        public static List<double> GetVirtualWork(int realLoadCaseID, int virtualLoadCaseID)
        {
            List<double> virtualWorkByVolume = new List<double>();
            using (OpenStaad os = new OpenStaad())
            {
                foreach (int beamID in os.Geometry.GetBeamList())
                {
                    double virtualWork = os.CalculateVirtualWorkDueToStrongAxisBending(beamID, realLoadCaseID, virtualLoadCaseID);
                    double volume = os.GetBeamVolume(beamID);
                    virtualWorkByVolume.Add(virtualWork / volume);
                }
            }
            return virtualWorkByVolume;
        }
    }
}
