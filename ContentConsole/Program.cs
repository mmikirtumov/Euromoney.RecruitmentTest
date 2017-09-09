using ContentConsole.Logic;
using System;

namespace ContentConsole
{
    public static class Program
    {
        private const string FileLocation = "../../BannedWords.txt";
        
        public static void Main(string[] args)
        {
            var content = GetContent();
            var showNegativeInformation = ShouldShowNegativeInformation();

            var fileReader = new BanWordsReader(FileLocation);
            var wordRegexProvider = new WordRegexProvider();

            ShowResult(FilterText(fileReader, wordRegexProvider, content, showNegativeInformation), ShowBadWordsCount(fileReader, wordRegexProvider, content));
        }

        private static int ShowBadWordsCount(IBanWordsReader fileReader, IWordRegexProvider wordRegexProvider, string content)
        {
            var bannedCounter = new BannedWordsCounter(fileReader, wordRegexProvider);

            return bannedCounter.CountOfBannedWords(content);
        }

        private static string FilterText(IBanWordsReader fileReader, IWordRegexProvider regexProvider, string content, bool showBannedText)
        {
            var textView = new BannedTextViewer(fileReader, regexProvider, showBannedText);

            return textView.BannedTextFilter(content);
        }

        private static string GetContent()
        {
            Console.WriteLine("Input Some Text");

            return Console.ReadLine();
        }

        private static bool ShouldShowNegativeInformation()
        {
            Console.WriteLine("Do you want to see the negative text (1-yes, 0 -no):");
            var showNegativeInput = Console.ReadLine();

            return showNegativeInput == "0";
        }

        private static void ShowResult(string finalContent,int badWordsCount)
        {
            Console.WriteLine("Scanned the text:");
            Console.WriteLine(finalContent);
            Console.WriteLine("Total Number of negative words: " + badWordsCount);

            Console.WriteLine("Press ANY key to exit.");
            Console.ReadKey();
        }
    }
}
