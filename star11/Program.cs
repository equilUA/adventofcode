using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Diagnostics;
using System.Collections.Immutable;
using System.Numerics;
namespace star11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Star11!");
            var input = File.ReadAllLines(@"C:\Stars\adventofcode\star11\input.txt").ToArray();

            var map = GalaxyMap.FromInput(input);
            Console.WriteLine(map.SumOfShortestDistances().ToString());
            var map2 = GalaxyMap.FromInput(input, 1000000);
            Console.WriteLine(map2.SumOfShortestDistances().ToString());
        }

        public record GalaxyMap(IList<Vector> Galaxies)
        {
            public long SumOfShortestDistances()
            {
                var pairs = Galaxies.SelectMany((g1, i) => Galaxies.Skip(i + 1).Select(g2 => (g1, g2))).ToArray();
                var sum = 0L;
                foreach (var (g1, g2) in pairs)
                {
                    sum += g1.VectorTo(g2).NumberSteps;
                }

                return sum;
            }

            public static GalaxyMap FromInput(IList<string> lines, long expansionMultiplier = 2)
            {
                var rowsToAdd = Enumerable.Range(0, lines.Count).Where(row => lines[row].All(c => c == '.')).ToArray();
                var colsToAdd = Enumerable.Range(0, lines[0].Length).Where(col => lines.All(l => l[col] == '.')).ToArray();

                var galaxies = new List<Vector>();
                for (var row = 0; row < lines.Count; row++)
                {
                    var rowOffset = rowsToAdd.Count(r => r <= row) * (expansionMultiplier - 1);
                    for (var col = 0; col < lines[0].Length; col++)
                    {
                        if (lines[row][col] != '#') continue;

                        var colOffset = colsToAdd.Count(c => c <= col) * (expansionMultiplier - 1);
                        galaxies.Add(new Vector(row + rowOffset, col + colOffset));
                    }
                }

                return new GalaxyMap(galaxies);
            }
        }

        public record Vector(long Row, long Col)
        {
            public override string ToString() => $"[{Row}, {Col}]";

            public Vector VectorTo(Vector other) => new(other.Row - Row, other.Col - Col);

            public long NumberSteps { get; } = Math.Abs(Row) + Math.Abs(Col);
        }
    }
}
