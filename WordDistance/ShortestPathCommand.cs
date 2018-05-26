﻿using System;
using System.IO;
using ManyConsole;

namespace WordDistance
{
    public class ShortestPathCommand : ConsoleCommand
    {
        private string _dictionaryFile;
        private string _startWord;
        private string _endWord;
        private string _resultFile;

        public ShortestPathCommand()
        {
            IsCommand("ShortestPath", "Returns the shortest list of four letter words starting with StartWord and ending with EndWord with a number of intermediate words that must appear in DictionaryFile where each word differs 's text");

            HasRequiredOption("d|dictionaryfile=", "The file name of a text file containing four letter words (included in the test pack)", t => _dictionaryFile = t);
            HasRequiredOption("s|startword=", "A four letter word (that you can assume is found in the DictionaryFile file).", t => _startWord = t);
            HasRequiredOption("e|endword=", "A four letter word (that you can assume is found in the DictionaryFile file) .", t => _endWord = t);
            HasRequiredOption("r|resultfile=", "The file name of a text file that will contain the result.", t => _resultFile = t);
        }

        public override int Run(string[] remainingArguments)
        {
            try
            {
                var dictionary = File.ReadAllLines(_dictionaryFile);

                IWordDistanceCalculator wordDistanceCalculator = new HammingWordDistanceCalculator();

                IWordPathLister wordPathLister = new ShortestEditPathLister(dictionary, wordDistanceCalculator);

                var result = wordPathLister.ListPath(_startWord, _endWord);

                File.WriteAllLines(_resultFile, result);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);

                return -1;
            }

            return 0;
        }
    }
}