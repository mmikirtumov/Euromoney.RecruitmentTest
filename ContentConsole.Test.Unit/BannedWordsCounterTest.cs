using ContentConsole.Logic;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ContentConsole.Test.Unit
{
    [TestFixture]
    public class BannedWordsCounterTest
    {
        private string _someText;
        private Mock<IBanWordsReader> _mockBanWordsReader;
        private readonly IWordRegexProvider _wordRegexProviderordRegex = new WordRegexProvider();

        private IEnumerable<string> SomeBannedWordsWithResult
        {
            get
            {
                var someBannedWords = new List<string> {"this", "some", "let's"};
                return someBannedWords;
            }
        }

        private IEnumerable<string> SomeBannedWordsWithNoResult
        {
            get
            {
                var someBannedWords = new List<string> {"risk"};
                return someBannedWords;
            }
        }

        [SetUp]
        public void Init()
        {
            _mockBanWordsReader = new Mock<IBanWordsReader>();
            _someText = "This is some banned text";
        }

        [Test]
        public void CountOfBannedWords_TwoBannedWords_ReturnsTwo()
        {
            // Arrange
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWordsWithResult);
            var textViewer = new BannedWordsCounter(_mockBanWordsReader.Object, _wordRegexProviderordRegex);

            // Act
            var result = textViewer.CountOfBannedWords(_someText);

            // Assert
            Assert.AreEqual(2, result, "Should return 2 banned words");
        }

        [Test]
        public void CountOfBannedWords_NoBannedWords_ReturnZero()
        {
            // Arrange
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWordsWithNoResult);
            var textViewer = new BannedWordsCounter(_mockBanWordsReader.Object, _wordRegexProviderordRegex);

            // Act
            var result = textViewer.CountOfBannedWords(_someText);

            // Assert
            Assert.AreEqual(0, result, "Should return 0 banned words");
        }

        [Test]
        public void CountOfBannedWords_TwoOfTheSameBannedWords_ReturnsTwo()
        {
            // Arrange
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWordsWithResult);
            var textViewer = new BannedWordsCounter(_mockBanWordsReader.Object, _wordRegexProviderordRegex);

            // Act
            var result = textViewer.CountOfBannedWords("this this");

            // Assert
            Assert.AreEqual(2, result, "Should return 2 banned words");
        }

        [Test]
        public void CountOfBannedWords_BannedWordContainedInAnotherWord_ReturnsZero()
        {
            // Arrange
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWordsWithResult);
            var textViewer = new BannedWordsCounter(_mockBanWordsReader.Object, _wordRegexProviderordRegex);

            // Act
            var result = textViewer.CountOfBannedWords("something");

            // Assert
            Assert.AreEqual(0, result, "Should return 0 banned words");
        }

        [Test]
        public void CountOfBannedWords_TextContainedPunctuation_ReturnsTwo()
        {
            // Arrange
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWordsWithResult);
            var textViewer = new BannedWordsCounter(_mockBanWordsReader.Object, _wordRegexProviderordRegex);

            // Act
            var result = textViewer.CountOfBannedWords("this; this");

            // Assert
            Assert.AreEqual(2, result, "Should return 2 banned words");
        }

        [Test]
        public void CountOfBannedWords_DuplicateBannedWords_ReturnOne()
        {
            // Arrange
            var fileMockBanWords = new BanWordsReader("../../Assets/10BannedWords.txt");
            var textViewer = new BannedWordsCounter(fileMockBanWords, _wordRegexProviderordRegex);

            // Act
            var result = textViewer.CountOfBannedWords("some try");

            // Assert
            Assert.AreEqual(1, result, "Should return 1 banned word");
        }

        [Test]
        public void CountOfBannedWords_TextContainedApostrophe_ReturnOne()
        {
            // Arrange
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWordsWithResult);
            var textViewer = new BannedWordsCounter(_mockBanWordsReader.Object, _wordRegexProviderordRegex);

            // Act
            var result = textViewer.CountOfBannedWords("let's do it");

            // Assert
            Assert.AreEqual(1, result, "Should return 1 banned word");
        }

        [Test]
        public void CountOfBannedWords_TextEndsWithNumber_ReturnOne()
        {
            // Arrange
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWordsWithResult);
            var textViewer = new BannedWordsCounter(_mockBanWordsReader.Object, _wordRegexProviderordRegex);

            // Act
            var result = textViewer.CountOfBannedWords("let's4 do it");

            // Assert
            Assert.AreEqual(1, result, "Should return 1 banned word");
        }

        [Test]
        public void CountOfBannedWords_TextStartsWithNumber_ReturnOne()
        {
            // Arrange
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWordsWithResult);
            var textViewer = new BannedWordsCounter(_mockBanWordsReader.Object, _wordRegexProviderordRegex);

            // Act
            var result = textViewer.CountOfBannedWords("4let's do it");

            // Assert
            Assert.AreEqual(1, result, "Should return 1 banned word");
        }
    }
}
