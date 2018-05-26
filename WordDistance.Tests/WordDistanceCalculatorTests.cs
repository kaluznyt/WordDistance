using System;
using Xunit;

namespace WordDistance.Tests
{
    public class WordDistanceCalculatorTests
    {
        [Theory]
        [InlineData("spin", "spot", 2)]
        [InlineData("spina", "spotd", 3)]
        [InlineData("abce", "bcde", 3)]
        [InlineData("tomasz", "tomasx", 1)]
        [InlineData("tombsz", "tomasx", 2)]
        [InlineData("aombsz", "tomasx", 3)]
        public void Calculate_Word_Distance_Returns_Shortest_Path(string startWord, string endWord, int expectedDistance)
        {
            var wordDistanceCalculator = new WordDistanceCalculator();

            var calculatedDistance = wordDistanceCalculator.Calculate(startWord, endWord);

            Assert.Equal(expectedDistance, calculatedDistance);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("", null)]
        [InlineData(null, null)]
        [InlineData(null, "")]
        [InlineData(null, "word")]
        [InlineData("", "word")]
        [InlineData("word", "")]
        public void Calculate_Empty_Words_Throw_ArgumentException(string startWord, string endWord)
        {
            var wordDistanceCalculator = new WordDistanceCalculator();

            Assert.Throws<ArgumentException>(() => wordDistanceCalculator.Calculate(startWord, endWord));
        }

        [Theory]
        [InlineData("abc", "abcd")]
        [InlineData("word", "wordd")]
        public void Calculate_Different_Length_Words_Throw_ArgumentException(string startWord, string endWord)
        {
            var wordDistanceCalculator = new WordDistanceCalculator();

            Assert.Throws<ArgumentException>(() => wordDistanceCalculator.Calculate(startWord, endWord));
        }
    }
}