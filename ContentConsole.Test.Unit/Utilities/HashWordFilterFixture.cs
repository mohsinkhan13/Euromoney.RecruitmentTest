using System.Collections.Generic;
using ContentConsole.Models;
using ContentConsole.Utilities;
using NUnit.Framework;

namespace ContentConsole.Test.Unit.Utilities
{
    [TestFixture]
    public class HashWordFilterFixture
    {
        [Test]
        public void WordHashedExceptFirstAndLast()
        {
            const string expectedWord = "h######e";
            var badWords = new List<BadWord>
            {
                new BadWord{Value = "bad"},
                new BadWord{Value = "swine"},
                new BadWord{Value = "horrible"},
            };

            var filter = new HashWordFilter();

            var actualWord = filter.Filter("horrible", badWords);

            Assert.AreEqual(expectedWord, actualWord);
        }

        [Test]
        public void FilterKeepsWordAsIsIfNotInList()
        {
            const string expectedWord = "lovely";
            var badWords = new List<BadWord>
            {
                new BadWord{Value = "bad"},
                new BadWord{Value = "swine"},
                new BadWord{Value = "horrible"},
            };

            var filter = new HashWordFilter();

            var actualWord = filter.Filter("lovely", badWords);

            Assert.AreEqual(expectedWord, actualWord);
        }

        [Test]
        public void ApplyFilterFlagDefaultsToTrue()
        {
            var filter = new HashWordFilter();
            
            Assert.IsTrue(filter.ApplyFilter);
        }

        [Test]
        public void ApplyFilterFlagCanBesetToFalse()
        {
            var filter = new HashWordFilter();
            filter.ApplyFilter = false;

            Assert.IsFalse(filter.ApplyFilter);
        }

        [Test]
        public void FilterDoesNotHashIfApplyFilterIsFalse()
        {
            const string expectedWord = "horrible";
            var badWords = new List<BadWord>
            {
                new BadWord{Value = "bad"},
                new BadWord{Value = "swine"},
                new BadWord{Value = "horrible"},
            };

            var filter = new HashWordFilter();
            filter.ApplyFilter = false;

            var actualWord = filter.Filter("horrible", badWords);

            Assert.AreEqual(expectedWord, actualWord);
        }
    }
}
