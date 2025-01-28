namespace MultiGuess.Manager;

public interface IGameManager
{
    Task<IEnumerable<string>> ProcessPlayerGuess(string playerGuess);
}
