using System;

namespace WordDistance
{
    public class ImpossibleToFindPathException : Exception
    {
        public ImpossibleToFindPathException(string message) : base(message)
        {

        }
    }
}