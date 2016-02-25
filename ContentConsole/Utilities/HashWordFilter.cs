using System.Collections.Generic;
using System.Linq;
using ContentConsole.Models;

namespace ContentConsole.Utilities
{
    public class HashWordFilter : IWordFilter
    {
        private const char Hash = '#';

        public HashWordFilter()
        {
            ApplyFilter = true;
        }

        public bool ApplyFilter { get; set; }

        public string Filter(string word, IEnumerable<BadWord> badWords)
        {
            if (!ApplyFilter) return word;

            var hashedWord = "";

            var val = badWords.FirstOrDefault(x => x.Value == word);

            if (val == null) return word;
            
            hashedWord += word[0];
            for (var i = 1; i < word.Length - 1; i++)
            {
                hashedWord += Hash;
            }
            hashedWord += word[word.Length - 1];
            return hashedWord;
        }
    }
}