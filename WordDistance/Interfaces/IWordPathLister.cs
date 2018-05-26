namespace WordDistance
{
    public interface IWordPathLister
    {
        string[] ListPath(string startWord, string endWord, string[] dictionary);
    }
}