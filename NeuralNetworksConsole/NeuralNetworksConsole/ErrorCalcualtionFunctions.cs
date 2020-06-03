using static System.Math;

namespace NeuralNetworksConsole
{
    static class ErrorCalcualtionFunctions
    {
        public static double MSE(double[] answers, double[] perfectAnswers)
        {
            double numerator = 0;
            for (int i = 0; i < answers.Length; i++)
                numerator += Pow(perfectAnswers[i] - answers[i], 2);
            double denominator = answers.Length;
            return numerator / denominator;
        }
    }
}
