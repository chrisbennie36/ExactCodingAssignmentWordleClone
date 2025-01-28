using System.Collections.Concurrent;
using MultiGuess.PlayerGuesses.Validators;
using MultiGuess.Words;

namespace MultiGuess.Manager;

public class GameManager : IGameManager
{
    private readonly IPlayerGuessValidator playerGuessValidator;
    private readonly IGameWordsFactory gameWordsFactory;

    public GameManager(IPlayerGuessValidator playerGuessValidator, IGameWordsFactory gameWordsFactory)
    {
        ArgumentNullException.ThrowIfNull(playerGuessValidator, nameof(playerGuessValidator));
        ArgumentNullException.ThrowIfNull(gameWordsFactory, nameof(gameWordsFactory));

        this.playerGuessValidator = playerGuessValidator;
        this.gameWordsFactory = gameWordsFactory;
    }

    public async Task<IEnumerable<string>> ProcessPlayerGuess(string playerGuess)
    {
        if(await playerGuessValidator.ValidatePlayerGuess(playerGuess) == false)
        {
            Console.WriteLine("You entered an invalid guess, please try again");
        }

        ConcurrentBag<string> revealedGameWords = new ConcurrentBag<string>();
        Parallel.ForEach(await gameWordsFactory.GetWordsForGame(), gameWord => 
        {
            revealedGameWords.Add(RevealCharacters(playerGuess, gameWord));
        });

        return revealedGameWords;
    }

    private string RevealCharacters(string playerGuess, string gameWord)
    {
        char[] gameWordChars = gameWord.ToCharArray();
        char[] playerGuessChars = playerGuess.ToCharArray();

        for(int i = 0; i < gameWordChars.Count(); i++)
        {
            try
            {
                if(gameWordChars[i] != '*')
                {
                    continue;
                }

                if(playerGuessChars[i] == gameWordChars[i])
                {
                    gameWordChars[i] = playerGuessChars[i];
                }
            }
            catch(IndexOutOfRangeException)
            {
                Console.Write($"Index out of range exception when attmepting to reveal characters for word: {gameWord} and player guess: {playerGuess}, index: {i}");
            }
        }

        return gameWordChars.ToString();
    }
}
