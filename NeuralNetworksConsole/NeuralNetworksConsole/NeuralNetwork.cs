using NeuralNetworksConsole.Neurons;
using System;

namespace NeuralNetworksConsole
{
    class NeuralNetwork
    {
        private Neuron[][] _layers;

        public NeuralNetwork(Func<double, double> activationFunction ,int hiddenLayers, int inputNeurons, int outputNeurons, int hiddenNeuronsPerLayer, bool useBiasNeurons)
        {
            _layers = new Neuron[hiddenLayers + 2][];
            //Инициализация входного слоя
            _layers[0] = new Neuron[useBiasNeurons ? inputNeurons + 1 : inputNeurons];
            for (int j = 0; j < inputNeurons; j++)
            {
                _layers[0][j] = new InputNeuron();
            }
            if (useBiasNeurons)
                _layers[0][inputNeurons] = new BiasNeuron();
            //Инициализация скрытых слоёв
            for (int i = 1; i <= hiddenLayers; i++)
            {
                _layers[i] = new Neuron[useBiasNeurons ? hiddenNeuronsPerLayer + 1 : hiddenNeuronsPerLayer];
                for (int j = 0; j < hiddenNeuronsPerLayer; j++)
                {
                    _layers[i][j] = new HiddenOrOutputNeuron(activationFunction, _layers[i - 1]);
                }
                if (useBiasNeurons)
                    _layers[i][hiddenNeuronsPerLayer] = new BiasNeuron();
            }
            //Инициализация выходного слоя
            _layers[hiddenLayers + 1] = new Neuron[outputNeurons];
            for (int j = 0; j < outputNeurons; j++)
            {
                _layers[hiddenLayers + 1][j] = new HiddenOrOutputNeuron(activationFunction, _layers[hiddenLayers]);
            }
        }

        public double[] Run(params double[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                _layers[0][i].Input = input[i];
            }
            for (int i = 1; i < _layers.Length; i++)
            {
                foreach (Neuron neuron in _layers[i])
                {
                    (neuron as HiddenOrOutputNeuron)?.CalculateInput();
                }
            }
            var outputLayer = _layers[_layers.Length - 1];
            double[] result = new double[outputLayer.Length];
            for (int i = 0; i < outputLayer.Length; i++)
            {
                result[i] = outputLayer[i].Output;
            }
            return result;
        }
    }
}
