using MultiGuess.Manager;
using MultiGuess.Words;

namespace MultiGuess;

public class MultiplayerGuessingGame : IMultiplayerGuessingGame
{
    /*private IGameWordsFactory gameWordsFactory;
    private IGameManager gameManager;

    public MultiplayerGuessingGame(IGameWordsFactory gameWordsFactory, IGameManager gameManager)
    {
        ArgumentNullException.ThrowIfNull(gameWordsFactory, nameof(gameWordsFactory));
        ArgumentNullException.ThrowIfNull(gameManager, nameof(gameManager));

        this.gameWordsFactory = gameWordsFactory;
        this.gameManager = gameManager;
    }*/
    
    public IList<string> GetGameStrings()
    {
        throw new NotImplementedException();
        //return gameWordsFactory.GetWordsForGame();
    }

    public int SubmitGuess(string playerName, string submission)
    {
        throw new NotImplementedException();
        //Console.WriteLine($"Player: {playerName}, your revealed words are as follows:");
        /*foreach(string revealedWord in await gameManager.ProcessPlayerGuess(submission))
        {
            Console.WriteLine(revealedWord);
        }*/
    }
}
