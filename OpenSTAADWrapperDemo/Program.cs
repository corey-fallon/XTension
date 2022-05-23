using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenStaadSharp;
using OpenStaadSharp.DataObjects;
using OpenStaadSharp.Enumerations;

namespace OpenSTAADWrapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (OpenStaad os = new OpenStaad())
            {
                os.Geometry.DeleteAllMembers();
                os.Geometry.DeleteAllNodes();

                MomentFrame mf = new MomentFrame()
                {
                    NumberOfBays = 1,
                    NumberOfStories = 4,
                    StoryHeight = 180,
                    BayLength = 360
                };

                var mfBuilder = new MomentFrameBuilder(os);
                mfBuilder.Build(mf);
                os.UpdateStructure();
            }
        }

        static void WaitForFileToBeCreated(string filePath)
        {
            foreach (var path in StaadFileDirectory.GetFilePathsCreatedOnCreateNewModel(filePath))
            {
                while (!File.Exists(path))
                {
                    Console.WriteLine($"Found {Path.GetExtension(path)} at {DateTime.Now}");
                    break;
                }
            }
        }

        static void CreateZXDiaphragm(OpenStaad os)
        {
            int[] dependentNodes = new int[] { 18, 19, 20, 37, 38, 39, 40, 57, 58, 59, 60, 77, 78, 79, 80 };
            os.Property.AddControlDependentRelation(17, dependentNodes, ControlDependentRelationPreset.ZX);
            os.UpdateStructure();
            os.Property.DeleteAllControlDependentRelations();
            os.UpdateStructure();
        }

        static void ThrowAway(OpenStaad os)
        {
            Coordinates targetCoordinates = new Coordinates() { XCoordinate = 1080, ZCoordinate = 0 };
            double[] upperDisplacement = GetDisplacementAtPoint(os, 38, targetCoordinates);
            double[] lowerDisplacement = GetDisplacementAtPoint(os, 73, targetCoordinates);

            double DeltaX = upperDisplacement[0] - lowerDisplacement[0];
            double DeltaZ = upperDisplacement[1] - lowerDisplacement[1];
            double Delta = Math.Sqrt(Math.Pow(DeltaX, 2) + Math.Pow(DeltaZ, 2));

            double h = 180;
            double ratio = h / Delta;
        }

        static double[] GetDisplacementAtPoint(OpenStaad os, int controlNodeID, Coordinates pointCoordinates)
        {
            Coordinates controlNodeCoordinates = os.Geometry.GetNodeCoordinates(controlNodeID);

            double Xo = controlNodeCoordinates.XCoordinate;
            double Zo = controlNodeCoordinates.ZCoordinate;
            double X = pointCoordinates.XCoordinate;
            double Z = pointCoordinates.ZCoordinate;

            double[] controlNodeDisplacement = os.Output.GetNodeDisplacements(controlNodeID, 1);
            double DeltaXo = controlNodeDisplacement[0];
            double DeltaZo = controlNodeDisplacement[2];
            double ThetaYo = controlNodeDisplacement[4];

            double DeltaX = DeltaXo + ThetaYo * (Z - Zo);
            double DeltaZ = DeltaZo - ThetaYo * (X - Xo);

            return new double[] { DeltaX, DeltaZ };
        }
    }

    public abstract class OpenStaadAnalysis
    {
        internal OpenStaad os;

        public OpenStaadAnalysis(OpenStaad os)
        {
            this.os = os;
        }

        public void Start()
        {
            os.SetSilentMode(1);
            BeforeAnalysis();
            os.AnalyzeEx(1, 1, 1);
            AfterAnalysis();
            os.UpdateStructure();
        }

        public abstract void BeforeAnalysis();
        public abstract void AfterAnalysis();
    }

    public class MultipleOpenStaadAnalysis : OpenStaadAnalysis
    {
        internal List<OpenStaadAnalysis> analyses = new List<OpenStaadAnalysis>();

        public MultipleOpenStaadAnalysis(OpenStaad os) : base(os)
        {
        }

        public void AddAnalysis(OpenStaadAnalysis analysis)
        {
            analyses.Add(analysis);
        }

        public override void BeforeAnalysis()
        {
            foreach (var analysis in analyses)
            {
                analysis.BeforeAnalysis();
            }
        }

        public override void AfterAnalysis()
        {
            foreach (var analysis in analyses)
            {
                analysis.AfterAnalysis();
            }
        }
    }

    public class CenterOfRigidityAnalysis : MultipleOpenStaadAnalysis
    {
        int[] diaphragmNodes;

        public CenterOfRigidityAnalysis(OpenStaad os, int[] diaphragmNodes) : base(os)
        {
            this.diaphragmNodes = diaphragmNodes;
            BuildAnalyses();
        }

        public void BuildAnalyses()
        {
            foreach (var node in diaphragmNodes)
            {
                SingleDiaphragmAnalysis analysis = new SingleDiaphragmAnalysis(os);
                analysis.SetNodeID(node);
                AddAnalysis(analysis);
            }
        }

        public List<SingleDiaphragmAnalysisResult> GetResults()
        {
            var results = new List<SingleDiaphragmAnalysisResult>();
            foreach (var analysis in analyses)
            {
                SingleDiaphragmAnalysis singleDiaphragmAnalysis = (SingleDiaphragmAnalysis)analysis;
                results.Add(singleDiaphragmAnalysis.GetResults());
            }
            return results;
        }
    }

    public class SingleDiaphragmAnalysisResult
    {
        public double XBar { get; set; }
        public double ZBar { get; set; }
    }

    public class SingleDiaphragmAnalysis : OpenStaadAnalysis
    {
        int nodeID;
        int fxLoadCaseID;
        int fzLoadCaseID;
        int myLoadCaseID;
        SingleDiaphragmAnalysisResult result;


        public SingleDiaphragmAnalysis(OpenStaad os) : base(os)
        {
        }

        public void SetNodeID(int nodeID)
        {
            this.nodeID = nodeID;
        }

        public SingleDiaphragmAnalysisResult GetResults()
        {
            return result;
        }

        public override void BeforeAnalysis()
        {
            fxLoadCaseID = os.Load.CreateNewPrimaryLoad("FX");
            fzLoadCaseID = os.Load.CreateNewPrimaryLoad("FZ");
            myLoadCaseID = os.Load.CreateNewPrimaryLoad("MY");

            os.Load.SetLoadActive(fxLoadCaseID);
            os.Load.AddNodalLoad(nodeID, 1000, 0, 0, 0, 0, 0);
            os.Load.SetLoadActive(fzLoadCaseID);
            os.Load.AddNodalLoad(nodeID, 0, 0, 1000, 0, 0, 0);
            os.Load.SetLoadActive(myLoadCaseID);
            os.Load.AddNodalLoad(nodeID, 0, 0, 0, 0, 1000, 0);
        }

        public override void AfterAnalysis()
        {
            double Ryx = os.Output.GetNodeDisplacements(nodeID, fxLoadCaseID)[4];
            double Ryz = os.Output.GetNodeDisplacements(nodeID, fzLoadCaseID)[4];
            double Ryy = os.Output.GetNodeDisplacements(nodeID, myLoadCaseID)[4];

            double xbar = Ryz / Ryy;
            double zbar = -Ryx / Ryy;

            Coordinates nodeCoordinates = os.Geometry.GetNodeCoordinates(nodeID);

            xbar = xbar + nodeCoordinates.XCoordinate;
            zbar = zbar + nodeCoordinates.ZCoordinate;

            os.Load.DeletePrimaryLoadCases(fxLoadCaseID, false);
            os.Load.DeletePrimaryLoadCases(fzLoadCaseID, false);
            os.Load.DeletePrimaryLoadCases(myLoadCaseID, false);

            result = new SingleDiaphragmAnalysisResult() { XBar = xbar, ZBar = zbar };
        }
    }
}
