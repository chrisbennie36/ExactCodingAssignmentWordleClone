namespace MultiGuess.Words;

public class WordListReader : IWordListReader
{
    public static IEnumerable<string> wordList = Enumerable.Empty<string>();

    public async Task<IEnumerable<string>> GetWordsFromFile(string filePath)
    {
        if(wordList.Any())
        {
            return wordList;
        }

        try
        {
            //Wrap this in a using (StreamReader reader = new FileStream) statement
            using (StreamReader reader = new StreamReader(new FileStream(filePath, FileMode.OpenOrCreate)))
            {
                var content = await reader.ReadToEndAsync();

                wordList = content.Split('\n').ToList();

                return wordList;
            }
        }

        catch (FileNotFoundException)
        {
            Console.WriteLine($"Could not find file at path: {filePath}");
            return wordList;
        }
    }
}
