using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTensionCore.DataObjects
{
    public class MeshData
    {
        public MeshFormatSectionData MeshFormatSection { get; set; }
        public NodesSectionData NodesSection { get; set; }
        public ElementsSectionData ElementsSection { get; set; }

        public MeshData()
        {
            MeshFormatSection = new MeshFormatSectionData();
            NodesSection = new NodesSectionData();
            ElementsSection = new ElementsSectionData();
        }
    }
}
