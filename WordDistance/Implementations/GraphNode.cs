using System.Collections.Generic;
using WordDistance.Interfaces;

namespace WordDistance.Implementations
{
    public class GraphNode : IGraphNode
    {
        public GraphNode()
        {
            ReferencedNodes = new List<GraphNode>();
        }

        public string Word { get; set; }

        public GraphNode PreviousNode { get; set; }

        public ICollection<GraphNode> ReferencedNodes { get; }
    }
}