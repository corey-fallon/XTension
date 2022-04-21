using System.Collections;
using System.Collections.Generic;

namespace XTensionCore.DataObjects
{
    public class ElementBlockData
    {
        public int EntityDimension { get; internal set; }
        public int EntityTag { get; internal set; }
        public int ElementType { get; internal set; }
        public int NumberOfElementsInBlock { get; internal set; }
        public List<ElementData> Elements { get; internal set; }

        public ElementBlockData()
        {
            Elements = new List<ElementData>();
        }
    }
}