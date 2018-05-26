using System.Collections.Generic;
using WordDistance.Implementations;

namespace WordDistance.Interfaces
{
    public interface IShortestPathFinder
    {
        ICollection<string> FindShortestPath(GraphNode startWordGraphNode, GraphNode endWordGraphNode);
    }
}