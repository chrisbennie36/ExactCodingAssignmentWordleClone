using System.Net.Http.Headers;

namespace MultiGuess.Words;

public class GameWordsFactory : IGameWordsFactory   //ToDo: Make singleton
{
    const string WordListFilePath = "wordlist.txt";
    const int WordLength = 5;

    private readonly IWordListReader wordListReader;

    private List<string> gameWords = new List<string>();

    public GameWordsFactory(IWordListReader wordListReader)
    {
        ArgumentNullException.ThrowIfNull(wordListReader, nameof(wordListReader));

        this.wordListReader = wordListReader;
    }

    public async Task<IEnumerable<string>> GetWordsForGame(bool newGame = false)
    {
        if(!gameWords.Any() || newGame)
        {
            IEnumerable<string> words = await wordListReader.GetWordsFromFile(WordListFilePath);

            if(!words.Any())
            {
                return words;
            }

            IEnumerable<string> gameWords = words.Where(w => w.Length == WordLength);

            
        }
        
        return gameWords;
    }
}
