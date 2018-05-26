using System.Collections.Generic;
using System.Linq;
using WordDistance.Exceptions;
using WordDistance.Interfaces;

namespace WordDistance.Implementations
{
    public class ShortestPathFinder : IShortestPathFinder
    {
        public ICollection<string> FindShortestPath(GraphNode startWordGraphNode, GraphNode endWordGraphNode)
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

            var result = RetrieveResultWordsPath(startWordGraphNode, endWordGraphNode);

            return result.Reverse().ToArray();
        }

        private ICollection<string> RetrieveResultWordsPath(GraphNode startWordGraphNode, GraphNode foundNode)
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

    }
}