using System;
using System.Collections.Generic;
using TestTask.Tree;

namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputDataSet = new List<string>
            {
                "1. Шторы",
                "1.1 Двери",
                "1.3 Окна",
                "1.2 Пальмы",
                "2.1.1 Скатерть",
                "2. Цемент",
                "2.2 Краска",
                "2.1 Плодовые",
                "2.1.2 Монеты",
            };

            Dictionary<string, string> parsingDataSet = ParseInputData(inputDataSet);

            var treeNodeCollection = new NestingListTree(parsingDataSet);

            treeNodeCollection.Traverse(x => Console.WriteLine($"{x.Key} {x.Value} -> "));

            Console.ReadKey();
        }

        public static Dictionary<string, string> ParseInputData(IEnumerable<string> inputDataSet)
        {
            var validDtaSet = new Dictionary<string, string>();

            foreach (string str in inputDataSet)
            {
                string[] spleatedString = str.Split(' ');
                if (!String.IsNullOrEmpty(spleatedString[0]) && !String.IsNullOrEmpty(spleatedString[1]))
                {
                    validDtaSet.Add(spleatedString[0], spleatedString[1].Trim());
                }
            }

            return validDtaSet;
        }
    }
}
