namespace WordDistance.Interfaces
{
    public interface IWordPathLister
    {
        string[] ListPath(string startWord, string endWord, string[] dictionary);
    }
}