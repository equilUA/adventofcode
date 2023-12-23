using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Diagnostics;
using System.Collections.Immutable;
using System.Numerics;
using System;
using System.Collections.Generic;

namespace star15
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Star15");
            List<string> inputFile = File.ReadAllText(@"C:\Stars\adventofcode\star15\input.txt").Split(',').ToList();
            static int HASH(string input) => input.Aggregate(0, (x, y) => (((x + y) * 17) % 256));

            int part1Answer = inputFile.Sum(HASH);

            List<List<(string Label, int LensPower)>> boxes = new();
            for (int i = 0; i < 256; i++) boxes.Add(new());

            foreach (string input in inputFile)
            {
                char opCode = input.Contains('=') ? '=' : '-';

                string label = input[..input.IndexOfAny("-=".ToCharArray())];
                int lensPower = opCode == '=' ? (input[^1] - '0') : -1;
                int hashIndex = HASH(label);

                int index = boxes[hashIndex].FindIndex(x => x.Label == label);

                if (opCode == '-' && index != -1) boxes[hashIndex].RemoveAt(index);
                if (opCode == '=' && index == -1) boxes[hashIndex].Add((label, lensPower));
                if (opCode == '=' && index != -1) boxes[hashIndex][index] = (label, lensPower);
            }

            int part2Answer = boxes.Select((box, boxIdx) => box.Select((lens, lensIdx) => (1 + boxIdx) * (1 + lensIdx) * lens.LensPower).Sum()).Sum();

            Console.WriteLine($"Part 1: The hash of each line gives a sum of {part1Answer}.");
            Console.WriteLine($"Part 2: The focusing power of the lens array is {part2Answer}.");
        }
    }
}
