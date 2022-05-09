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

        public Node GetNodeByID(int nodeID)
        {
            if (NodeExists(nodeID))
            {
                return new Node(this, nodeID);
            }
            else
            {
                throw new Exception($"Node {nodeID} does not exist");
            }
        }

        public bool NodeExists(int nodeID)
        {
            return Array.Exists<int>(Geometry.GetNodeList(), x => x == nodeID);
        }

        public List<Node> GetNodes()
        {
            List<Node> nodes = new List<Node>();
            foreach (var nodeID in Geometry.GetNodeList())
            {
                nodes.Add(GetNodeByID(nodeID));
            }
            return nodes;
        }

        public Member GetMemberByID(int beamID)
        {
            if (MemberExists(beamID))
            {
                return new Member(this, beamID);
            }
            else
            {
                throw new Exception($"Beam {beamID} does not exist");
            }
        }

        public bool MemberExists(int memberID)
        {
            return Array.Exists<int>(Geometry.GetBeamList(), x => x == memberID);
        }

        public List<Member> GetMembers()
        {
            List<Member> members = new List<Member>();
            foreach (var memberID in Geometry.GetBeamList())
            {
                members.Add(GetMemberByID(memberID));
            }
            return members;
        }

        public List<Node> GetSupportNodes()
        {
            List<Node> nodes = new List<Node>();
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
