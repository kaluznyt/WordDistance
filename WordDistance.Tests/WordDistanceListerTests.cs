using Xunit;

namespace WordDistance.Tests
{
    public class WordDistanceListerTests
    {

        [Fact]
        public void ListShortestPath_For_Defined_Correct_Dictionary_Returns_Correct_ShortestPath()
        {
            var startWord = "Spin";
            var endWord = "Spot";

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

            var wordDistanceLister = new ShortestEditPathLister(mockWordDictionary, new HammingWordDistanceCalculator());

            var results = wordDistanceLister.ListPath(startWord, endWord);

            Assert.Equal(expectedResult,results);
        }

        [Fact]
        public void ListShortestPath_ForInvalidDictionary_ThrowsImpossibleToFindPathException()
        {
            var startWord = "Spin";
            var endWord = "Spot";

            var mockWordDictionary = new string[]
            {
                "Spin",
                "Joke",
                "Teal",
                "Spot"
            };

            var wordDistanceLister = new ShortestEditPathLister(mockWordDictionary, new HammingWordDistanceCalculator());

            Assert.Throws<ImpossibleToFindPathException>(()=> wordDistanceLister.ListPath(startWord, endWord));
        }
    }
}
