using Autodesk.DesignScript.Geometry;
using System.Collections.Generic;
using XTensionCore.DataObjects;

namespace XTensionCore.Mappers
{
    public class MeshDataMapper
    {
        public static Mesh ToMesh(MeshData meshData)
        {
            List<Point> vertexPositions = new List<Point>();
            List<IndexGroup> indicies = new List<IndexGroup>();
            Dictionary<int, int> pointIndexByNodeDataTag = new Dictionary<int, int>();
            int i = 0;
            foreach (var nodeBlock in meshData.NodesSection.NodeBlocks)
            {
                foreach (var nodeData in nodeBlock.Nodes)
                {
                    vertexPositions.Add(NodeDataMapper.ToPoint(nodeData));
                    pointIndexByNodeDataTag.Add(nodeData.Tag, i++);
                }
            }
            foreach (var elementBlock in meshData.ElementsSection.ElementBlocks)
            {
                foreach (var elementData in elementBlock.Elements)
                {
                    int numberOfNodes = elementData.NodeEntityTags.Count;
                    if (numberOfNodes == 3)
                    {
                        uint a = (uint)pointIndexByNodeDataTag[elementData.NodeEntityTags[0]];
                        uint b = (uint)pointIndexByNodeDataTag[elementData.NodeEntityTags[1]];
                        uint c = (uint)pointIndexByNodeDataTag[elementData.NodeEntityTags[2]];
                        indicies.Add(IndexGroup.ByIndices(a, b, c));
                    }
                    if (numberOfNodes == 4)
                    {
                        uint a = (uint)pointIndexByNodeDataTag[elementData.NodeEntityTags[0]];
                        uint b = (uint)pointIndexByNodeDataTag[elementData.NodeEntityTags[1]];
                        uint c = (uint)pointIndexByNodeDataTag[elementData.NodeEntityTags[2]];
                        uint d = (uint)pointIndexByNodeDataTag[elementData.NodeEntityTags[3]];
                        indicies.Add(IndexGroup.ByIndices(a, b, c, d));
                    }
                }
            }

            return Mesh.ByPointsFaceIndices(vertexPositions, indicies);
        }
    }
}
