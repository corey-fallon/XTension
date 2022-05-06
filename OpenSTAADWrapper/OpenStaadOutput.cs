using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSTAADWrapper
{
    public class OpenStaadOutput
    {
        private dynamic OSOutputUI;

        internal OpenStaadOutput(dynamic output)
        {
            OSOutputUI = output;
        }

        public double[] GetNodeDisplacements(int nodeNumberID, int loadCaseID)
        {
            dynamic displacements = new double[6];
            dynamic retval = OSOutputUI.GetNodeDisplacements(nodeNumberID, loadCaseID, ref displacements);
            return displacements;
        }

        public double[] GetSupportReactions(int nodeNumberID, int loadCaseID)
        {
            dynamic reactions = new double[6];
            dynamic retval = OSOutputUI.GetSupportReactions(nodeNumberID, loadCaseID, ref reactions);
            return reactions;
        }

        public double[] GetMinMaxAxialForce(int memberID, int loadCaseID)
        {
            dynamic min = new double();
            dynamic minPos = new double();
            dynamic max = new double();
            dynamic maxPos = new double();
            dynamic retval = OSOutputUI.GetMinMaxAxialForce(memberID, loadCaseID, ref min, ref minPos, ref max, ref maxPos);
            return new double[] { min, minPos, max, maxPos };
        }

        public double[] GetIntermediateMemberForcesAtDistance(int memberID, double distance, int loadCaseID)
        {
            dynamic forces = new double[6];
            dynamic retval = OSOutputUI.GetIntermediateMemberForcesAtDistance(memberID, distance, loadCaseID, ref forces);
            return forces;
        }

        public double GetIntermediateMemberForceAtDistance(int memberID, double distance, int loadCaseID, InternalForce internalForce)
        {
            double[] forces = GetIntermediateMemberForcesAtDistance(memberID, distance, loadCaseID);
            return forces[(int)internalForce];
        }

        public double[] GetIntermediateDeflectionAtDistance(int memberID, double distance, int loadCaseID)
        {
            dynamic yDisplacement = new double();
            dynamic zDisplacement = new double();
            dynamic retval = OSOutputUI.GetIntermediateDeflectionAtDistance(memberID, distance, loadCaseID, ref yDisplacement, ref zDisplacement);
            return new double[] { yDisplacement, zDisplacement };
        }

        public double[] GetMaxSectionDisplacement(int memberID, string direction, int loadCaseID)
        {
            dynamic maximumValue = new double();
            dynamic position = new double();
            dynamic retval = OSOutputUI.GetMaxSectionDisplacement(memberID, direction, loadCaseID, ref maximumValue, ref position);
            return new double[] { maximumValue, position };
        }

        public double[] GetMaxSectionDisplacement(int memberID, Direction direction, int loadCaseID)
        {
            return GetMaxSectionDisplacement(memberID, direction.ToString(), loadCaseID);
        }

        public double[] GetMemberEndForces(int memberID, int end, int loadCaseID, int localOrGlobal)
        {
            dynamic forces = new double[6];
            dynamic retval = OSOutputUI.GetMemberEndForces(memberID, end, loadCaseID, ref forces, localOrGlobal);
            return forces;
        }

        public double[] GetMemberEndForces(int memberID, MemberEnd end, int loadCaseID, CoordinateSystem coordinateSystem)
        {
            return GetMemberEndForces(memberID, (int)end, loadCaseID, (int)coordinateSystem);
        }
    }
}
