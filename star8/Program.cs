using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Diagnostics;

namespace star8
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Star8!");
            var input = File.ReadAllLines(@"C:\Stars\adventofcode\star8\input.txt");

            var instructions = input[0].Select(x => x == 'L' ? 0 : 1).ToArray();
            var nodes =
                input.Skip(2)
                .Select(x => x.Split(new[] { ' ', ',', '(', ')', '=' }, StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(x => x[0], x => x[1..]);

            long result = Part1(nodes, instructions);
            Console.WriteLine($"Result1 = {result}");

            var cur = "AAA";
            var ip = 0;

            long result2 = 1;

            var cur2 = nodes.Keys.Where(k => k[2] == 'A').ToList();

            foreach (var c2 in cur2)
            {
                var c3 = c2;
                long cnt = 0;
                ip = 0;
                while (c3[2] != 'Z')
                {
                    c3 = instructions[ip] == 'L' ? nodes[c3].GetValue(0).ToString() : nodes[c3].GetValue(1).ToString();
                    ip++;
                    cnt++;
                    if (ip == instructions.Length) ip = 0;
                }
                // determine smallest common multiplier
                var (g1, g2) = result2 > cnt ? (result2, cnt) : (cnt, result2);
                while (g2 != 0) (g1, g2) = (g2, g1 % g2);

                result2 = result2 * (cnt / g1);
            }
            Console.WriteLine($"Result2 = {result2}");
        }
        static long Part1(Dictionary<string, string[]> nodes, int[] instructions)
        {
            long result1 = 0;

            for (var node = "AAA"; node != "ZZZ"; result1++)
                node = nodes[node][instructions[result1 % instructions.Length]];
            return result1;
        }

    }
}
