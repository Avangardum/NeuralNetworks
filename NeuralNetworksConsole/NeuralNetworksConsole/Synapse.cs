using System;

namespace NeuralNetworksConsole
{
    class Synapse
    {
        private const double RANDOM_MULTIPLIER = 1;

        private static Random _random = new Random();

        public int Id { get; }
        public Neuron NeuronBehind { get; }
        public Neuron NeuronAhead { get; }
        public double Weight { get; set; }

        private Synapse(int id, Neuron neuronBehind, Neuron neuronAhead, double weight)
        {
            Id = id;
            NeuronBehind = neuronBehind;
            NeuronAhead = neuronAhead;
            Weight = weight;
        }
        public Synapse(Neuron neuronBehind, Neuron neuronAhead, double weight) : this(IdGenerator.GetId(), neuronBehind, neuronAhead, weight)
        {

        }
        public Synapse(Neuron neuronBehind, Neuron neuronAhead) : this(neuronBehind, neuronAhead, _random.NextDouble() * RANDOM_MULTIPLIER)
        {

        }
    }
}
