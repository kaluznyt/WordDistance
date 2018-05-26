using System;
using System.Collections.Generic;
using Xunit;

namespace WordDistance.Tests
{
    public class WordDistanceListerTests
    {
        [Fact]
        public void ListShortestPath_ReturnsCorrectListOfWords()
        {
            var startWord = "Spin";
            var endWord = "Spot";

            var expectedResult = new string[]
            {
                "Spin",
                "Spit",
                "Spot"
            };

            var mockWordDictionary = new List<string>
            {
                "Spat",
                "Span",
                "Spin",
                "Spit",
                "Spot",
                
            };

            var wordDistanceLister = new ShortestEditPathLister(mockWordDictionary);

            var results = wordDistanceLister.ListPath(startWord, endWord);

            Assert.Equal(expectedResult,results);
            
        }
    }
}
