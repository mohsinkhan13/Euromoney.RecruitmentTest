using System.Collections.Generic;
using ContentConsole.Models;

namespace ContentConsole.Repositories
{
    public interface IRepository
    {
        int Add(BadWord word);
        int Remove(BadWord word);
        IEnumerable<BadWord> GetList();
    }
}