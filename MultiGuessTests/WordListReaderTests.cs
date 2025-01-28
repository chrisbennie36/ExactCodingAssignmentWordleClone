using MultiGuess.Words;
using NUnit.Framework;

namespace MultiGuessTests;


public class WordListReaderTests
{
    private string filePath = string.Empty;

    [SetUp]
    public void SetUp()
    {
        filePath = "wordlist.txt";
    }

    [Test]
    public void WordListReaderTests_ThrowsFileNotFoundException_IfFileNotFound()
    {
        var sut = new WordListReader();

        var result = sut.GetWordsFromFile(filePath);

        Assert.Throws<FileNotFoundException>(async () => await sut.GetWordsFromFile("DodgyFilePath"));
    }
}
