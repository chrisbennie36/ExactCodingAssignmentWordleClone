using System.Collections.Concurrent;
using MultiGuess.Words;

namespace MultiGuess.PlayerGuesses.Validators;

public class PlayerGuessValidator : IPlayerGuessValidator
{
    private const string WordListFilePath = "wordlist.txt";

    private readonly IWordListReader wordListReader;
    private readonly IGameWordsFactory gameWordsFactory;

    public PlayerGuessValidator(IWordListReader wordListReader, IGameWordsFactory gameWordsFactory)
    {
        ArgumentNullException.ThrowIfNull(wordListReader, nameof(wordListReader));
        ArgumentNullException.ThrowIfNull(gameWordsFactory, nameof(gameWordsFactory));

        this.wordListReader = wordListReader;
        this.gameWordsFactory = gameWordsFactory;
    }

    public async Task<bool> ValidatePlayerGuess(string playerGuess)
    {
        if(await IsInvalidEnglishWord(playerGuess) || !await HasMatchingCharacterPositionsInAtLeastOnePartiallyRevealedWord(playerGuess))
        {
            return false;
        }

        return true;    //ToDo: Return words with character matches
    }

    private async Task<bool> HasMatchingCharacterPositionsInAtLeastOnePartiallyRevealedWord(string playerGuess)
    {
        ConcurrentBag<bool> guessMatchesACharacterInGameWord = new ConcurrentBag<bool>();
        Parallel.ForEach(await gameWordsFactory.GetWordsForGame(), word => 
        {
            //Revealed words should not be validated - ideally I'd implement logic to only return non-fully revelaed words from the GameWordFactory...
            if(word.Contains('*'))
            {
                guessMatchesACharacterInGameWord.Add(HasMatchingCharacterPositionsInPartiallyRevealedWord(playerGuess, word));
            }
        });

        return guessMatchesACharacterInGameWord.Any(g => g == true);
    }

    private bool HasMatchingCharacterPositionsInPartiallyRevealedWord(string playerGuess, string gameWord)
    {
        char[] gameWordChars = gameWord.ToCharArray();
        char[] playerGuessChars = playerGuess.ToCharArray();

        for(int i = 0; i < gameWordChars.Count(); i++)
        {
            if(gameWordChars[i] == '*')
            {
                continue;
            }

            if(playerGuessChars[i] != gameWordChars[i])
            {
                return false;
            }
        }

        return true;
    }

    private async Task<bool> IsInvalidEnglishWord(string playerGuess)
    {
        IEnumerable<string> validEnglishWords = await wordListReader.GetWordsFromFile(WordListFilePath);

        if(!validEnglishWords.Contains(playerGuess))
        {
            return true;
        }

        return false;
    }

}
