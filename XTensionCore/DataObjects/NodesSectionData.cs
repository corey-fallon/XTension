using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTensionCore.DataObjects
{
    public class NodesSectionData
    {
        public int NumberOfNodeBlocks { get; internal set; }
        public int NumberOfNodes { get; internal set; }
        public int MinimumNodeTag { get; internal set; }
        public int MaximumNodeTag { get; internal set; }
        public List<NodeBlockData> NodeBlocks { get; internal set; }

        public NodesSectionData()
        {
            NodeBlocks = new List<NodeBlockData>();
        }
    }
}
