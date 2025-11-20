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

        // Reproducable dungeon
        public static void Initialize(int seed)
        {
            _random = new Random(seed);
            _currentseed = seed;
        }

        public static void Reset()
        {
            _random = new Random();
            _currentseed = null;
        }

        public static int Next(int min, int max) // Returns a random integer between min and max
        {
            return _random.Next(min, max);
        }

        public static float NextFloat() // Returns a random float between 0.0 and 1.0
        {
            return (float)_random.NextDouble();
        }

        public static bool NextBool() // Returns a random boolean (true or false)
        {
            return _random.Next(2) == 0;
        }

        public static T PickRandom<T>(T[] array) // Picks a random element from an array
        // Generic method works with any type: string[], Enemy[], Tile[], etc.
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException("Array cannot be null or empty");
            }

            return array[_random.Next(array.Length)];
        }

        public static T PickRandom<T>(List<T> list) // Picks a random element from a List
        {
            if (list == null || list.Count == 0)
                throw new ArgumentException("List cannot be null or empty");

            return list[_random.Next(list.Count)];
        }

        public static int? CurrentSeed => _currentseed;
    }
}