using System.Collections.Generic;

namespace WordDistance
{
    public class GraphNode
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