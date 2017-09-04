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

        private List<string> SomeBannedWords
        {
            get
            {
                List<string> someBannedWords = new List<string> { "some" };
                return someBannedWords;
            }
        }

        [Test]
        public void BannedTextFilter_DisableFilter_ShowBannedText()
        {
            // Arrange
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWords);
            IBannedTextViewer textViewer = new BannedTextViewer(_mockBanWordsReader.Object, false);

            // Act
            string result = textViewer.BannedTextFilter(_someText);

            // Assert
            Assert.AreEqual(_someText, result, "Should show banned text");
        }

        [Test]
        public void BannedTextFilter_EnableFilter_NotShowBannedText()
        {
            // Arrange
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWords);
            IBannedTextViewer textViewer = new BannedTextViewer(_mockBanWordsReader.Object, true);

            // Act
            string result = textViewer.BannedTextFilter(_someText);

            // Assert
            Assert.AreNotEqual(_someText, result, "Should not show banned text");
        }

        [Test]
        public void BannedTextFilter_ChangeTheTex_ShowChangeText()
        {
            // Arrange
            string actualResult = "s##e banned";
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWords);
            IBannedTextViewer textViewer = new BannedTextViewer(_mockBanWordsReader.Object, true);

            // Act
            string result = textViewer.BannedTextFilter(_someText);

            // Assert
            Assert.AreEqual(actualResult, result, "Should change the text");
        }

        [Test]
        public void BannedTextFilter_GetBannedList_ShowCallTheMethod()
        {
            // Arrange
            _mockBanWordsReader.Setup(reader => reader.GetBannedList()).Returns(SomeBannedWords);
            IBannedTextViewer textViewer = new BannedTextViewer(_mockBanWordsReader.Object, true);

            // Act
            textViewer.BannedTextFilter(_someText);

            // Assert
            _mockBanWordsReader.Verify(reader => reader.GetBannedList(), Times.Once, "Should call GetBannedList");
        }
    }
}
