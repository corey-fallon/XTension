using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.DesignScript.Geometry;
using Autodesk.Revit.DB;
using RevitServices.Persistence;

namespace XTension
{
    public class RevitXTension
    {
        public static IEnumerable<Element> GetAnalyticalNodes()
        {
            Document doc = DocumentManager.Instance.CurrentDBDocument;
            var collector = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_AnalyticalNodes);
            return collector.AsEnumerable<Element>();
        }
    }
}
