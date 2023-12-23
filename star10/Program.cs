using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Diagnostics;
using System.Collections.Immutable;
using System.Numerics;
using Map = System.Collections.Generic.Dictionary<System.Numerics.Complex, char>;
namespace star10
{
    class Program
    {
        internal struct Point
        {
            public int x { get; set; }
            public int y { get; set; }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Star10!");
            var lines = File.ReadAllLines(@"C:\Stars\adventofcode\star10\input.txt").ToList();
            long answer1 = 0;
            long answer2 = 0;

            var input = lines.Select(l => l.Select(k => '.').ToArray()).ToArray();
            var print = input;
            // find S
            int x = 0;
            int y = 0;
            bool ready = false;
            while (y < lines.Count && !ready)
            {
                while (x < lines[0].Length && !ready)
                {
                    ready = lines[y][x] == 'S';
                    if (!ready) x++;
                }
                if (!ready)
                {
                    x = 0;
                    y++;
                }
            }

            var start = (x, y);
            int direction = 0;
            int steps = 0;
            ready = false;
            input[y][x] = 'S';
            if (x < lines[0].Length - 1 && "-J7".Contains(lines[y][x + 1])) direction = 1;
            else if (y > 0 && "|7F".Contains(lines[y - 1][x])) direction = 2;
            else if (x > 0 && "-FL".Contains(lines[y][x - 1])) direction = 3;
            while (!ready)
            {
                char currentChar = '.';
                switch (direction)
                {
                    case 0:
                        currentChar = lines[y + 1][x];
                        if (currentChar == 'J') direction = 3;
                        else if (currentChar == 'L') direction = 1;
                        y++;
                        break;
                    case 1:
                        currentChar = lines[y][x + 1];
                        if (currentChar == 'J') direction = 2;
                        else if (currentChar == '7') direction = 0;
                        x++;
                        break;
                    case 2:
                        currentChar = lines[y - 1][x];
                        if (currentChar == 'F') direction = 1;
                        else if (currentChar == '7') direction = 3;
                        y--;
                        break;
                    case 3:
                        currentChar = lines[y][x - 1];
                        if (currentChar == 'F') direction = 0;
                        else if (currentChar == 'L') direction = 2;
                        x--;
                        break;
                    default: break;
                }
                input[y][x] = currentChar;
                ready = currentChar == 'S';

                steps++;

            }

            answer1 = (long)steps / 2;

            for (int i = 0; i < input.Length; i++)
            {
                bool inside = false;
                char online = '.';
                for (int j = 0; j < input[0].Length; j++)
                {
                    char currentCharIn = input[i][j];
                    if ("|JLF7".Contains(currentCharIn))
                    {
                        switch (currentCharIn)
                        {
                            case '|': inside = !inside; break;
                            case 'F': online = 'F'; break;
                            case 'L': online = 'L'; break;
                            case '7': if (online == 'L') inside = !inside; break;
                            case 'J': if (online == 'F') inside = !inside; break;
                            default: break;
                        }
                    }
                    else if (currentCharIn == '.')
                    {
                        if (inside) answer2++;
                    }

                    char replase = currentCharIn;
                    switch (currentCharIn)
                    {
                        case '|':
                            replase = '║';
                            break;
                        case '-':
                            replase = '═';
                            break;
                        case 'L':
                            replase = '╚';
                            break;
                        case 'J':
                            replase = '╝';
                            break;
                        case '7':
                            replase = '╗';
                            break;
                        case 'F':
                            replase = '╔';
                            break;
                    }
                    print[i][j] = replase;
                }
            }
            Console.WriteLine(answer1);
            Console.WriteLine(answer2);
            for (int i = 0; i < print.Length; i++)
            {
                string linePrint = string.Empty;
                for (int j = 0; j < print[i].Length; j++)
                {
                    if ("║═╚╝╗╔".Contains(print[i][j]))
                    {
                        linePrint = linePrint + print[i][j];
                        
                    }
                    else { linePrint = linePrint + " "; }
                }
                Console.WriteLine(linePrint);
            }
        }
    }
}