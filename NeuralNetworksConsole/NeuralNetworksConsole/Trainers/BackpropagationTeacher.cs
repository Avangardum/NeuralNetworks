using System;
using System.Diagnostics;
using System.Linq;

namespace NeuralNetworksConsole
{
    class BackpropagationTeacher : ITeacher
    {
        public void Teach(NeuralNetwork neuralNetwork, double[] output, double[] idealOutput, double learningSpeed, double momentum)
        {
            var outputLayer = neuralNetwork.Layers[neuralNetwork.Layers.Length - 1];
            for (int i = 0; i < outputLayer.Length; i++)
            {
                outputLayer[i].Delta = CalculateOutputDelta(idealOutput[i], output[i], outputLayer[i].Input, outputLayer[i].ActivationFunction);
            }
            var hiddenLayers = new Neuron[neuralNetwork.Layers.Length - 2][];
            //Пробегаем по всем скрытым и входному слоям в обратном порядке
            for (int i = neuralNetwork.Layers.Length - 2; i >= 0; i--)
            {
                for (int j = 0; j < neuralNetwork.Layers[i].Length; j++)
                {
                    var neuron = neuralNetwork.Layers[i][j];
                    //Считаем дельту скрытого нейрона
                    neuron.Delta = CalculateHiddenDelta(neuron.Input, neuron.SynapsesAhead.Select(x => x.Weight).ToArray(),
                        neuron.SynapsesAhead.Select(x => x.NeuronAhead.Delta).ToArray(), neuron.ActivationFunction);
                    //Обновляем веса исходящих синапсов
                    foreach (var synapse in neuron.SynapsesAhead)
                    {
                        double weightChange = learningSpeed * Gradient(synapse) + momentum * synapse.PreviousWeightChange;
                        synapse.Weight += weightChange;
                        synapse.PreviousWeightChange = weightChange;
                        //Console.WriteLine($"    Вес синапса №{synapse.Id} изменён на {weightChange}");
                    }
                }
            }
        }

        private double CalculateOutputDelta(double outIdeal, double outActual, double input, Func<double, double> activationFunction)
        {
            return (outIdeal - outActual) * activationFunction.Derivative(input);
        }

        private double CalculateHiddenDelta(double input, double[] outputWeights, double[] nextNeuronsDeltas, Func<double, double> activationFunction)
        {
            double sum = 0;
            for (int i = 0; i < nextNeuronsDeltas.Length; i++)
            {
                sum += outputWeights[i] * nextNeuronsDeltas[i];
            }
            return activationFunction.Derivative(input) * sum;
        }

        private double Gradient(Synapse synapse)
        {
            return synapse.NeuronAhead.Delta * synapse.NeuronBehind.Output;
        }
    }
}
