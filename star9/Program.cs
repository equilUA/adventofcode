using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Diagnostics;

namespace star9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Star9!");
            var input = File.ReadAllLines(@"C:\Stars\adventofcode\star9\input.txt");
            var part1 = input.Select(line => GetNextValue(line.Split().Select(int.Parse).ToList())).Sum();

            Console.WriteLine($"Part1: {part1}");
            var part2 = input.Select(line => GetPreviousValue(line.Split().Select(int.Parse).ToList())).Sum();

            Console.WriteLine($"Part2: {part2}");
        }

        static int GetNextValue(List<int> history)
        {
            List<int> diff;
            List<List<int>> listHist = new List<List<int>> { new List<int>(history) };

            do
            {
                diff = listHist.Last().Zip(listHist.Last().Skip(1), (a, b) => b - a).ToList();
                listHist.Add(diff);
            }
            while (diff.Any(d => d != 0));


            for (int i = listHist.Count - 2; i >= 0; i--)
            {
                listHist[i].Add(listHist[i].Last() + listHist[i + 1].Last());
            }

            return listHist[0].Last();
        }

        static int GetPreviousValue(List<int> history)
        {
            List<int> diff;
            List<List<int>> listHist = new List<List<int>> { new List<int>(history) };

            do
            {
                diff = listHist.Last().Zip(listHist.Last().Skip(1), (a, b) => b - a).ToList();
                listHist.Add(diff);
            }
            while (diff.Any(d => d != 0));

            for (int i = listHist.Count - 2; i >= 0; i--)
            {
                listHist[i].Insert(0, listHist[i][0] - listHist[i + 1][0]);
            }

            return listHist[0].First();
        }
    }
}
