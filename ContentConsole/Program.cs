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

            ShowResult(FilterText(fileReader, content, showNegativeInformation), ShowBadWordsCount(fileReader, content));
        }

        private static int ShowBadWordsCount(IBanWordsReader fileReader, string content)
        {
            var bannedCounter = new BannedWordsCounter(fileReader);

            return bannedCounter.CountOfBannedWords(content);
        }

        private static string FilterText(IBanWordsReader fileReader, string content, bool showBannedText)
        {
            var textView = new BannedTextViewer(fileReader, showBannedText);

            return textView.BannedTextFilter(content);
        }

        private static string GetContent()
        {
            Console.WriteLine("Input Some Text");

            return Console.ReadLine();
        }

        private static bool ShouldShowNegativeInformation()
        {
            Console.WriteLine("Do want to see the negative text (1-yes, 0 -no):");
            var showNegativeInput = Console.ReadLine();

            return showNegativeInput == "0";
        }

        private static void ShowResult(string finalContent,int badWordsCount)
        {
            Console.WriteLine("Scanned the text:");
            Console.WriteLine(finalContent);
            Console.WriteLine("Total Number of negative words: " + badWordsCount);

            Console.WriteLine("Press ANY key to exit.");
            Console.ReadLine();
        }
    }
}
