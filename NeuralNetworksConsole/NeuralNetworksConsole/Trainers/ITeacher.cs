namespace NeuralNetworksConsole
{
    interface ITeacher
    {
        void Teach(NeuralNetwork neuralNetwork, double[] output, double[] perfectOutput, double trainingSpeed, double momentum);
    }
}
