using System;

namespace NeuralNetworksConsole
{
    class TestSubroutine : ISubroutine
    {
        public void Run()
        {
            Random random = new Random(736);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(random.NextDouble());
            }
        }
    }
}
