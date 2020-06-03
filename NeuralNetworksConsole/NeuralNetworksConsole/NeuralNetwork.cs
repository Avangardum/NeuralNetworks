using NeuralNetworksConsole.Neurons;
using System;
using System.IO;
using System.Xml.Serialization;

namespace NeuralNetworksConsole
{
    class NeuralNetwork
    {
        private const string NEURAL_NETWORKS_SAVE_PATH = "Data/Neural Networks";

        public Neuron[][] Layers { get; }

        public NeuralNetwork(Func<double, double> activationFunction ,int hiddenLayers, int inputNeurons, int outputNeurons, int hiddenNeuronsPerLayer, bool useBiasNeurons)
        {
            Layers = new Neuron[hiddenLayers + 2][];
            //Инициализация входного слоя
            Layers[0] = new Neuron[useBiasNeurons ? inputNeurons + 1 : inputNeurons];
            for (int j = 0; j < inputNeurons; j++)
            {
                Layers[0][j] = new InputNeuron();
            }
            if (useBiasNeurons)
                Layers[0][inputNeurons] = new BiasNeuron();
            //Инициализация скрытых слоёв
            for (int i = 1; i <= hiddenLayers; i++)
            {
                Layers[i] = new Neuron[useBiasNeurons ? hiddenNeuronsPerLayer + 1 : hiddenNeuronsPerLayer];
                for (int j = 0; j < hiddenNeuronsPerLayer; j++)
                {
                    Layers[i][j] = new HiddenOrOutputNeuron(activationFunction, Layers[i - 1]);
                }
                if (useBiasNeurons)
                    Layers[i][hiddenNeuronsPerLayer] = new BiasNeuron();
            }
            //Инициализация выходного слоя
            Layers[hiddenLayers + 1] = new Neuron[outputNeurons];
            for (int j = 0; j < outputNeurons; j++)
            {
                Layers[hiddenLayers + 1][j] = new HiddenOrOutputNeuron(activationFunction, Layers[hiddenLayers]);
            }
        }

        public double[] Run(params double[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                Layers[0][i].Input = input[i];
            }
            for (int i = 1; i < Layers.Length; i++)
            {
                foreach (Neuron neuron in Layers[i])
                {
                    (neuron as HiddenOrOutputNeuron)?.CalculateInput();
                }
            }
            var outputLayer = Layers[Layers.Length - 1];
            double[] result = new double[outputLayer.Length];
            for (int i = 0; i < outputLayer.Length; i++)
            {
                result[i] = outputLayer[i].Output;
            }
            return result;
        }
    }
}
