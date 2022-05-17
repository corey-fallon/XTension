using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSTAADWrapper;

namespace OpenSTAADWrapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (OpenStaad os = new OpenStaad())
            {
                var diaphragmNodes = new int[] { 40, 32, 17 };
                CenterOfRigidityAnalysis analysis = new CenterOfRigidityAnalysis(os, diaphragmNodes);
                analysis.Start();
                var results = analysis.GetResults();
            }
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
