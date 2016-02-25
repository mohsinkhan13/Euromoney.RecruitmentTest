using System.Linq;
using ContentConsole.Libraries;
using ContentConsole.Models;
using ContentConsole.Repositories;
using NUnit.Framework;

namespace ContentConsole.Test.Unit.Repositories
{
    [TestFixture]
    public class BadWordRepositoryFixture
    {
        private IRepository _badWordRepository;

        [SetUp]
        public void Setup()
        {
            _badWordRepository = new BadWordRepository();
        }

        [Test]
        public void GetListReturnsListOfCountZero()
        {
            const int expectedCount = 0;

            var returnedBadWords = _badWordRepository.GetList();

            Assert.AreEqual(expectedCount,returnedBadWords.Count());
        }

        [Test]
        public void AddsAndCountIsOne()
        {
            const int expectedCount = 1;
            var newWord = new BadWord {Value = "Bad"};

            var actualCount = _badWordRepository.Add(newWord);

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddsShouldNotAddExistingWord()
        {
            var newWord = new BadWord { Value = "Bad" };

            var expectedCount = _badWordRepository.Add(newWord);
            var actualCount = _badWordRepository.Add(newWord);

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void RemoveShouldGiveCountIsOne()
        {
            const int expectedCount = 1;
            var word1 = new BadWord { Value = "Bad" };
            var word2 = new BadWord { Value = "swine" };
            _badWordRepository.Add(word1);
            _badWordRepository.Add(word2);
            
            var actualCount = _badWordRepository.Remove(word1);

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void RemoveReturnsNegativeIfNotfound()
        {
            var word1 = new BadWord { Value = "Bad" };
            var word2 = new BadWord { Value = "swine" };
            _badWordRepository.Add(word1);
            _badWordRepository.Add(word2);

            var actualCount = _badWordRepository.Remove(new BadWord{Value = "hate"});

            Assert.IsTrue(actualCount < 0);
        }

    }
}
