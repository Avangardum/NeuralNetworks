using System.Collections.Generic;

namespace NeuralNetworksConsole
{
    static class SubroutineDictionary
    {
        private static Dictionary<string, ISubroutine> _dictionary;
        private static ISubroutine _doNotingSubroutine;

        public static string SubroutineList
        {
            get
            {
                string result = string.Empty;
                foreach (var key in _dictionary.Keys)
                {
                    result += key;
                    result += "\n";
                }
                return result;
            }
        }

        static SubroutineDictionary()
        {
            _doNotingSubroutine = new DoNothingSubroutine();
            _dictionary = new Dictionary<string, ISubroutine>();
            _dictionary.Add("activation_test", new ActivationTestSubroutine());
            _dictionary.Add("test", new TestSubroutine());
            _dictionary.Add("xor", new XORSubroutine());
        }

        public static ISubroutine GetSubroutine(string key)
        {
            if (!_dictionary.ContainsKey(key))
                return _doNotingSubroutine;
            return _dictionary[key];
        }
    }
}
