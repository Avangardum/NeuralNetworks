using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworksConsole
{
    abstract class Neuron
    {
        public int Id { get; }

        protected Func<double, double> _activationFunction;

        private double _input;

        public double Input
        {
            get => _input;
            set
            {
                _input = value;
                Output = _activationFunction(_input);
            }
        }

        public double Output { get; private set; }

        private Neuron(int id, Func<double, double> activationFunction)
        {
            Id = id;
            _activationFunction = activationFunction;
        }

        protected Neuron(Func<double, double> activationFunction) : this(IdGenerator.GetId(), activationFunction)
        {

        }
    }
}
