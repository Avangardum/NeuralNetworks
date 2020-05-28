using System;

namespace NeuralNetworksConsole
{
    class ActivationTestSubroutine : ISubroutine
    {
        private readonly double[] _testXArray = { -100, -9, -2, -0.7, -0.2, 0, 0.2, 0.7, 2, 9, 100 };

        public void Run()
        {
            Console.WriteLine("Тест функций активации");
            Console.WriteLine("Тест сигмоида");
            TestFunction(ActivationFunctions.Sigmoid);
        }

        private void TestFunction(Func<double, double> function)
        {
            foreach (var x in _testXArray)
            {
                Console.WriteLine($"f({x} = {function(x)})");
            }
        }
    }
}
