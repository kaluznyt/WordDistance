using System.Collections.Generic;
using System.Linq;

namespace WordDistance
{
    public class ShortestEditPathLister : IWordPathLister
    {
        private readonly ICollection<string> _wordDictionary;
        private readonly IWordDistanceCalculator _wordDistanceCalculator;

        public ShortestEditPathLister(ICollection<string> wordDictionary, IWordDistanceCalculator wordDistanceCalculator)
        {
            _wordDictionary = wordDictionary;
            _wordDistanceCalculator = wordDistanceCalculator;
            _wordDistanceCalculator = new HammingWordDistanceCalculator();
        }

        public string[] ListPath(string startWord, string endWord)
        {
            var wordLength = startWord.Length;

            var wordsOfEqualLength = _wordDictionary.Where(word => IsOfLength(word, wordLength)).Distinct().ToList();

            var wordGraphNodeMapping = new Dictionary<string, GraphNode>();

            foreach (var checkedWord in wordsOfEqualLength)
            {
                var currentWordNode = GetOrCreateGraphNode(wordGraphNodeMapping, checkedWord);

                foreach (var otherWord in wordsOfEqualLength)
                {
                    if (checkedWord == otherWord)
                    {
                        continue;
                    }

                    IsSmallestPossibleDistance(checkedWord, otherWord, wordGraphNodeMapping, currentWordNode);
                }
            }

            var startWordGraphNode = wordGraphNodeMapping[startWord];
            var endWordGraphNode = wordGraphNodeMapping[endWord];

            var path = FindShortestPath(startWordGraphNode, endWordGraphNode);

            return path.ToArray();
        }

        private void IsSmallestPossibleDistance(string checkedWord, string otherWord, Dictionary<string, GraphNode> wordGraphNodeMapping,
            GraphNode currentWordNode)
        {
            var distance = _wordDistanceCalculator.Calculate(checkedWord, otherWord);

            if (distance == 1)
            {
                var otherWordGraphNode = GetOrCreateGraphNode(wordGraphNodeMapping, otherWord);
                currentWordNode.ReferencedNodes.Add(otherWordGraphNode);
            }
        }

        private ICollection<string> FindShortestPath(GraphNode startWordGraphNode, GraphNode endWordGraphNode)
        {
            var result = new List<string>();

            var queue = new Queue<GraphNode>();

            queue.Enqueue(startWordGraphNode);

            var shouldBreak = false;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                foreach (var nodeReferencedNode in node.ReferencedNodes)
                {
                    if (nodeReferencedNode.PreviousNode == null)
                        nodeReferencedNode.PreviousNode = node;

                    if (nodeReferencedNode == endWordGraphNode)
                    {
                        shouldBreak = true;
                    }

                    queue.Enqueue(nodeReferencedNode);
                }

                if (shouldBreak)
                {
                    break;
                }
            }

            var foundNode = endWordGraphNode;

            if (foundNode.PreviousNode == null)
            {
                throw new ImpossibleToFindPathException("It is not possible to find a solution with provided input");
            }

            while (foundNode != null)
            {
                result.Add(foundNode.Word);

                if (foundNode == startWordGraphNode)
                {
                    break;
                }

                foundNode = foundNode.PreviousNode;
            }

            result.Reverse();

            return result.ToArray();
        }

        private GraphNode GetOrCreateGraphNode(Dictionary<string, GraphNode> wordGraphNodeMapping, string checkedWord)
        {
            GraphNode currentWordNode;

            if (wordGraphNodeMapping.ContainsKey(checkedWord))
            {
                currentWordNode = wordGraphNodeMapping[checkedWord];
            }
            else
            {
                currentWordNode = wordGraphNodeMapping[checkedWord] = new GraphNode() { Word = checkedWord };
            }

            return currentWordNode;
        }

        private static bool IsOfLength(string word, int wordLength)
        {
            return word.Length == wordLength;
        }
    }
}