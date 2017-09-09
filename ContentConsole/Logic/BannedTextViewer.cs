using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ContentConsole.Logic
{
    public class BannedTextViewer: IBannedTextViewer
    {
        private readonly bool _enableFilter;
        private readonly IBanWordsReader _banWordsReader;
        private readonly IWordRegexProvider _wordRegexProvider;

        public BannedTextViewer(IBanWordsReader banWordsReader, IWordRegexProvider wordRegexProvider, bool enableFilter)
        {
            _banWordsReader = banWordsReader;
            _wordRegexProvider = wordRegexProvider;
            _enableFilter = enableFilter;
        }

        public string BannedTextFilter(string content)
        {
            if (!_enableFilter)
            {
                return content;
            }

            var bannedWords = _banWordsReader.GetBannedList();

            return bannedWords.Aggregate(content, func: (current, banWord) => DecoreateBanWord(new Regex(String.Format(_wordRegexProvider.GetWordMatchingRegex(),banWord)).Matches(current), current, banWord));
        }

        private string DecoreateBanWord(MatchCollection banWordMatch, string text, string banWord)
        {
            foreach (Match matchItem in banWordMatch)
            {
                var replace = matchItem.ToString().Replace(banWord, AddBannedSymbols(banWord));
                text = new Regex(string.Format(_wordRegexProvider.GetBadWordDecoratingRegex(), matchItem.Value)).Replace(text, replace);
            }

            return text;
        }

        private string AddBannedSymbols(string banWord)
        {
            return banWord.Substring(0, 1) + new string('#', banWord.Length - 2) + banWord.Substring(banWord.Length - 1, 1);
        }
    }
}
