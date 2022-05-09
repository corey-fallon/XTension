using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSTAADWrapper
{
    public class FunctionIntegrator
    {
        public static double DefiniteIntegralOfProduct(DiscreteUnivariateFunction firstFunction, DiscreteUnivariateFunction secondFunction)
        {
            double value = 0, xi, xj, Yi, Yj, yi, yj;
            for (int i = 0; i < firstFunction.NumberOfValues - 1; i++)
            {
                xi = firstFunction.xValues[i];
                xj = firstFunction.xValues[i + 1];
                Yi = firstFunction.yValues[i];
                Yj = firstFunction.yValues[i + 1];
                yi = secondFunction.yValues[i];
                yj = secondFunction.yValues[i + 1];
                value += (xj - xi) / 6 * (yi * (2 * Yi + Yj) + yj * (Yi + 2 * Yj));
            }
            return value;
        }

}
}
