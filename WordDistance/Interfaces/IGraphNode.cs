using System.Collections.Generic;
using WordDistance.Implementations;

namespace WordDistance.Interfaces
{
    public interface IGraphNode
    {
        string Word { get; set; }

        GraphNode PreviousNode { get; set; }

        ICollection<GraphNode> ReferencedNodes { get; }
    }
}