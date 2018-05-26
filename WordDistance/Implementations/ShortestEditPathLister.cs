using System.Linq;
using WordDistance.Interfaces;

namespace WordDistance.Implementations
{
    public class ShortestEditPathLister : IWordPathLister
    {
        private readonly IShortestPathFinder _shortestPathFinder;
        private readonly IGraphBuilder _graphBuilder;

        public ShortestEditPathLister(
            IShortestPathFinder shortestPathFinder, 
            IGraphBuilder graphBuilder)
        {
            _shortestPathFinder = shortestPathFinder;
            _graphBuilder = graphBuilder;
        }

        public string[] ListPath(string startWord, string endWord, string[] dictionary)
        {
            var inputWordLength = startWord.Length;

            _graphBuilder.BuildWordGraph(inputWordLength, dictionary);

            var startWordGraphNode = _graphBuilder.GetNodeByWord(startWord);
            var endWordGraphNode = _graphBuilder.GetNodeByWord(endWord);

            var path = _shortestPathFinder.FindShortestPath(startWordGraphNode, endWordGraphNode);

            return path.ToArray();
        }
    }
}