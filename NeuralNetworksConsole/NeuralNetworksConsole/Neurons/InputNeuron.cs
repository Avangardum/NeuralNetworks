using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworksConsole
{
    class InputNeuron : Neuron
    {
        public InputNeuron() : base(ActivationFunctions.Linear)
        {
            
        }
    }
}
