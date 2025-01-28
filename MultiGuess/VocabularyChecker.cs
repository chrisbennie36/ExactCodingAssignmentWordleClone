namespace MultiGuess
{
    internal class VocabularyChecker : IVocabularyChecker
    {
        //Don't make this a nullable list, easier to debug and maintain with default behaviour being an empty enumerable. No need for null checks everywhere in this case.
        //Replace with List<string> stringList = Enumerable.Empty<string>();
        List<string>? stringList;

        public VocabularyChecker()
        {
            StreamReader? reader = null;
            try
            {
                //Wrap this in a using (StreamReader reader = new FileStream) statement
                //FileMode would be better as FileMode.Open as we can then handle our FileNotFoundException as detailed below
                reader = new StreamReader(new FileStream("wordlist.txt", FileMode.OpenOrCreate));

                //Check reader for null before proceeding
                // await this method and call from within a method returning async Task
                var content = reader.ReadToEndAsync();

                //Better to have a separate class which fetches the words from the wordlist.txt and leave this method soley responsible for checking the vocabulary.
                //...Speaking of which, some vocabulary checking logic is missing from this method! 
                stringList = content.Result.Split('\n').ToList();
            }
            //Replace this with a FileNotFoundException and log a more specific error message
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                reader?.Dispose();
            }
        }

        //Rename to make the method purpose clearer - WordExists
        public bool Exists(string word)
        {
            //No need for the == true part, return stringList.Contains(word) is enough 
            return stringList?.Contains(word) == true;
        }
    }
}
