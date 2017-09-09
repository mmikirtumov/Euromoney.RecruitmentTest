using ContentConsole.Logic;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ContentConsole.Test.Unit
{
    [TestFixture]
    public class BannedTextViewTest
    {
        private string _someText;
        private Mock<IBanWordsReader> _mockBanWordsReader;
        private readonly IWordRegexProvider _wordRegexProviderord = new WordRegexProvider();
        
        [SetUp]
        public void Init()
        {
            _mockBanWordsReader = new Mock<IBanWordsReader>();
            _someText = "some banned";
        }

        private IEnumerable<string> SomeBannedWords
        {
            get
            {
                var someBannedWords = new List<string> { "some", "let's" };
                return someBannedWords;
            }
        }

        [Test]
        public void BannedTextFilter_DisableFilter_ShowBannedText()
        {
            // Arrange
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWords);
            var textViewer = new BannedTextViewer(_mockBanWordsReader.Object, _wordRegexProviderord, false);

            // Act
            var result = textViewer.BannedTextFilter(_someText);

            // Assert
            Assert.AreEqual(_someText, result, "Should show banned text");
        }

        [Test]
        public void BannedTextFilter_EnableFilter_NotShowBannedText()
        {
            // Arrange
            var actualResult = "s##e banned";
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWords);
            var textViewer = new BannedTextViewer(_mockBanWordsReader.Object, _wordRegexProviderord, true);

            // Act
            var result = textViewer.BannedTextFilter(_someText);

            // Assert
            Assert.AreNotEqual(_someText, result, "Should not show banned text");
            Assert.AreEqual(actualResult, result, "Should change the text");
        }

        [Test]
        public void BannedTextFilter_EnableFilterNoBannedWords_ShowText()
        {
            // Arrange
            var actualResult = "some banned";
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(new List<string>());
            var textViewer = new BannedTextViewer(_mockBanWordsReader.Object, _wordRegexProviderord, true);

            // Act
            var result = textViewer.BannedTextFilter(_someText);

            // Assert
            Assert.AreEqual(actualResult, result, "Should not change the text");
        }

        [Test]
        public void BannedTextFilter_EnableFilter_CallGetBannedListOnce()
        {
            // Arrange
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWords);
            var textViewer = new BannedTextViewer(_mockBanWordsReader.Object, _wordRegexProviderord, true);

            // Act
            textViewer.BannedTextFilter(_someText);

            // Assert
            _mockBanWordsReader.Verify(reader => reader.GetBannedList(), Times.Once, "Should call GetBannedList");
        }

        [Test]
        public void BannedTextFilter_EnableFilterAndBannedWordContainedWithinAnotherWord_ShowsFullWord()
        {
            // Arrange
            var actualResult = "something banned";
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWords);
            var textViewer = new BannedTextViewer(_mockBanWordsReader.Object, _wordRegexProviderord, true);

            // Act
            var result = textViewer.BannedTextFilter("something banned");

            // Assert
            Assert.AreEqual(actualResult, result, "Should not change the text");
        }

        [Test]
        public void BannedTextFilter_EnableFilterTextContainedPunctuation_NotShowBannedText()
        {
            var actualResult = "s##e; banned";
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWords);
            var textViewer = new BannedTextViewer(_mockBanWordsReader.Object, _wordRegexProviderord, true);

            // Act
            var result = textViewer.BannedTextFilter("some; banned");

            // Assert
            Assert.AreEqual(actualResult, result, "Should change the text");
        }

        [Test]
        public void BannedTextFilter_EnableFilterTextContainedDash_NotShowBannedText()
        {
            var actualResult = "s##e-very banned";
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWords);
            var textViewer = new BannedTextViewer(_mockBanWordsReader.Object, _wordRegexProviderord, true);

            // Act
            var result = textViewer.BannedTextFilter("some-very banned");

            // Assert
            Assert.AreEqual(actualResult, result, "Should change the text");
        }

        [Test]
        public void BannedTextFilter_EnableFilterTextContainedApostrophe_NotShowBannedText()
        {
            var actualResult = "l###s";
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWords);
            var textViewer = new BannedTextViewer(_mockBanWordsReader.Object, _wordRegexProviderord, true);

            // Act
            var result = textViewer.BannedTextFilter("let's");

            // Assert
            Assert.AreEqual(actualResult, result, "Should change the text");
        }

        [Test]
        public void BannedTextFilter_EnableFilterTextEndsWithNumber_NotShowBannedText()
        {
            var actualResult = "l###s4";
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWords);
            var textViewer = new BannedTextViewer(_mockBanWordsReader.Object, _wordRegexProviderord, true);

            // Act
            var result = textViewer.BannedTextFilter("let's4");

            // Assert
            Assert.AreEqual(actualResult, result, "Should change the text");
        }

        [Test]
        public void BannedTextFilter_EnableFilterTextStartsWithNumber_NotShowBannedText()
        {
            var actualResult = "4l###s";
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWords);
            var textViewer = new BannedTextViewer(_mockBanWordsReader.Object, _wordRegexProviderord, true);

            // Act
            var result = textViewer.BannedTextFilter("4let's");

            // Assert
            Assert.AreEqual(actualResult, result, "Should change the text");
        }
    }
}
