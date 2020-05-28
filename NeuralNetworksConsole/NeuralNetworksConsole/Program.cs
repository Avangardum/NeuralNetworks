using System;

namespace NeuralNetworksConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Choose subroutine:");
                Console.Write(SubroutineDictionary.SubroutineList);
                Console.Write(">");
                string input = Console.ReadLine();
                Console.WriteLine();
                SubroutineDictionary.GetSubroutine(input.ToLower()).Run();
                Console.WriteLine();
            }
        }
    }
}
