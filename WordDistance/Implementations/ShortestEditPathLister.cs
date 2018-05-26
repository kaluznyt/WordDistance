using System.Collections.Generic;
using System.Linq;

namespace WordDistance
{
    public class ShortestEditPathLister : IWordPathLister
    {
        private readonly IWordDistanceCalculator _wordDistanceCalculator;
        private IDictionary<string, GraphNode> _wordGraphNodeMapping;

        public ShortestEditPathLister(IWordDistanceCalculator wordDistanceCalculator)
        {
            _wordDistanceCalculator = wordDistanceCalculator;
        }

        public string[] ListPath(string startWord, string endWord, string[] dictionary)
        {
            var wordLength = startWord.Length;

            var wordsOfEqualLength = ReturnAllWordsWithSameLengthAsInputWord(dictionary, wordLength);

            _wordGraphNodeMapping = new Dictionary<string, GraphNode>();

            foreach (var checkedWord in wordsOfEqualLength)
            {
                var currentWordNode = GetOrCreateGraphNode(checkedWord);

                foreach (var otherWord in wordsOfEqualLength)
                {
                    if (checkedWord == otherWord)
                    {
                        continue;
                    }

                    BuildGraphReferencesJustForSmallestPossibleWordDistance(checkedWord, otherWord, currentWordNode);
                }
            }

            var startWordGraphNode = _wordGraphNodeMapping[startWord];
            var endWordGraphNode = _wordGraphNodeMapping[endWord];

            var path = FindShortestPath(startWordGraphNode, endWordGraphNode);

            return path.ToArray();
        }

        private List<string> ReturnAllWordsWithSameLengthAsInputWord(string[] dictionary, int wordLength)
        {
            return dictionary.Where(word => IsOfLength(word, wordLength)).Distinct().ToList();
        }

        private void BuildGraphReferencesJustForSmallestPossibleWordDistance(
                                    string currentWord,
                                    string otherWord,
                                    GraphNode currentWordNode)
        {
            const int smallestDistance = 1;
            var distance = _wordDistanceCalculator.Calculate(currentWord, otherWord);

            if (distance == smallestDistance)
            {
                var otherWordGraphNode = GetOrCreateGraphNode(otherWord);

                currentWordNode.ReferencedNodes.Add(otherWordGraphNode);
            }
        }

        private ICollection<string> FindShortestPath(GraphNode startWordGraphNode, GraphNode endWordGraphNode)
        {
            var queue = new Queue<GraphNode>();

            queue.Enqueue(startWordGraphNode);

            var pathFound = false;

            while (QueueNotEmpty(queue))
            {
                var node = queue.Dequeue();

                foreach (var referencedNode in node.ReferencedNodes)
                {
                    SetPreviousNode(referencedNode, node);

                    if (referencedNode == endWordGraphNode)
                    {
                        pathFound = true;
                        break;
                    }

                    queue.Enqueue(referencedNode);
                }

                if (pathFound)
                {
                    break;
                }
            }

            if (!pathFound)
            {
                throw new ImpossibleToFindPathException("It is not possible to find a solution with provided input");
            }

            var result = RetrieveResultWordPath(startWordGraphNode, endWordGraphNode);

            return result.Reverse().ToArray();
        }

        private void SetPreviousNode(GraphNode referencedNode, GraphNode node)
        {
            if (referencedNode.PreviousNode == null)
            {
                referencedNode.PreviousNode = node;
            }
        }

        private bool QueueNotEmpty(Queue<GraphNode> queue)
        {
            return queue.Count > 0;
        }

        private ICollection<string> RetrieveResultWordPath(GraphNode startWordGraphNode, GraphNode foundNode)
        {
            var result = new List<string>();
            
            while (foundNode != null)
            {
                result.Add(foundNode.Word);

                if (foundNode == startWordGraphNode)
                {
                    break;
                }

                foundNode = foundNode.PreviousNode;
            }

            return result;
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

        private bool IsOfLength(string word, int wordLength)
        {
            return word.Length == wordLength;
        }
    }
}