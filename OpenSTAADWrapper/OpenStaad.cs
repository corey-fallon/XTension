using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace OpenSTAADWrapper
{
    public class OpenStaad : IDisposable
    {
        private dynamic OpenSTAAD;
        public OpenStaadGeometry Geometry { get => new OpenStaadGeometry(OpenSTAAD.Geometry); }
        public OpenStaadOutput Output { get => new OpenStaadOutput(OpenSTAAD.Output); }
        public OpenStaadSupport Support { get => new OpenStaadSupport(OpenSTAAD.Support); }
        public OpenStaadLoad Load { get => new OpenStaadLoad(OpenSTAAD.Load); }
        public OpenStaadProperty Property { get => new OpenStaadProperty(OpenSTAAD.Property); }

        public OpenStaad()
        {
            OpenSTAAD = Marshal.GetActiveObject("StaadPro.OpenSTAAD");
        }

        public OpenStaadNode GetNodeByID(int nodeID)
        {
            return new OpenStaadNode(this, nodeID);
        }

        public List<OpenStaadNode> GetNodes()
        {
            List<OpenStaadNode> nodes = new List<OpenStaadNode>();
            foreach (var nodeID in Geometry.GetNodeList())
            {
                nodes.Add(GetNodeByID(nodeID));
            }
            return nodes;
        }

        public OpenStaadBeam GetBeamByID(int beamID)
        {
            return new OpenStaadBeam(this, beamID);
        }

        public List<OpenStaadBeam> GetBeams()
        {
            List<OpenStaadBeam> beams = new List<OpenStaadBeam>();
            foreach (var beamID in Geometry.GetBeamList())
            {
                beams.Add(GetBeamByID(beamID));
            }
            return beams;
        }

        public List<OpenStaadNode> GetSupportNodes()
        {
            List<OpenStaadNode> nodes = new List<OpenStaadNode>();
            foreach (var nodeID in Support.GetSupportNodes())
            {
                nodes.Add(GetNodeByID(nodeID));
            }
            return nodes;
        }

        public void Dispose()
        {
            OpenSTAAD = null;
        }
    }
}
