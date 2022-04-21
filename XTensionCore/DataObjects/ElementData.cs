using System.Collections.Generic;

namespace XTensionCore.DataObjects
{
    public class ElementData
    {
        public int Tag { get; set; }
        public List<int> NodeEntityTags { get; set; }
        public int Type { get; set; }

        public ElementData()
        {
            NodeEntityTags = new List<int>();
        }
    }
}