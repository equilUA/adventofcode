using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Star1
{
    class Program
    {
        private static String[] units = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten"};

        static void Main(string[] args)
        {
            Console.WriteLine("STAR1");
            string[] lines = File.ReadAllLines(@"C:\Stars\star1\Star1\Star1\input2.txt");
            int total = 0;
            foreach (string line in lines)
            {
                int first = 0;
                bool firstBool = true;
                int last = 0;
                Console.WriteLine(line);
                string newline = line.Replace("twone", "twoone").Replace("eighthree", "eightthree").Replace("sevenine", "sevennine").Replace("oneight", "oneeight").Replace("threeight", "threeeight").Replace("fiveight", "fiveeight").Replace("nineight", "nineeight").Replace("eightwo", "eighttwo");

                string result = newline.Replace(units[0], "0").Replace(units[1], "1").Replace(units[2], "2").Replace(units[3], "3").Replace(units[4], "4").Replace(units[5], "5").Replace(units[6], "6").Replace(units[7], "7").Replace(units[8], "8").Replace(units[9], "9");
                Console.WriteLine(result);
                foreach (char c in result)
                {
                    if (Char.IsNumber(c))
                    {
                        if (firstBool)
                        {
                            first = c - '0';
                            last = first;
                            firstBool = false;
                        }
                        else
                        {
                            last = c - '0';
                        }
                    }
                }
                int lineNum = first * 10 + last;
                Console.WriteLine(lineNum);
                total = total + lineNum;
            }
            Console.WriteLine(total);
        }


    }
}
