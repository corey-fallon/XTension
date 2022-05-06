using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSTAADWrapper
{
    public class OpenStaadPlate
    {
        private OpenStaad os;
        public int ID { get; private set; }

        internal OpenStaadPlate(OpenStaad os, int id)
        {
            this.os = os;
            this.ID = id;
        }


    }
}
