using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenStaadSharp.DataObjects;
using OpenStaadSharp.Enumerations;

namespace OpenStaadSharp
{
    public class OpenStaadProperty
    {
        private dynamic OSPropertyUI;

        internal OpenStaadProperty(dynamic property)
        {
            OSPropertyUI = property;
        }

        public void AddControlDependentRelation(int controlNode, int rigidXYYZZX, int fX, int fY, int fZ, int mX, int mY, int mZ, int[] dependentNodeArray)
        {
            dynamic retval = OSPropertyUI.AddControlDependentRelation(controlNode, rigidXYYZZX, fX, fY, fZ, mX, mY, mZ, dependentNodeArray);
        }

        public void AddControlDependentRelation(int controlNode, int[] dependentNodes, ControlDependentRelationPreset preset)
        {
            AddControlDependentRelation(controlNode, (int)preset, 0, 0, 0, 0, 0, 0, dependentNodes);
        }

        public void DeleteAllControlDependentRelations()
        {
            dynamic retval = OSPropertyUI.DeleteAllControlDependentRelations();
        }

        public string GetBeamMaterialName(int beamID)
        {
            return OSPropertyUI.GetBeamMaterialName(beamID);
        }

        public MaterialConstants GetMaterialProperty(string materialName)
        {
            dynamic modulusOfElasticity = new double();
            dynamic poissonsRatio = new double();
            dynamic density = new double();
            dynamic coefficientOfThermalExpansion = new double();
            dynamic dampingRatio = new double();
            dynamic retval = OSPropertyUI.GetMaterialProperty(materialName, ref modulusOfElasticity, ref poissonsRatio, ref density, ref coefficientOfThermalExpansion, ref dampingRatio);
            return new MaterialConstants()
            {
                ModulusOfElasticity = modulusOfElasticity,
                PoissonsRatio = poissonsRatio,
                Density = density,
                CoefficientOfThermalExpansion = coefficientOfThermalExpansion,
                DampingRatio = dampingRatio
            };
        }

        public string GetBeamSectionName(int beamID)
        {
            return OSPropertyUI.GetBeamSectionName(beamID);
        }

        public int GetBeamSectionPropertyRefNo(int beamID)
        {
            return OSPropertyUI.GetBeamSectionPropertyRefNo(beamID);
        }

        public SectionProperties GetSectionPropertyValues(int profileReferenceNumber)
        {
            dynamic width = new double();
            dynamic depth = new double();
            dynamic ax = new double();
            dynamic ay = new double();
            dynamic az = new double();
            dynamic ix = new double();
            dynamic iy = new double();
            dynamic iz = new double();
            dynamic tf = new double();
            dynamic tw = new double();
            dynamic retval = OSPropertyUI.GetSectionPropertyValues(profileReferenceNumber, ref width, ref depth, ref ax, ref ay, ref az, ref ix, ref iy, ref iz, ref tf, ref tw);
            return new SectionProperties()
            {
                Width = width,
                Depth = depth,
                Ax = ax,
                Ay = ay,
                Az = az,
                Ix = ix,
                Iy = iy,
                Iz = iz,
                Tf = tf,
                Tw = tw
            };
        }
    }
}
