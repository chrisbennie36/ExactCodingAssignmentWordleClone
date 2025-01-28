namespace MultiGuess.Words;

public interface IWordListReader
{
    Task<IEnumerable<string>> GetWordsFromFile(string filePath); 
}
