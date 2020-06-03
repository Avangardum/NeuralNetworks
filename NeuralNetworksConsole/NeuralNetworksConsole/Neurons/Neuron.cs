using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworksConsole
{
    abstract class Neuron
    {
        public int Id { get; }
        public Func<double, double> ActivationFunction { get; protected set; }
        public List<Synapse> SynapsesAhead;

        public double Delta;

        private double _input;

        public double Input
        {
            get => _input;
            set
            {
                _input = value;
                Output = ActivationFunction(_input);
            }
        }

        public double Output { get; private set; }

        private Neuron(int id, Func<double, double> activationFunction)
        {
            Id = id;
            ActivationFunction = activationFunction;
            SynapsesAhead = new List<Synapse>();
        }

        protected Neuron(Func<double, double> activationFunction) : this(IdGenerator.GetId(), activationFunction)
        {

        }
    }
}
