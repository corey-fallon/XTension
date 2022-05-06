using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSTAADWrapper
{
    public class OpenStaadSupport
    {
        private dynamic OSSupportUI;

        internal OpenStaadSupport(dynamic support)
        {
            OSSupportUI = support;
        }

        public int GetSupportCount()
        {
            return OSSupportUI.GetSupportCount();
        }

        public int[] GetSupportNodes()
        {
            int supportCount = GetSupportCount();
            dynamic supportNodeIDs = new int[supportCount];
            dynamic retval = OSSupportUI.GetSupportNodes(ref supportNodeIDs);
            return supportNodeIDs;
        }
    }
}
