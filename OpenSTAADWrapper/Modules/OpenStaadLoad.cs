using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStaadSharp
{
    public class OpenStaadLoad
    {
        private dynamic OSLoadUI;

        internal OpenStaadLoad(dynamic load)
        {
            OSLoadUI = load;
        }

        public int GetPrimaryLoadCaseCount()
        {
            return OSLoadUI.GetPrimaryLoadCaseCount();
        }

        public int[] GetPrimaryLoadCaseNumbers()
        {
            int primaryLoadCaseCount = GetPrimaryLoadCaseCount();
            dynamic loadCaseIDs = new int[primaryLoadCaseCount];
            dynamic retval = OSLoadUI.GetPrimaryLoadCaseNumbers(ref loadCaseIDs);
            return loadCaseIDs;
        }

        public int CreateNewPrimaryLoad(string primaryLoadTitle)
        {
            return OSLoadUI.CreateNewPrimaryLoad(primaryLoadTitle);
        }

        public bool SetLoadActive(int loadCaseID)
        {
            return OSLoadUI.SetLoadActive(loadCaseID);
        }

        public bool AddNodalLoad(int nodeID, double fx, double fy, double fz, double mx, double my, double mz)
        {
            return OSLoadUI.AddNodalLoad(nodeID, fx, fy, fz, mx, my, mz);
        }

        public int DeletePrimaryLoadCases(int primaryLoadCaseNo, bool referenceLoads)
        {
            return OSLoadUI.DeletePrimaryLoadCases(primaryLoadCaseNo, referenceLoads);
        }
    }
}
