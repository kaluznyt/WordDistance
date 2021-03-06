﻿using System;
using System.Linq;
using WordDistance.Interfaces;

namespace WordDistance.Implementations
{
    public class HammingWordDistanceCalculator : IWordDistanceCalculator
    {
        public int Calculate(string startWord, string endWord)
        {
            if (string.IsNullOrEmpty(startWord) || string.IsNullOrEmpty(endWord))
            {
                throw new ArgumentException($"Parameters {nameof(startWord)},{nameof(endWord)} should not be empty");
            }

            if (startWord.Length != endWord.Length)
            {
                throw new ArgumentException($"{nameof(startWord)} and {nameof(endWord)} should have the same length");
            }

            return startWord.Select((character, characterIndex) => character != endWord[characterIndex] ? 1 : 0).Sum();
        }
    }
}


