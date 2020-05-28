using static System.Math;

namespace NeuralNetworksConsole
{
    static class ActivationFunctions
    {
        public static double Linear(double x) => x;
        public static double Sigmoid(double x) => 1 / (1 + Pow(E, -x));
    }
}
