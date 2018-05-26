using WordDistance.Implementations;

namespace WordDistance.Interfaces
{
    public interface IGraphBuilder
    {
        GraphNode GetNodeByWord(string word);
        void BuildWordGraph(int wordLength, string[] dictionary);
    }
}