// We make a service for randomness so it can be used throughout the whole game.

using System;
using System.Collections.Generic;

namespace Ludit.exe.Services
{
    public static class RNGService
    {
        private static Random _random;
        private static int? _currentseed;

        static RNGService()
        {
            _random = new Random();
            _currentseed = null;
        }
    }
}