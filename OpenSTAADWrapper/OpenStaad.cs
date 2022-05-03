using System;
using System.Collections.Generic;
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

        public OpenStaad()
        {
            OpenSTAAD = Marshal.GetActiveObject("StaadPro.OpenSTAAD");
        }

        public void Dispose()
        {
            OpenSTAAD = null;
        }
    }
}
