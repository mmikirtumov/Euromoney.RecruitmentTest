namespace ContentConsole.Logic
{
    public class WordRegexProvider: IWordRegexProvider
    {
        private const string WordMatching = @"(?i)\b{0}\b|(?i)\b{0}[0-9]\b|(?i)\b[0-9]{0}\b|(?i)\b[0-9]{0}[0-9]\b";
        private const string BadWordDecorating = @"(?i)\b{0}\b";

        public string GetWordMatchingRegex()
        {
            return WordMatching;
        }

        public string GetBadWordDecoratingRegex()
        {
            return BadWordDecorating;
        }
    }
}
