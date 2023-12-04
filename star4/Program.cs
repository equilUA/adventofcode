using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace star4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("STAR4");

            string[] lines = File.ReadAllLines(@"C:\Stars\adventofcode\star4\input.txt");
            int result = 0;
            var winningNumberCounts = GetWinningNumberCounts(lines);
            var cardCounts = Enumerable.Range(0, lines.Length).ToDictionary(index => index, _ => 1);

            for (var i = 0; i < lines.Length; ++i)
            {
                for (var j = 1; j <= winningNumberCounts[i] && j + i < lines.Length; ++j)
                {
                    cardCounts[i + j] += cardCounts[i];
                }
            }

            Console.WriteLine($"Part 2: {cardCounts.Values.Sum()}");
        }

        static private int CheckGame1(string line)
        {
            var reveals = line.Split(": ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1].Split("| ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var numberRegex = new Regex(@"\d+");
            var split = line.Split(": ")[1].Split(" | ");
            var winningNumbers = numberRegex.Matches(split[0]).Select(match => match.Value);
            var myNumbers = numberRegex.Matches(split[1]).Select(match => match.Value);
            int points = 0;
            foreach (var numbers in winningNumbers)
            {
                if(!string.IsNullOrEmpty(numbers.Trim()))
                {
                    Console.WriteLine(numbers);
                    if (points == 0)
                        points = 1;
                    else
                        points *= 2;
                }
            }

            Console.WriteLine("Points :" + points);
            return points;
        }


        private static List<int> GetWinningNumberCounts(string[] cards)
        {
            var winningNumbersCount = new List<int>();
            var numberRegex = new Regex(@"\d+");

            foreach (var card in cards)
            {
                var split = card.Split(": ")[1].Split(" | ");
                var winningNumbers = numberRegex.Matches(split[0]).Select(match => match.Value);
                var myNumbers = numberRegex.Matches(split[1]).Select(match => match.Value);

                winningNumbersCount.Add(winningNumbers.Intersect(myNumbers).Count());
            }

            return winningNumbersCount;
        }

    }
}
