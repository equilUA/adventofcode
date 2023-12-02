using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Star2
{
    class Program
    {
        private static int red = 12, blue = 14, green = 13;

        static void Main(string[] args)
        {
            Console.WriteLine("STAR2");
            string[] lines = File.ReadAllLines(@"C:\Stars\adventofcode\star2\Star2\input.txt");

            int total = 0;

            //Game 1: 3 blue, 4 red; 1 red
            foreach (string line in lines)
            {
                Console.WriteLine(line);
                int gameID = CheckGame2(line);
                Console.WriteLine(gameID);
                total = total + gameID;
            }
            Console.WriteLine(total);
        }

        // check if game can exist
        static private int CheckGame(string line)
        {
            var reveals = line.Split(": ")[1].Split("; ");
            foreach (var reveal in reveals)
            {
                var colorCounts = reveal.Split(", ");
                foreach (var colorCount in colorCounts)
                {
                    var split = colorCount.Split(' ');
                    switch (split[1])
                    {
                        case "red":
                            if (int.Parse(split[0]) > red)
                            {
                                return 0;
                            }
                            break;

                        case "blue":
                            if (int.Parse(split[0]) > blue)
                            {
                                return 0;
                            }
                            break;

                        case "green":
                            if (int.Parse(split[0]) > green)
                            {
                                return 0;
                            }
                            break;

                        default:
                            return 0;
                    }

                }
            }
            return int.Parse(line.Split(": ")[0].Split(' ')[1]);
        }

        // game 2
        static private int CheckGame2(string line)
        {
            int countRed = 0, countBlue = 0, countGreen = 0;
            var reveals = line.Split(": ")[1].Split("; ");
            foreach (var reveal in reveals)
            {
                var colorCounts = reveal.Split(", ");
                foreach (var colorCount in colorCounts)
                {
                    var split = colorCount.Split(' ');
                    switch (split[1])
                    {
                        case "red":
                            countRed = Math.Max(countRed, int.Parse(split[0]));
                            break;

                        case "blue":
                            countBlue = Math.Max(countBlue, int.Parse(split[0]));
                            break;

                        case "green":
                            countGreen = Math.Max(countGreen, int.Parse(split[0]));
                            break;

                    }

                }
            }
            return countRed * countBlue * countGreen;

        }
    }
}
