using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworksConsole
{
    class BiasNeuron : Neuron
    {
        public BiasNeuron() : base(ActivationFunctions.Linear)
        {
            Input = 1;
        }
    }
}
