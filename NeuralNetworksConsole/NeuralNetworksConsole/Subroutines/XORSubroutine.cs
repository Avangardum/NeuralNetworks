using System;

namespace NeuralNetworksConsole
{
    class XORSubroutine : ISubroutine
    {
        const int ERROR_SIGNS_AFTER_POINT = 2;
        const bool USE_STATIC_SEED = true;
        const int WEIGHT_RANDOM_SEED = 60;

        public void Run()
        {
            if(USE_STATIC_SEED)
                Synapse.SetWeightRandomSeed(WEIGHT_RANDOM_SEED);

#region Гиперпараметры
            double learningSpeed = 5000;
            double momentum = .5;
            int maxEpoch = 10000000;

            NeuralNetwork neuralNetwork = new NeuralNetwork(
                activationFunction: ActivationFunctions.Sigmoid,
                hiddenLayers: 1,
                inputNeurons: 2,
                outputNeurons: 1,
                hiddenNeuronsPerLayer: 2,
                useBiasNeurons: false
                );
#endregion

            Console.WriteLine("Тренировка нейросети");
            ITeacher teacher = new BackpropagationTeacher();
            int epoch = 0;
            double previousEpochError = 0;
            double initialError = 0;
            double finalError = 0;
            int errorIncreases = 0;
            int errorDecreases = 0;
            int errorStagnations = 0;
            while(epoch <= maxEpoch)
            {
                double[] answers = new double[4];
                double[] perfectAnswers = { 0, 1, 1, 0 };
                answers[0] = neuralNetwork.Run(0, 0)[0];
                teacher.Teach(neuralNetwork, new double[] { answers[0] }, new double[] { perfectAnswers[0] }, learningSpeed, momentum);
                answers[1] = neuralNetwork.Run(0, 1)[0];
                teacher.Teach(neuralNetwork, new double[] { answers[1] }, new double[] { perfectAnswers[1] }, learningSpeed, momentum);
                answers[2] = neuralNetwork.Run(1, 0)[0];
                teacher.Teach(neuralNetwork, new double[] { answers[2] }, new double[] { perfectAnswers[2] }, learningSpeed, momentum);
                answers[3] = neuralNetwork.Run(1, 1)[0];
                teacher.Teach(neuralNetwork, new double[] { answers[3] }, new double[] { perfectAnswers[3] }, learningSpeed, momentum);

                double error = ErrorCalcualtionFunctions.MSE(answers, perfectAnswers);
                //Console.WriteLine($"Эпоха {epoch} - ошибка {error.ToString("P" + ERROR_SIGNS_AFTER_POINT)}");
                if(epoch > 0)
                {
                    if (error > previousEpochError)
                        errorIncreases++;
                    else if (error < previousEpochError)
                        errorDecreases++;
                    else
                        errorStagnations++;
                }
                if (epoch == 0)
                    initialError = error;
                if (epoch == maxEpoch)
                    finalError = error;
                previousEpochError = error;
                epoch++;
            }
            Console.WriteLine($"Тренировка завершена. уменьшений ошибки: {errorDecreases}, увеличений ошибки: {errorIncreases}, стагнаций ошибки: {errorStagnations}");
            Console.WriteLine($"Начальная ошибка: {initialError.ToString("P" + ERROR_SIGNS_AFTER_POINT)}, " +
                $"конечная ошибка: {finalError.ToString("P" + ERROR_SIGNS_AFTER_POINT)}");

            while (true)
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

                double output = neuralNetwork.Run(a, b)[0];
                Console.WriteLine($"a = {a}, b = {b}");
                Console.WriteLine($"Ответ: {Math.Round(output)}");
                Console.WriteLine(string.Empty);
            }
        }
    }
}
