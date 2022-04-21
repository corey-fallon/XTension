using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.DesignScript.Geometry;

using XTensionCore.Domain;

namespace XTensionCore.Utilities
{
    public static class LineUtilities
    {
        public static List<Line> ToLines(this HashSet<FaceEdge> edges)
        {
            List<Line> lines = new List<Line>();
            foreach (FaceEdge edge in edges)
            {
                lines.Add(edge.ToLine());
            }
            return lines;
        }
    }
}
