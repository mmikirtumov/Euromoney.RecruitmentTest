using System.Collections.Generic;
using System.Linq;

namespace ContentConsole.Logic
{
    public class BannedWordsCounter : IBannedWordsCounter
    {
        private readonly IBanWordsReader _banWordsReader;

        public BannedWordsCounter(IBanWordsReader banWordsReader)
        {
            _banWordsReader = banWordsReader;
        }
        public int CountOfBannedWords(string content)
        {
            IEnumerable<string> bannedWords = _banWordsReader.GetBannedList();
            bannedWords = bannedWords.Select(bannedWord => bannedWord.ToLower()).ToList();
            content = content.ToLower();
            
            return bannedWords.Count(r => content.Contains(r));
        }
    }
}
