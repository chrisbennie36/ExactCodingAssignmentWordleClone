using Moq;
using MultiGuess;
using MultiGuess.Words;
using NUnit.Framework;

namespace MultiGuessTests;

public class GameWordsFactoryTests
{
    private Mock<IWordListReader> wordListReaderMock;

    [SetUp]
    public void SetUp()
    {
        List<string> words = new List<string>
        {
            "To", "Be", "Or", "Not", "To", "Be", "Might", "Be", "A", "Very", "Good", "Example"
        };

        wordListReaderMock = new Mock<IWordListReader>();

        wordListReaderMock.Setup(w => w.GetWordsFromFile(It.IsAny<string>())).ReturnsAsync(words);
    }

    [Test]
    public async Task GameWordsFactoryTests_ReturnsListOfWordsForUseInGame()
    {
        var sut = new GameWordsFactory(wordListReaderMock.Object);

        var result = await sut.GetWordsForGame();

        Assert.IsNotEmpty(result);
    }

    [Test]
    public async Task GameWordsFactoryTests_AlwaysReturnsGameWords_InTheSameOrder()
    {
        var sut = new GameWordsFactory(wordListReaderMock.Object);

        var initialWords = await sut.GetWordsForGame();
        var nextWords = await sut.GetWordsForGame();

        string[] initialWordsArray = initialWords.ToArray();
        string[] nextWordsArray = nextWords.ToArray();

        for(int i = 0; i< initialWordsArray.Length; i++)
        {
            Assert.AreEqual(initialWordsArray[i], nextWordsArray[i]);
        }
    }
}
