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

        private IEnumerable<string> SomeBannedWordsWithResult
        {
            get
            {
                var someBannedWords = new List<string> {"this", "some"};
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
            var textViewer = new BannedWordsCounter(_mockBanWordsReader.Object);

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
            var textViewer = new BannedWordsCounter(_mockBanWordsReader.Object);

            // Act
            var result = textViewer.CountOfBannedWords(_someText);

            // Assert
            Assert.AreEqual(0, result, "Should return 0 banned words");
        }
    }
}
