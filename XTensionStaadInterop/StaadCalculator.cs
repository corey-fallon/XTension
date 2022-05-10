using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenSTAADWrapper;

namespace XTensionStaadInterop
{
    public class StaadCalculator
    {
        //public static List<double> MemberDisplayValues(MemberDisplayValue strategy)
        //{
        //    List<double> values = new List<double>();
        //    using (OpenStaad os = new OpenStaad())
        //    {
        //        foreach (var member in os.GetMembers())
        //        {
        //            values.Add(strategy.GetValue(member));
        //        }
        //    }
        //    return values;
        //}

        public static List<double> NormalizeList(List<double> list)
        {
            List<double> normalizedList = new List<double>();
            double min = list.Min();
            double max = list.Max();
            foreach (double item in list)
            {
                normalizedList.Add((item - min) / (max - min));
            }
            return normalizedList;
        }
    }
}
