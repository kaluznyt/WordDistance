using System.Collections.Generic;

namespace WordDistance
{
    public interface IGraphNode
    {
        string Word { get; set; }

        GraphNode PreviousNode { get; set; }

        ICollection<GraphNode> ReferencedNodes { get; }
    }
}