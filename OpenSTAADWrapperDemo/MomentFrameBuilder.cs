using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenStaadSharp;

namespace OpenSTAADWrapperDemo
{
    public class MomentFrameBuilder
    {
        private OpenStaad os;

        public MomentFrameBuilder(OpenStaad os)
        {
            this.os = os;
        }

        public void Build(MomentFrame mf)
        {
            BuildNodes(mf);
            BuildMembers(mf);
            IncludeSupport(mf);
        }

        private void BuildNodes(MomentFrame mf)
        {
            int numberOfNodes = (mf.NumberOfStories + 1) * (mf.NumberOfBays + 1);
            int[] nodeIDs = new int[numberOfNodes];
            double[,] coordinates = new double[numberOfNodes, 3];
            int currentNodeID = 1;
            for (int i = 0; i < mf.NumberOfBays + 1; i++)
            {
                for (int j = 0; j < mf.NumberOfStories + 1; j++)
                {
                    double xCoordinate = i * mf.BayLength;
                    double yCoordinate = j * mf.StoryHeight;
                    double zCoordinate = 0.0;
                    nodeIDs[currentNodeID - 1] = currentNodeID;
                    coordinates[currentNodeID - 1, 0] = xCoordinate;
                    coordinates[currentNodeID - 1, 1] = yCoordinate;
                    coordinates[currentNodeID - 1, 2] = zCoordinate;
                    currentNodeID++;
                }
            }
            os.Geometry.CreateMultipleNodes(nodeIDs, coordinates);
        }

        private void BuildMembers(MomentFrame mf)
        {
            BuildColumns(mf);
            BuildBeams(mf);
        }

        private void BuildColumns(MomentFrame mf)
        {
            for (int i = 0; i < mf.NumberOfBays + 1; i++)
            {
                int firstColumnStackStartNodeID = (i + 1) + i * mf.NumberOfStories;
                int secondColumnStackStartNodeID = (i + 1) + (i + 1) * mf.NumberOfStories;
                for (int j = 0; j < mf.NumberOfStories; j++)
                {
                    int startNode = firstColumnStackStartNodeID + j;
                    int endNode = startNode + 1;
                    os.Geometry.AddMember(startNode, endNode);
                }
            }
        }

        private void BuildBeams(MomentFrame mf)
        {
            for (int i = 0; i < mf.NumberOfBays; i++)
            {
                int firstColumnStackStartNodeID = (i + 1) + i * mf.NumberOfStories;
                int secondColumnStackStartNodeID = (i + 2) + (i + 1) * mf.NumberOfStories;
                for (int j = 0; j < mf.NumberOfStories; j++)
                {
                    int startNode = firstColumnStackStartNodeID + (j + 1);
                    int endNode = secondColumnStackStartNodeID + (j + 1);
                    os.Geometry.AddMember(startNode, endNode);
                }
            }
        }

        private void IncludeSupport(MomentFrame mf)
        {
            int supportID = os.Support.CreateSupportFixed();
            for (int i = 0; i < mf.NumberOfBays + 1; i++)
            {
                int supportNodeID = (i + 1) + i * mf.NumberOfStories;
                os.Support.AssignSupportToNode(supportNodeID, supportID);
            }
        }
    }
}
