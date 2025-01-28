using Moq;
using MultiGuess.Manager;
using MultiGuess.PlayerGuesses.Validators;
using MultiGuess.Words;
using NUnit.Framework;

namespace MultiGuessTests;

public class GameManagerTests
{
    private Mock<IPlayerGuessValidator> playerGuessValidatorMock;
    private Mock<IGameWordsFactory> gameWordsFactoryMock;

    private List<string> gameWords;

    private string hiddenWordBanjo = string.Empty;
    private string hiddenWordFence = string.Empty;
    private string hiddenWordNobel = string.Empty;

    [SetUp]
    public void SetUp()
    {
        playerGuessValidatorMock = new Mock<IPlayerGuessValidator>();
        gameWordsFactoryMock = new Mock<IGameWordsFactory>();

        hiddenWordBanjo = "B***o";
        hiddenWordFence = "Fe***";
        hiddenWordNobel = "*o**l";

        gameWords = new List<string>
        {
            hiddenWordBanjo,
            hiddenWordFence,
            hiddenWordNobel
        };

        gameWordsFactoryMock.Setup(g => g.GetWordsForGame(true)).ReturnsAsync(gameWords);
    }

    [Test]
    public async Task GameManagerTests_ProcessPlayerGuess_MakesNoChangesToWords_IfGuessInvalid()
    {
        string playerGuess = "Invalid Guess";

        playerGuessValidatorMock.Setup(v => v.ValidatePlayerGuess(playerGuess)).ReturnsAsync(false);

        var sut = new GameManager(playerGuessValidatorMock.Object, gameWordsFactoryMock.Object);

        IEnumerable<string> result = await sut.ProcessPlayerGuess(playerGuess);

        Assert.Contains(hiddenWordBanjo, result.ToList());
        Assert.Contains(hiddenWordFence, result.ToList());
        Assert.Contains(hiddenWordNobel, result.ToList());
    }

    [TestCase("Bands", "Ban*o")]
    [TestCase("Fends", "Fen**")]
    public async Task GameManagerTests_ProcessPlayerGuess_ReturnsWordsWithRevealedCharacters_IfGuessValid(string guess, string revealedWord)
    {
        playerGuessValidatorMock.Setup(v => v.ValidatePlayerGuess(guess)).ReturnsAsync(true);

        var sut = new GameManager(playerGuessValidatorMock.Object, gameWordsFactoryMock.Object);

        IEnumerable<string> result = await sut.ProcessPlayerGuess(guess);

        Assert.Contains(revealedWord, result.ToList());
    }
}
