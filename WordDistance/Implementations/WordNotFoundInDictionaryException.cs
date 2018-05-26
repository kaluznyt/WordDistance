using System;

namespace WordDistance.Implementations
{
    public class WordNotFoundInDictionaryException : Exception
    {
        public WordNotFoundInDictionaryException(string word) : base($"Word {word} cannot be found in provided dictionary")
        {

        }
    }
}