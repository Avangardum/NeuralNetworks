using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworksConsole.Neurons
{
    class HiddenOrOutputNeuron : Neuron
    {
        private Synapse[] _synapsesBehind;

        public HiddenOrOutputNeuron(Func<double, double> activationFunction, Neuron[] neuronsBehind) : base(activationFunction)
        {
            _synapsesBehind = new Synapse[neuronsBehind.Length];
            for (int i = 0; i < neuronsBehind.Length; i++)
            {
                _synapsesBehind[i] = new Synapse(neuronsBehind[i], this);
            }
        }

        public void CalculateInput()
        {
            double input = 0;
            foreach (var synapse in _synapsesBehind)
                input += synapse.NeuronBehind.Output * synapse.Weight;
            Input = input;
        }
    }
}
