using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ContentConsole.Logic
{
    public class BanWordsReader : IBanWordsReader
    {
        private readonly string _filename;

        public BanWordsReader(string filename)
        {
            _filename = filename;
        }

        public IEnumerable<string> GetBannedList()
        {
            var lines = File.ReadAllLines(_filename);
            var result = new List<string>(lines);

            return result.Select(res => res.Trim().ToLower()).Distinct();
        }
    }
}
