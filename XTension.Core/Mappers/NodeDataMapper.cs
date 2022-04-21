using Autodesk.DesignScript.Geometry;
using XTensionCore.DataObjects;

namespace XTensionCore.Mappers
{
    public class NodeDataMapper
    {
        public static Point ToPoint(NodeData nodeData)
        {
            return Point.ByCoordinates(nodeData.X, nodeData.Y, nodeData.Z);
        }
    }
}
