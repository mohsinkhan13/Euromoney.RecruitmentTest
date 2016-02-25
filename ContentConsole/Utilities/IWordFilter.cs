using System.Collections;
using System.Collections.Generic;
using ContentConsole.Models;

namespace ContentConsole.Utilities
{
    public interface IWordFilter
    {
        string Filter(string word, IEnumerable<BadWord> badWords);
        bool ApplyFilter { get; set; }
    }
}