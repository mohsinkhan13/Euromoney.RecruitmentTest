namespace ContentConsole.Models
{
    //TODO create a base class as Word and derive BadWord class from it
    // this will help abstract out and test different kinds of words
    public class BadWord
    {
        public string Value { get; set; }
    }
}