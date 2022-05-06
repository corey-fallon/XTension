using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSTAADWrapper
{
    public class OpenStaadBeam
    {
        private OpenStaad os;
        public int ID { get; private set; }

        internal OpenStaadBeam(OpenStaad os, int id)
        {
            this.os = os;
            this.ID = id;
        }

        public double Length { get => os.Geometry.GetBeamLength(ID); }
        public double Volume { get => SectionProperties.Ax * Length; }
        public int StartNodeID { get => os.Geometry.GetMemberIncidence(ID)[0]; }
        public int EndNodeID { get => os.Geometry.GetMemberIncidence(ID)[1]; }
        public OpenStaadNode StartNode { get => os.GetNodeByID(StartNodeID); }
        public OpenStaadNode EndNode { get => os.GetNodeByID(EndNodeID); }
        public MaterialConstants MaterialConstants { get => os.Property.GetBeamMaterial(ID); }
        public SectionProperties SectionProperties { get => os.Property.GetBeamSectionPropertyValues(ID); }

        public double GetIntermediateForceAtDistance(double distance, int loadCaseID, InternalForce internalForce)
        {
            return os.Output.GetIntermediateMemberForceAtDistance(ID, distance, loadCaseID, internalForce);
        }

        public double[] GetIntermediateDeflectionAtDistance(double distance, int loadCaseID)
        {
            return os.Output.GetIntermediateDeflectionAtDistance(ID, distance, loadCaseID);
        }

        public double GetMaxSectionDisplacement(Direction direction, int loadCaseID)
        {
            return os.Output.GetMaxSectionDisplacement(ID, direction, loadCaseID)[0];
        }

        public double GetMaxSectionDisplacementPosition(Direction direction, int loadCaseID)
        {
            return os.Output.GetMaxSectionDisplacement(ID, direction, loadCaseID)[1];
        }

        public double[] GetEndForces(int end, int loadCaseID, int localOrGlobal)
        {
            return os.Output.GetMemberEndForces(ID, end, loadCaseID, localOrGlobal);
        }

        public double[] GetEndForces(MemberEnd end, int loadCaseID, CoordinateSystem coordinateSystem)
        {
            return os.Output.GetMemberEndForces(ID, end, loadCaseID, coordinateSystem);
        }

        public double[] GetMinMaxAxialForce(int loadCaseID)
        {
            return os.Output.GetMinMaxAxialForce(ID, loadCaseID);
        }

        public double GetMinimumAxialForce(int loadCaseID)
        {
            return GetMinMaxAxialForce(loadCaseID)[0];
        }

        public double GetMinimumAxialForcePosition(int loadCaseID)
        {
            return GetMinMaxAxialForce(loadCaseID)[1];
        }

        public double GetMaximumAxialForce(int loadCaseID)
        {
            return GetMinMaxAxialForce(loadCaseID)[2];
        }

        public double GetMaximumAxialForceLocation(int loadCaseID)
        {
            return GetMinMaxAxialForce(loadCaseID)[3];
        }


        public List<double> GetPositionsOnLength(int numberOfPoints)
        {
            List<double> positionsOnLength = new List<double>();
            for (int i = 0; i < numberOfPoints; i++)
            {
                positionsOnLength.Add(Length * i / (numberOfPoints - 1));
            }
            return positionsOnLength;
        }

        public double GetVirtualWorkDueToStrongAxisBending(int realLoadCaseID, int virtualLoadCaseID)
        {
            var realMomentFunction = GetInternalForceFunction(realLoadCaseID, InternalForce.Mz);
            var virtualMomentFunction = GetInternalForceFunction(virtualLoadCaseID, InternalForce.Mz);
            double value = 0;
            double xi;
            double xj;
            double Yi;
            double Yj;
            double yi;
            double yj;
            for (int i = 0; i < realMomentFunction.NumberOfValues - 1; i++)
            {
                xi = realMomentFunction.xValues[i];
                xj = realMomentFunction.xValues[i + 1];
                Yi = realMomentFunction.yValues[i];
                Yj = realMomentFunction.yValues[i + 1];
                yi = virtualMomentFunction.yValues[i];
                yj = virtualMomentFunction.yValues[i + 1];

                value += (xj - xi) / 6 * (yi * (2 * Yi + Yj) + yj * (Yi + 2 * Yj));
            }
            double E = MaterialConstants.ModulusOfElasticity;
            double I = SectionProperties.Iz;
            return value / (E * I);
        }

        public DiscreteUnivariateFunction GetInternalForceFunction(int loadCaseID, InternalForce internalForce)
        {
            double distance;
            double internalForceValue;
            DiscreteUnivariateFunction internalForceFunction = new DiscreteUnivariateFunction();
            for (int i = 0; i < 13; i++)
            {
                distance = Length * i / 12;
                internalForceValue = GetIntermediateForceAtDistance(distance, loadCaseID, internalForce);
                internalForceFunction.Insert(distance, internalForceValue);
            }
            return internalForceFunction;
        }
    }
}
