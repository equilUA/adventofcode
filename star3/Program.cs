using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
namespace star3
{
    class Program
    {
        internal struct Number
        {
            public int Value { get; set; }
            public (int Row, int Column) Start { get; set; }
            public (int Row, int Column) End { get; set; }
        }

        internal struct Symbol
        {
            public char Value { get; set; }
            public (int Row, int Column) Position { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("STAR3");
            string[] lines = File.ReadAllLines(@"C:\Stars\adventofcode\star3\input.txt");
            int result = 0;
            var numbers = new List<Number>();
            var symbols = new List<Symbol>();
            for (var i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
                var currentNumber = new Number();
                var digits = new List<int>();

                for (var j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '.')
                        continue;

                    if (int.TryParse(lines[i][j].ToString(), out var digit))
                    {
                        digits.Add(digit);
                        if (digits.Count == 1)
                        {
                            currentNumber.Start = (i, j);
                        }

                        while (j < lines[i].Length - 1 && int.TryParse(lines[i][j + 1].ToString(), out digit))
                        {
                            digits.Add(digit);
                            j++;
                        }

                        currentNumber.End = (i, j);
                        currentNumber.Value = int.Parse(string.Join("", digits));
                        numbers.Add(currentNumber);
                        Console.WriteLine(currentNumber.Value);
                        currentNumber = new Number();
                        digits.Clear();
                    }
                    else
                    {
                        symbols.Add(new Symbol
                        {
                            Value = lines[i][j],
                            Position = (i, j)
                        });
                    }
                }


            }

            result = numbers.Where(number => symbols.Any(symbol =>
                        Math.Abs(symbol.Position.Row - number.Start.Row) <= 1
                        && symbol.Position.Column >= number.Start.Column - 1
                        && symbol.Position.Column <= number.End.Column + 1))
                    .Sum(number => number.Value);
            Console.WriteLine(result);

            var part2 = symbols.Where(symbol => symbol.Value == '*')
                        .Select(symbol => numbers.Where(number =>
                            Math.Abs(symbol.Position.Row - number.Start.Row) <= 1
                            && symbol.Position.Column >= number.Start.Column - 1
                            && symbol.Position.Column <= number.End.Column + 1)
                            .ToArray())
                        .Where(gears => gears.Length == 2)
                        .Sum(gears => gears[0].Value * gears[1].Value);

            Console.WriteLine(part2);
        }

    }
}
