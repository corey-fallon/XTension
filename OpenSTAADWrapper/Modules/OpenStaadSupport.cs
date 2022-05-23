using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStaadSharp
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
        public void RemoveSupportFromNode(int nodeID)
        {
            dynamic retval = OSSupportUI.RemoveSupportFromNode(nodeID);
        }
        public void RemoveSupportFromAllNodes()
        {
            int[] supportNodeIDs = GetSupportNodes();
            foreach (var supportNodeID in supportNodeIDs)
            {
                RemoveSupportFromNode(supportNodeID);
            }
        }
        public int CreateSupportFixed()
        {
            dynamic retval = OSSupportUI.CreateSupportFixed();
            return retval;
        }
        public void DeleteSupport(int supportID)
        {
            OSSupportUI.DeleteSupport(supportID);
        }
        public void AssignSupportToNode(int nodeID, int supportID)
        {
            dynamic retval = OSSupportUI.AssignSupportToNode(nodeID, supportID);
        }
    }
}
