using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSTAADWrapper
{
    public class Node
    {
        private OpenStaad os;
        public int ID { get; private set; }

        public Node(OpenStaad os, int id)
        {
            this.os = os;
            this.ID = id;
        }

        public double XCoordinate { get => os.Geometry.GetNodeCoordinates(ID)[0]; }
        public double YCoordinate { get => os.Geometry.GetNodeCoordinates(ID)[1]; }
        public double ZCoordinate { get => os.Geometry.GetNodeCoordinates(ID)[2]; }

        public double[] GetDisplacements(int loadCaseID)
        {
            return os.Output.GetNodeDisplacements(ID, loadCaseID);
        }

        public double[] GetSupportReactions(int loadCaseID)
        {
            return os.Output.GetSupportReactions(ID, loadCaseID);
        }
    }
}
