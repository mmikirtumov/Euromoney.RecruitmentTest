using System.Collections.Generic;

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

            IEnumerable<string> bannedWords = _banWordsReader.GetBannedList();
            string result = content.ToLower();

            foreach(string banWord in bannedWords)
            {
               result=result.Replace(banWord, DecoreateBanWord(banWord));
            }

            return result;
        }

        private string DecoreateBanWord(string banWord)
        {
            return banWord.Substring(0,1)+new string('#', banWord.Length-2)+ banWord.Substring(banWord.Length-1,1);
        }
    }
}
