using System.Linq;

namespace ContentConsole.Logic
{
    public class BannedTextViewer: IBannedTextViewer
    {
        private readonly bool _enableFitler;
        private readonly IBanWordsReader _banWordsReader;

        public BannedTextViewer(IBanWordsReader banWordsReader, bool enableFitler)
        {
            _banWordsReader = banWordsReader;
            _enableFitler = enableFitler;
        }

        public string BannedTextFilter(string content)
        {
            if (!_enableFitler)
            {
                return content;
            }

            var bannedWords = _banWordsReader.GetBannedList();
            var result = content.ToLower();

            return bannedWords.Aggregate(result, (current, banWord) => current.Replace(banWord, DecoreateBanWord(banWord)));
        }

        private string DecoreateBanWord(string banWord)
        {
            return banWord.Substring(0,1)+new string('#', banWord.Length-2)+ banWord.Substring(banWord.Length-1,1);
        }
    }
}
