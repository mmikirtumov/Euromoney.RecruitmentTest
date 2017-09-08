using Moq;
using NUnit.Framework;
using ContentConsole.Logic;
using System.Collections.Generic;

namespace ContentConsole.Test.Unit
{
    [TestFixture]
    public class BannedTextViewTest
    {
        private string _someText;
        private Mock<IBanWordsReader> _mockBanWordsReader;
        
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
                var someBannedWords = new List<string> { "some" };
                return someBannedWords;
            }
        }

        [Test]
        public void BannedTextFilter_DisableFilter_ShowBannedText()
        {
            // Arrange
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWords);
            var textViewer = new BannedTextViewer(_mockBanWordsReader.Object, false);

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
            var textViewer = new BannedTextViewer(_mockBanWordsReader.Object, true);

            // Act
            var result = textViewer.BannedTextFilter(_someText);

            // Assert
            Assert.AreNotEqual(_someText, result, "Should not show banned text");
            Assert.AreEqual(actualResult, result, "Should change the text");
        }

        [Test]
        public void BannedTextFilter_EnableFilter_CallGetBannedListOnce()
        {
            // Arrange
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWords);
            var textViewer = new BannedTextViewer(_mockBanWordsReader.Object, true);

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
            var textViewer = new BannedTextViewer(_mockBanWordsReader.Object, true);

            // Act
            var result = textViewer.BannedTextFilter("something banned");

            // Assert
            Assert.AreEqual(actualResult, result, "Should not change the text");
        }
    }
}
