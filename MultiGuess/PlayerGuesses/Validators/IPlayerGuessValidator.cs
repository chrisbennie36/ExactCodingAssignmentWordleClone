namespace MultiGuess.PlayerGuesses.Validators;

public interface IPlayerGuessValidator
{
    public Task<bool> ValidatePlayerGuess(string playerGuess);
}
