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
                var supportNodes = os.GetSupportNodes();

                foreach (var node in supportNodes)
                {
                    Console.WriteLine(node.GetSupportReactions(1));
                }
            }
        }
    }
}
