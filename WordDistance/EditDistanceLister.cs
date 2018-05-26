using System.Collections.Generic;
using System.Linq;

namespace WordDistance
{
    public class ShortestEditPathLister : IWordPathLister
    {
        private readonly ICollection<string> _wordDictionary;

        public ShortestEditPathLister(ICollection<string> wordDictionary)
        {
            _wordDictionary = wordDictionary;
        }

        public string[] ListPath(string startWord, string endWord)
        {
            var wordLength = startWord.Length;

            var sameLengthWords = _wordDictionary.Where(word => IsOfLength(word, wordLength)).Distinct();

            var wordDistanceCalculator = new HammingWordDistanceCalculator();

            var wordGraphNodeMapping = new Dictionary<string, GraphNode>();

            var checkedWords = sameLengthWords.ToList();

            foreach (var checkedWord in checkedWords)
            {
                var currentWordNode = GetOrCreateGraphNode(wordGraphNodeMapping, checkedWord);

                foreach (var otherWord in checkedWords)
                {
                    if (checkedWord == otherWord)
                    {
                        continue;
                    }

                    var distance = wordDistanceCalculator.Calculate(checkedWord, otherWord);

                    if (distance == 1)
                    {
                        var otherWordGraphNode = GetOrCreateGraphNode(wordGraphNodeMapping, otherWord);
                        currentWordNode.ReferencedNodes.Add(otherWordGraphNode);
                    }
                }
            }

            var startWordGraphNode = wordGraphNodeMapping[startWord];
            var endWordGraphNode = wordGraphNodeMapping[endWord];

            var path = FindShortestPath(startWordGraphNode, endWordGraphNode);

            return path.ToArray();
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

            var processNode = endWordGraphNode;

            while (processNode != null)
            {
                result.Add(processNode.Word);

                if (processNode == startWordGraphNode)
                {
                    break;
                }

                processNode = processNode.PreviousNode;
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