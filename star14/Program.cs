using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace star14
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Star14");
       
            var input = File.ReadAllLines(@"C:\Stars\adventofcode\star14\input.txt");
            int size = input.Length;
            char[,] map = new char[size, size];

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    map[r, c] = input[r][c];
                }
            }
            North(map, size);
            int result1 = CalculateCount(map, size);
            Console.WriteLine(result1);
            // okay, lets run through a bunch of cycles!
            int round = 0;
            var loads = new List<int>();
            var hashes = new List<int>();
            do
            {
                Cycle(map, size);
                int hash = MapHash(map, size);
                hashes.Add(hash);
                int load = CalculateCount(map, size);
                loads.Add(load);

                ++round;
            }
            while (round < 1100);

            // alright, let's find a cycle in the hashes!
            int tortoise = 0, rabbit;
            do
            {
                for (rabbit = tortoise + 1; rabbit < hashes.Count; rabbit++)
                {
                    if (hashes[tortoise] == hashes[rabbit] && IsCycle(hashes, tortoise, rabbit))
                    {
                        goto done;
                    }
                }
                ++tortoise;
            }
            while (true);
        done:

            int cycleLen = rabbit - tortoise;
            int offset = (1_000_000_000 - rabbit) % cycleLen - 1;
            Console.WriteLine(loads[tortoise + offset]);
        }

        static void Cycle(char[,] map, int size)
        {
            North(map, size);
            West(map, size);
            South(map, size);
            East(map, size);
        }
        static bool IsCycle(List<int> hashes, int a, int b)
        {
            int c = b - a;

            for (int j = 0; j < c; j++)
            {
                if (hashes[a + j] != hashes[b + j])
                    return false;
            }

            return true;
        }
        static int MapHash(char[,] map, int size)
        {
            var sb = new StringBuilder();
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    sb.Append(map[r, c]);
                }
            }

            return sb.ToString().GetHashCode();
        }
        static int CalculateCount(char[,] map, int size)
        {
            int load = 0;
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    if (map[r, c] == 'O')
                        load += size - r;
                }
            }

            return load;
        }
        static void West(char[,] map, int size)
        {
            for (int r = 0; r < size; r++)
            {
                for (int c = 1; c < size; c++)
                {
                    if (map[r, c] == 'O')
                    {
                        int nc = c;
                        while (nc - 1 >= 0 && map[r, nc - 1] == '.')
                        {
                            map[r, nc - 1] = 'O';
                            map[r, nc] = '.';
                            --nc;
                        }
                    }
                }
            }
        }

        static void East(char[,] map, int size)
        {
            for (int r = 0; r < size; r++)
            {
                for (int c = size - 1; c >= 0; c--)
                {
                    if (map[r, c] == 'O')
                    {
                        int nc = c;
                        while (nc + 1 < size && map[r, nc + 1] == '.')
                        {
                            map[r, nc + 1] = 'O';
                            map[r, nc] = '.';
                            ++nc;
                        }
                    }
                }
            }
        }
        static void South(char[,] map, int size)
        {
            for (int r = size - 1; r >= 0; r--)
            {
                for (int c = 0; c < size; c++)
                {
                    if (map[r, c] == 'O')
                    {
                        int nr = r;
                        while (nr + 1 < size && map[nr + 1, c] == '.')
                        {
                            map[nr + 1, c] = 'O';
                            map[nr, c] = '.';
                            ++nr;
                        }
                    }
                }
            }
        }
        static void North(char[,] map, int size)
        {
            for (int r = 1; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    if (map[r, c] == 'O')
                    {
                        int nr = r;
                        while (nr - 1 >= 0 && map[nr - 1, c] == '.')
                        {
                            map[nr - 1, c] = 'O';
                            map[nr, c] = '.';
                            --nr;
                        }
                    }
                }
            }
        }
    }

}
