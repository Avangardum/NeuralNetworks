using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworksConsole
{
    static class IdGenerator
    {
        private static int _lastId = 0;

        public static int GetId()
        {
            _lastId++;
            return _lastId;
        }
    }
}
