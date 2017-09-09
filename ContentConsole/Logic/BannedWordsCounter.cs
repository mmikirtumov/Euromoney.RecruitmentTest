using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ContentConsole.Logic
{
    public class BannedWordsCounter : IBannedWordsCounter
    {
        private readonly IBanWordsReader _banWordsReader;
        private readonly IWordRegexProvider _wordRegexProvider;

        public BannedWordsCounter(IBanWordsReader banWordsReader, IWordRegexProvider wordRegexProvider)
        {
            _banWordsReader = banWordsReader;
            _wordRegexProvider = wordRegexProvider;
        }

        public int CountOfBannedWords(string content)
        {
            var bannedWords = _banWordsReader.GetBannedList();
            bannedWords = bannedWords.Select(bannedWord => bannedWord.ToLower()).ToList();

            return bannedWords.Sum(r => new Regex(String.Format(_wordRegexProvider.GetWordMatchingRegex(), r)).Matches(content).Count);
        }
    }
}
