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
    }
}
