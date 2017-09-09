using ContentConsole.Logic;
using NUnit.Framework;
using System.Linq;

namespace ContentConsole.Test.Unit
{
    [TestFixture]
    public class BanWordsReaderTest
    {
        private string _doubleBannedFilePath, _bannedWords;

        [SetUp]
        public void Init()
        {
            _doubleBannedFilePath = @"../../Assets/DoubleBannedWords.txt";
            _bannedWords = @"../../Assets/10BannedWords.txt";
        }

        [Test]
        public void GetBannedList_DuplicateBannedWords_ReturnsOneBanWord()
        {
            // Arrange
            var banReader = new BanWordsReader(_doubleBannedFilePath);

            // Act
            var result = banReader.GetBannedList();

            // Assert
            Assert.AreEqual(1, result.Count(), "Should return 1 banned words");
        }

        [Test]
        public void GetBannedList_BannedWordsList_ReturnsTenBanWord()
        {
            // Arrange
            var banReader = new BanWordsReader(_bannedWords);

            // Act
            var result = banReader.GetBannedList();

            // Assert
            Assert.AreEqual(10, result.Count(), "Should return 10 banned words");
        }
    }
}
