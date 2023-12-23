using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Diagnostics;
using System.Collections.Immutable;
using System.Numerics;
namespace star13
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Star13");
            int part1 = 0;
            int part2 = 0;

            var input = File.ReadAllText(@"C:\Stars\adventofcode\star13\input.txt").Split($"{Environment.NewLine}{Environment.NewLine}").Select(b => b.Split(Environment.NewLine)).ToList();

            foreach(string[] grid in input)
            {
                part1 += Part1(grid);
                part2 += Part2(grid);
            }

            Console.WriteLine(part1);
            Console.WriteLine(part2);
        }
        private static int Part1(string[] grid)
        {
            for (var reflectAfterCol = 0; reflectAfterCol < grid[0].Length - 1; reflectAfterCol++)
            {
                bool foundSmudges = false;
                for (var y = 0; y < grid.Length; y++)
                {
                    for (var deltaCol = 0; reflectAfterCol - deltaCol >= 0 && reflectAfterCol + deltaCol + 1 < grid[0].Length; deltaCol++)
                    {
                        if (grid[y][reflectAfterCol - deltaCol] != grid[y][reflectAfterCol + deltaCol + 1])
                        {
                            foundSmudges = true;
                            break;
                        }
                    }

                    if (foundSmudges) 
                        break;
                }

                if (!foundSmudges) 
                    return reflectAfterCol + 1;
            }

            for (var reflectAfterRow = 0; reflectAfterRow < grid.Length - 1; reflectAfterRow++)
            {
                bool foundSmudges = false;
                for (var x = 0; x < grid[0].Length; x++)
                {
                    for (var deltaRow = 0; reflectAfterRow - deltaRow >= 0 && reflectAfterRow + deltaRow + 1 < grid.Length; deltaRow++)
                    {
                        if (grid[reflectAfterRow - deltaRow][x] != grid[reflectAfterRow + deltaRow + 1][x])
                        {
                            foundSmudges = true;
                            break;
                        }
                    }

                    if (foundSmudges) 
                        break;
                }

                if (!foundSmudges) 
                    return (reflectAfterRow + 1) * 100;
            }

            throw new Exception("No match");
        }

        private static int Part2(string[] grid)
        {
            for (var reflectAfterCol = 0; reflectAfterCol < grid[0].Length - 1; reflectAfterCol++)
            {
                int foundSmudges = 0;
                for (var y = 0; y < grid.Length; y++) 
                {
                    for (var deltaCol = 0; reflectAfterCol - deltaCol >= 0 && reflectAfterCol + deltaCol + 1 < grid[0].Length; deltaCol++)
                    {
                        if (grid[y][reflectAfterCol - deltaCol] != grid[y][reflectAfterCol + deltaCol + 1])
                        {
                            foundSmudges++;
                            if (foundSmudges > 1) 
                                break;
                        }
                    }

                    if (foundSmudges > 1) 
                        break;
                }

                if (foundSmudges == 1) 
                    return reflectAfterCol + 1;
            }

            for (var reflectAfterRow = 0; reflectAfterRow < grid.Length - 1; reflectAfterRow++)
            {
                int foundSmudges = 0;
                for (var x = 0; x < grid[0].Length; x++)
                {
                    for (var deltaRow = 0; reflectAfterRow - deltaRow >= 0 && reflectAfterRow + deltaRow + 1 < grid.Length; deltaRow++)
                    {
                        if (grid[reflectAfterRow - deltaRow][x] != grid[reflectAfterRow + deltaRow + 1][x])
                        {
                            foundSmudges++;
                            if (foundSmudges > 1) 
                                break;
                        }
                    }

                    if (foundSmudges > 1) 
                        break;
                }

                if (foundSmudges == 1) 
                    return (reflectAfterRow + 1) * 100;
            }

            throw new Exception("No match");
        }

    }
}
