namespace WordDistance.Interfaces
{
    public interface IWordDistanceCalculator
    {
        int Calculate(string startWord, string endWord);
    }
}