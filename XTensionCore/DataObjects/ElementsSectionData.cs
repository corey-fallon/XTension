using System.Collections;
using System.Collections.Generic;

namespace XTensionCore.DataObjects
{
    public class ElementsSectionData
    {
        public int NumberOfEntityBlocks { get; internal set; }
        public int NumberOfElements { get; internal set; }
        public int MinimumElementTag { get; internal set; }
        public int MaximumElementTag { get; internal set; }
        public List<ElementBlockData> ElementBlocks { get; private set; }

        public ElementsSectionData()
        {
            ElementBlocks = new List<ElementBlockData>();
        }
    }
}