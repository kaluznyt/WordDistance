﻿using Xunit;

namespace WordDistance.Tests
{
    public class WordDistanceListerTests
    {

        [Fact]
        public void ListShortestPath_For_Defined_Correct_Dictionary_Returns_Correct_ShortestPath()
        {
            const string startWord = "Spin";
            const string endWord = "Spot";

            var expectedResult = new string[]
            {
                "Spin",
                "Spit",
                "Spot"
            };

            var mockWordDictionary = new string[]
            {
                "Spat",
                "Spin",
                "Spit",
                "Apit",
                "abcd",
                "Span",
                "Spot"
            };

            var wordDistanceLister = new ShortestEditPathLister(new HammingWordDistanceCalculator());

            var results = wordDistanceLister.ListPath(startWord, endWord, mockWordDictionary);

            Assert.Equal(expectedResult, results);
        }

        [Fact]
        public void ListShortestPath_ForInvalidDictionary_ThrowsImpossibleToFindPathException()
        {
            const string startWord = "Spin";
            const string endWord = "Spot";

            var mockWordDictionary = new string[]
            {
                "Spin",
                "Joke",
                "Teal",
                "Spot"
            };

            var wordDistanceLister = new ShortestEditPathLister(new HammingWordDistanceCalculator());

            Assert.Throws<ImpossibleToFindPathException>(() => wordDistanceLister.ListPath(startWord, endWord, mockWordDictionary));
        }
    }
}
