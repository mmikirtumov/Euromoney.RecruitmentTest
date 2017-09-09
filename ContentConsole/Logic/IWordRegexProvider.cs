namespace ContentConsole.Logic
{
    public interface IWordRegexProvider
    {
        string GetWordMatchingRegex();

        string GetBadWordDecoratingRegex();
    }
}
