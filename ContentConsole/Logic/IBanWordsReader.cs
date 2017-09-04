using System.Collections.Generic;

namespace ContentConsole.Logic
{
    public interface IBanWordsReader
    {
        IEnumerable<string> GetBannedList();
    }
}
