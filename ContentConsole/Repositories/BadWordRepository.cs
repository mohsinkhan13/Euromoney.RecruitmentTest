using System.Collections.Generic;
using System.Linq;
using ContentConsole.Libraries;
using ContentConsole.Models;

namespace ContentConsole.Repositories
{
    public class BadWordRepository : IRepository
    {
        private readonly BadWordLibrary _library;

        public BadWordRepository()
        {
            //TODO introduce IOC like Autofac
            _library = new BadWordLibrary();
        }

        public int Add(BadWord word)
        {

            if (!_library.BadWords.Exists(x=> x.Value.ToLower() == word.Value.ToLower()))
                _library.BadWords.Add(word);
            return _library.BadWords.Count;
        }

        public int Remove(BadWord word)
        {
            var foundWord = _library.BadWords.FirstOrDefault(x => x.Value.ToLower() == word.Value.ToLower());
            if (foundWord == null) 
                return -1;
            
            _library.BadWords.Remove(foundWord);
            return _library.BadWords.Count;
        }

        public IEnumerable<BadWord> GetList()
        {
            return _library.BadWords;
        }
    }
}