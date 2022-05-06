using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSTAADWrapper
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
    }
}
