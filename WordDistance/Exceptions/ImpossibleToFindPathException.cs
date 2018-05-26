using System;

namespace WordDistance.Exceptions
{
    public class ImpossibleToFindPathException : Exception
    {
        public ImpossibleToFindPathException(string message) : base(message)
        {

        }
    }
}