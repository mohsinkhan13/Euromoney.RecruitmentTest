﻿using System;
using System.Collections.Generic;
using System.Linq;
using ContentConsole.Models;
using ContentConsole.Repositories;
using ContentConsole.Utilities;

namespace ContentConsole
{
    public static class Program
    {
        //TODO introduce IOC like autofac, etc
        private static readonly IRepository Repository = new BadWordRepository();
        private static  readonly IWordFilter WordFilter = new HashWordFilter();

        public static void Main(string[] args)
        {
            var badWords = InitializeBadWordsLibrary().ToList();
            var input = string.Empty;
            do
            {
                Console.WriteLine("");
                Console.WriteLine("Press 1 for content check");
                Console.WriteLine("Press 2 to add Bad word");
                Console.WriteLine("Press 3 to remove Bad word");
                Console.WriteLine("Press 4 to toggle Negative word filter");
                Console.WriteLine("Press X to exit");
                input = Console.ReadLine() ?? "";
                switch (input)
                {
                    case "1":
                    {
                        TriggerContentCheck(badWords);
                        break;
                    }
                    case "2":
                    {
                        TriggerBadWordAddition(out badWords);
                        break;
                    }
                    case "3":
                    {
                        TriggerBadWordRemoval(out badWords);
                        break;
                    }
                    case "4":
                    {
                        WordFilter.ApplyFilter = !WordFilter.ApplyFilter;
                        Console.WriteLine(string.Format("Negative word filter is now {0}", WordFilter.ApplyFilter ? "enabled" : "disabled"));
                        break;
                    }
                }


            } while (input.ToLower() != "x");
            
        }

        private static void TriggerBadWordRemoval(out List<BadWord> badWords)
        {
            Console.Write("Enter a word to remove: ");
            var userValue = Console.ReadLine();
            var newBadWord = new BadWord {Value = userValue};
            Repository.Remove(newBadWord);
            badWords = Repository.GetList().ToList();
            Console.WriteLine("List of bad words from the data store");
            foreach (var word in badWords)
            {
                Console.WriteLine(word.Value);
            }
        }

        private static void TriggerBadWordAddition(out List<BadWord> badWords)
        {
            Console.Write("Enter a word to add: ");
            var userValue = Console.ReadLine();
            var newBadWord = new BadWord {Value = userValue};
            Repository.Add(newBadWord);
            badWords = Repository.GetList().ToList();
            Console.WriteLine("List of bad words from the data store");
            foreach (var word in badWords)
            {
                Console.WriteLine(word.Value);
            }
        }

        private static void TriggerContentCheck(IEnumerable<BadWord> badWords)
        {
            Console.WriteLine("Enter content to be checked for bad words:");
            string content = Console.ReadLine();

            var contentList = content.ToLower().Split(' ').ToList();

            var filteredContent = contentList.Aggregate((x,y)=> WordFilter.Filter(x, badWords) + " " + WordFilter.Filter(y, badWords));

            var count = badWords.Select(
                word => contentList.Where(x => x.Contains(word.Value.ToLower())))
                .Select(res => res.Count())
                .Sum();

            Console.WriteLine("Scanned the text:");
            Console.WriteLine(filteredContent);
            Console.WriteLine("Total Number of negative words: " + count);

            Console.WriteLine("Press ANY key to exit.");
        }

        
        private static IEnumerable<BadWord> InitializeBadWordsLibrary()
        {
            Repository.Add(new BadWord { Value = "swine" });
            Repository.Add(new BadWord { Value = "bad" });
            Repository.Add(new BadWord { Value = "nasty" });
            Repository.Add(new BadWord { Value = "horrible" });

            return Repository.GetList();
        }
    }
}
