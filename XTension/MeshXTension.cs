using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Geometry;

using XTensionCore.Parsers;
using XTensionCore.Mappers;
using XTensionCore.DataObjects;
using XTensionCore.Utilities;

using XTensionStaadInterop;

namespace XTension
{
    public static class MeshXTension
    {
        public static List<Line> GetEdgeLines(Mesh mesh)
        {
            return MeshUtilities.GetEdgeLines(mesh);
        }

        public static Mesh ImportFile(string path)
        {
            MeshData meshData;

            using (StreamReader sr = new StreamReader(path))
            {
                meshData = new MshFileParser().Parse(sr);
            }

            return MeshDataMapper.ToMesh(meshData);
        }

        public static void ExportToSTAAD(Mesh mesh)
        {
            StaadExporter.Export(mesh);
        }
    }
}
