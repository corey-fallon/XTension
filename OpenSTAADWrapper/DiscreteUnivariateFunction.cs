using System.Collections.Generic;

namespace OpenSTAADWrapper
{
    public class DiscreteUnivariateFunction
    {
        public List<double> xValues { get; }
        public List<double> yValues { get; }
        public int NumberOfValues { get => xValues.Count; }

        public DiscreteUnivariateFunction()
        {
            xValues = new List<double>();
            yValues = new List<double>();
        }

        public void Insert(double x, double y)
        {
            xValues.Add(x);
            yValues.Add(y);
        }
    }
}
