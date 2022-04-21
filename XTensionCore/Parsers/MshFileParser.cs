using System;
using System.IO;
using XTensionCore.DataObjects;

namespace XTensionCore.Parsers
{
    public class MshFileParser
    {
        public MeshData Parse(StreamReader sr)
        {
            MeshData mshFile = new MeshData();

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                switch (line)
                {
                    case "$MeshFormat":
                        MeshFormatSectionData meshFormatSection = ParseMeshFormatSection(sr);
                        mshFile.MeshFormatSection = meshFormatSection;
                        break;
                    case "$Nodes":
                        NodesSectionData nodesSection = ParseNodesSection(sr);
                        mshFile.NodesSection = nodesSection;
                        break;
                    case "$Elements":
                        ElementsSectionData elementsSection = ParseElementsSection(sr);
                        mshFile.ElementsSection = elementsSection;
                        break;
                    default:
                        break;
                }
            }

            return mshFile;
        }

        private MeshFormatSectionData ParseMeshFormatSection(StreamReader sr)
        {
            MeshFormatSectionData meshFormatSection = ParseMeshFormatSection(sr.ReadLine());
            return meshFormatSection;
        }

        private NodesSectionData ParseNodesSection(StreamReader sr)
        {
            NodesSectionData nodesSection = ParseNodesSectionPropertiesLine(sr.ReadLine());

            for (int i = 0; i < nodesSection.NumberOfNodeBlocks; i++)
            {
                NodeBlockData nodeBlock = ParseNodeBlockPropertiesLine(sr.ReadLine());

                for (int j = 0; j < nodeBlock.NumberOfNodesInBlock; j++)
                {
                    NodeData node = ParseNodeTagLine(sr.ReadLine());
                    nodeBlock.Nodes.Add(node);
                }

                for (int j = 0; j < nodeBlock.NumberOfNodesInBlock; j++)
                {
                    ParseNodeCoordinatesLine(nodeBlock.Nodes[j], sr.ReadLine());
                }

                nodesSection.NodeBlocks.Add(nodeBlock);
            }

            return nodesSection;
        }

        private ElementsSectionData ParseElementsSection(StreamReader sr)
        {
            ElementsSectionData elementsSection = ParseElementsSectionPropertiesLine(sr.ReadLine());

            for (int i = 0; i < elementsSection.NumberOfEntityBlocks; i++)
            {
                ElementBlockData elementBlock = ParseElementBlockPropertiesLine(sr.ReadLine());

                for (int j = 0; j < elementBlock.NumberOfElementsInBlock; j++)
                {
                    ElementData element = ParseElementTagLine(sr.ReadLine());
                    elementBlock.Elements.Add(element);
                }

                elementsSection.ElementBlocks.Add(elementBlock);
            }

            return elementsSection;
        }

        private NodesSectionData ParseNodesSectionPropertiesLine(string line)
        {
            string[] stringValues = line.Trim().Split();
            NodesSectionData nodesSection = new NodesSectionData();
            nodesSection.NumberOfNodeBlocks = Convert.ToInt32(stringValues[0]);
            nodesSection.NumberOfNodes = Convert.ToInt32(stringValues[1]);
            nodesSection.MinimumNodeTag = Convert.ToInt32(stringValues[2]);
            nodesSection.MaximumNodeTag = Convert.ToInt32(stringValues[3]);
            return nodesSection;
        }

        private NodeBlockData ParseNodeBlockPropertiesLine(string line)
        {
            string[] stringValues = line.Trim().Split();
            NodeBlockData nodeBlock = new NodeBlockData();
            nodeBlock.EntityDimension = Convert.ToInt32(stringValues[0]);
            nodeBlock.EntityTag = Convert.ToInt32(stringValues[1]);
            nodeBlock.Parametric = Convert.ToInt32(stringValues[2]);
            nodeBlock.NumberOfNodesInBlock = Convert.ToInt32(stringValues[3]);
            return nodeBlock;
        }

        private NodeData ParseNodeTagLine(string line)
        {
            string[] stringValues = line.Trim().Split();
            NodeData node = new NodeData();
            node.Tag = Convert.ToInt32(stringValues[0]);
            return node;
        }

        private void ParseNodeCoordinatesLine(NodeData node, string line)
        {
            string[] stringValues = line.Trim().Split();
            node.X = Convert.ToDouble(stringValues[0]);
            node.Y = Convert.ToDouble(stringValues[1]);
            node.Z = Convert.ToDouble(stringValues[2]);
        }

        private ElementsSectionData ParseElementsSectionPropertiesLine(string line)
        {
            string[] stringValues = line.Trim().Split();
            ElementsSectionData elementsSection = new ElementsSectionData();
            elementsSection.NumberOfEntityBlocks = Convert.ToInt32(stringValues[0]);
            elementsSection.NumberOfElements = Convert.ToInt32(stringValues[1]);
            elementsSection.MinimumElementTag = Convert.ToInt32(stringValues[2]);
            elementsSection.MaximumElementTag = Convert.ToInt32(stringValues[3]);
            return elementsSection;
        }

        private MeshFormatSectionData ParseMeshFormatSection(string line)
        {
            string[] stringValues = line.Trim().Split();
            MeshFormatSectionData meshFormatSection = new MeshFormatSectionData();
            meshFormatSection.Version = stringValues[0];
            meshFormatSection.FileFormat = Convert.ToInt32(stringValues[1]);
            meshFormatSection.DataSize = Convert.ToInt32(stringValues[2]);
            return meshFormatSection;
        }

        private ElementBlockData ParseElementBlockPropertiesLine(string line)
        {
            string[] stringValues = line.Trim().Split();
            ElementBlockData elementBlock = new ElementBlockData();
            elementBlock.EntityDimension = Convert.ToInt32(stringValues[0]);
            elementBlock.EntityTag = Convert.ToInt32(stringValues[1]);
            elementBlock.ElementType = Convert.ToInt32(stringValues[2]);
            elementBlock.NumberOfElementsInBlock = Convert.ToInt32(stringValues[3]);
            return elementBlock;
        }

        private ElementData ParseElementTagLine(string line)
        {
            string[] stringValues = line.Trim().Split();
            ElementData element = new ElementData();
            element.Tag = Convert.ToInt32(stringValues[0]);
            for (int i = 1; i < stringValues.Length; i++)
            {
                element.NodeEntityTags.Add(Convert.ToInt32(stringValues[i]));
            }
            return element;
        }
    }
}
