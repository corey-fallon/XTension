using System.Collections;
using System.Collections.Generic;

namespace XTensionCore.DataObjects
{
    public class NodeBlockData
    {
        public int EntityDimension { get; internal set; }
        public int EntityTag { get; internal set; }
        public int Parametric { get; internal set; }
        public int NumberOfNodesInBlock { get; internal set; }
        public List<NodeData> Nodes { get; internal set; }

        public NodeBlockData()
        {
            Nodes = new List<NodeData>();
        }
    }
}