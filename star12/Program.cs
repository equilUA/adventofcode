using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Diagnostics;
using System.Collections.Immutable;
using System.Numerics;

namespace star12
{
    class Program
    {
        static Dictionary<string, long> cache = new Dictionary<string, long>();
        static void Main(string[] args)
        {
            Console.WriteLine("Star12!");
            long part1 = 0;
            long part2 = 0;
            
            var input = File.ReadAllLines(@"C:\Stars\adventofcode\star12\input.txt");

            foreach (var line in input)
            {
                var springs = line.Split(" ").ToArray();
                var rows = springs[0];
                var damaged = springs[1].Split(",").Select(a => int.Parse(a)).ToArray();

                var rowsList = String.Format("{0}?{0}?{0}?{0}?{0}", rows);
                var damagedList = damaged.ToList();
                for (int i = 0; i < 4; i++) foreach (var v in damaged) damagedList.Add(v);
                var damagedArray = damagedList.ToArray();

                var subresults = new Dictionary<(int p, int c), long>();
                part1 += CheckPos(rows, damaged, 0, 0, subresults);
                subresults = new Dictionary<(int p, int c), long>();
                part2 += CheckPos(rowsList, damagedArray, 0, 0, subresults);
            }
            Console.WriteLine(part1);
            Console.WriteLine(part2);
        }

        static long CheckPos(string row, int[] damaged, int offset, int current, Dictionary<(int p, int c), long> subresults)
        {
            long result = 0;
            int i = offset;
            bool ready = false;
            if (subresults.ContainsKey((offset, current))) result = subresults[(offset, current)];
            else
            {
                while (!ready && i <= row.Length - damaged[current])
                {
                    string check = row.Substring(i, damaged[current]);
                    var checkOff = i + damaged[current];
                    if (check.Length == damaged[current] && !check.Contains('.'))
                    {
                        if (current == damaged.Length - 1)
                        {
                            if (checkOff == row.Length || !row.Substring(checkOff).Contains("#")) result++;
                        }
                        else
                        {
                            if (checkOff < row.Length && (row[checkOff] == '.' || row[checkOff] == '?'))
                            {
                                result += CheckPos(row, damaged, checkOff + 1, current + 1, subresults);
                            }
                        }
                    }
                    if (row[i] == '.' || row[i] == '?') i++;
                    else ready = true;
                }
                subresults[(offset, current)] = result;
            }
            return result;
        }
    }
}
