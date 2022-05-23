using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using OpenStaadSharp.Enumerations;

namespace OpenStaadSharp
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

        public void NewSTAADFile(string fileName, int lenUnitInput, int forceUnitInput)
        {
            OpenSTAAD.NewSTAADFile(fileName, lenUnitInput, forceUnitInput);
        }

        public void NewSTAADFile(string fileName, LengthUnitInput lengthUnitInput, ForceUnitInput forceUnitInput)
        {
            NewSTAADFile(fileName, (int)lengthUnitInput, (int)forceUnitInput);
        }

        public void UpdateStructure()
        {
            OpenSTAAD.UpdateStructure();
        }

        public int SetSilentMode(int flag)
        {
            return OpenSTAAD.SetSilentMode(flag);
        }

        public int AnalyzeEx(int silent, int hidden, int wait)
        {
            return OpenSTAAD.AnalyzeEx(silent, hidden, wait);
        }

        public void Dispose()
        {
            OpenSTAAD = null;
        }
    }
}
