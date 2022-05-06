using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

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

        public double CalculateVirtualWorkDueToStrongAxisBending(int beamID, int realLoadCase, int virtualLoadCase)
        {
            MaterialConstants material = Property.GetBeamMaterial(beamID);
            SectionProperties properties = Property.GetBeamSectionPropertyValues(beamID);
            var realMomentFunction = GetInternalForceFunction(beamID, realLoadCase, InternalForce.Mz);
            var virtualMomentFunction = GetInternalForceFunction(beamID, virtualLoadCase, InternalForce.Mz);
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
            double E = material.ModulusOfElasticity;
            double I = properties.Iz;
            return value / (E * I);
        }

        public DiscreteUnivariateFunction GetInternalForceFunction(int beamID, int loadCaseID, InternalForce internalForce)
        {
            double beamLength = Geometry.GetBeamLength(beamID);
            double distance;
            double internalForceValue;
            DiscreteUnivariateFunction internalForceFunction = new DiscreteUnivariateFunction();
            for (int i = 0; i < 13; i++)
            {
                distance = beamLength * i / 12;
                internalForceValue = Output.GetIntermediateMemberForceAtDistance(beamID, distance, loadCaseID, internalForce);
                internalForceFunction.Insert(distance, internalForceValue);
            }
            return internalForceFunction;
        }

        public double GetBeamVolume(int beamID)
        {
            double area = Property.GetBeamSectionPropertyValues(beamID).Ax;
            double length = Geometry.GetBeamLength(beamID);
            return area * length;
        }

        public void Dispose()
        {
            OpenSTAAD = null;
        }
    }
}
