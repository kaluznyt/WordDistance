using System.Collections.Generic;
using System.Linq;
using WordDistance.Interfaces;

namespace WordDistance.Implementations
{
    public class GraphBuilder : IGraphBuilder
    {
        private readonly IWordDistanceCalculator _wordDistanceCalculator;
        private readonly IDictionary<string, GraphNode> _wordGraphNodeMapping = new Dictionary<string, GraphNode>();

        public GraphBuilder(IWordDistanceCalculator wordDistanceCalculator)
        {
            _wordDistanceCalculator = wordDistanceCalculator;
        }

        public GraphNode GetNodeByWord(string word)
        {
            if (!_wordGraphNodeMapping.ContainsKey(word))
            {
                throw new WordNotFoundInDictionaryException(word);
            }

            return _wordGraphNodeMapping[word];
        }

        public void BuildWordGraph(int wordLength, string[] dictionary)
        {
            var wordsOfEqualLength = ReturnAllWordsWithSameLengthAsInputWord(dictionary, wordLength);

            _wordGraphNodeMapping.Clear();

            foreach (var currentWord in wordsOfEqualLength)
            {
                var currentWordNode = GetOrCreateGraphNode(currentWord);

                foreach (var otherWord in wordsOfEqualLength)
                {
                    if (currentWord == otherWord)
                    {
                        continue;
                    }

                    BuildGraphReferencesJustForSmallestPossibleWordDistance(currentWord, otherWord, currentWordNode);
                }
            }
        }

        private List<string> ReturnAllWordsWithSameLengthAsInputWord(string[] dictionary, int wordLength)
        {
            return dictionary.Where(word => IsOfLength(word, wordLength)).Distinct().ToList();
        }
        private bool IsOfLength(string word, int wordLength)
        {
            return word.Length == wordLength;
        }

        private GraphNode GetOrCreateGraphNode(string checkedWord)
        {
            GraphNode currentWordNode;

            if (_wordGraphNodeMapping.ContainsKey(checkedWord))
            {
                currentWordNode = _wordGraphNodeMapping[checkedWord];
            }
            else
            {
                currentWordNode = _wordGraphNodeMapping[checkedWord] = new GraphNode() { Word = checkedWord };
            }

            return currentWordNode;
        }

        private void BuildGraphReferencesJustForSmallestPossibleWordDistance(
            string currentWord,
            string otherWord,
            GraphNode currentWordNode)
        {
            const int smallestDistanceBetweenWords = 1;

            var distance = _wordDistanceCalculator.Calculate(currentWord, otherWord);

            if (distance == smallestDistanceBetweenWords)
            {
                var otherWordGraphNode = GetOrCreateGraphNode(otherWord);

                currentWordNode.ReferencedNodes.Add(otherWordGraphNode);
            }
        }
    }
}