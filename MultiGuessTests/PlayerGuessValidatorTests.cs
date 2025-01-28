using Moq;
using MultiGuess.PlayerGuesses.Validators;
using MultiGuess.Words;
using NUnit.Framework;

namespace MultiGuessTests;

public class PlayerGuessValidatorTests
{
    private Mock<IWordListReader> wordListReaderMock;
    private Mock<IGameWordsFactory> gameWordsFactoryMock;

    private string validWord = "Valid";

    [SetUp]
    public void SetUp()
    {
        wordListReaderMock = new Mock<IWordListReader>();
        gameWordsFactoryMock = new Mock<IGameWordsFactory>();

        wordListReaderMock.Setup(w => w.GetWordsFromFile(It.IsAny<string>())).ReturnsAsync(new List<string> { validWord });
    }

    public async Task PlayerGuessValidatorTests_ValidatePlayerGuess_ReturnsFalse_IfPlayerGuess_NotAValidEnglishWord()
    {
        var sut = new PlayerGuessValidator(wordListReaderMock.Object, )
    }
}
