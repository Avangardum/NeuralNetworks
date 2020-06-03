using System;

namespace NeuralNetworksConsole
{
    static class DerivativeCalculator
    {
        private const double H = 0.001;

        public static double Derivative(this Func<double, double> f, double x)
        {
            return (f(x + H) - f(x - H)) / 2 * H;
        }
    }
}
