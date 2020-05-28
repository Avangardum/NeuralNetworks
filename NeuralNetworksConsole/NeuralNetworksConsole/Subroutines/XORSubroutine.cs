using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworksConsole.Subroutines
{
    class XORSubroutine : ISubroutine
    {
        public void Run()
        {
            Console.WriteLine("Введите 2 числа(0 или 1), нейросеть вычислит значение a XOR b");
            byte a;
            byte b;
            while (true)
            {
                Console.Write("Введите a\n>");
                string aStr = Console.ReadLine();
                bool parseSuccessful = byte.TryParse(aStr, out a);
                if (parseSuccessful && (a == 0 || a == 1))
                    break;
            }
            while (true)
            {
                Console.Write("Введите b\n>");
                string bStr = Console.ReadLine();
                bool parseSuccessful = byte.TryParse(bStr, out b);
                if (parseSuccessful && (b == 0 || b == 1))
                    break;
            }

            NeuralNetwork neuralNetwork = new NeuralNetwork(
                activationFunction: ActivationFunctions.Sigmoid,
                hiddenLayers: 1,
                inputNeurons: 2,
                outputNeurons: 1,
                hiddenNeuronsPerLayer: 3,
                useBiasNeurons: false
                );
            double output = neuralNetwork.Run(a, b)[0];
            Console.WriteLine($"a = {a}, b = {b}");
            Console.WriteLine($"Ответ: {output}");
        }
    }
}
