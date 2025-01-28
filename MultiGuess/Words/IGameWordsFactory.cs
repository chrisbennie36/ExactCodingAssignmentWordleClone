namespace MultiGuess.Words;

public interface IGameWordsFactory
{
    public Task<IEnumerable<string>> GetWordsForGame(bool newGame = false);
}
