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
                foreach (var beam in os.GetMembers())
                {
                    double virtualWork = beam.GetVirtualWorkDueToStrongAxisBending(realLoadCaseID, virtualLoadCaseID);
                    double volume = beam.Volume;
                    virtualWorkByVolume.Add(virtualWork / volume);
                }
            }
            return virtualWorkByVolume;
        }
    }
}
