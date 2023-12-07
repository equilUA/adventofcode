using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace star6
{
    class Program
    {
        internal struct Number
        {
            public int Time { get; set; }
            public int Dist { get; set; }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Star6!");
            using (var reader = new StreamReader(@"C:\Stars\adventofcode\star6\input.txt"))
            {
                int result = Program.Part2(reader);
                Console.WriteLine(result);
            }
        }
        static int Part2(StreamReader reader)
        {
            string timestr = reader.ReadLine();
            var time = long.Parse(timestr.Split(':')[1].Replace(" ", ""));
            Console.WriteLine(time);
            string distanceStr = reader.ReadLine();
            var distance = long.Parse(distanceStr.Split(':')[1].Replace(" ", ""));
            Console.WriteLine(distance);

            var total = 0;
            for (var j = 0; j < time; ++j)
            {
                if (j * (time - j) > distance)
                {
                    ++total;
                }
            }

            return total;
        }
        static int Part1(StreamReader reader)
        {
            int total = 1;
            var numbers = new List<Number>();
            string time = reader.ReadLine();
            Console.WriteLine(time);
            var reveals = time.Split(": ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1];
            var numberRegex = new Regex(@"\d+");
            var inputTime = numberRegex.Matches(reveals).Select(match => match.Value).ToList();

            string distance = reader.ReadLine();
            Console.WriteLine(distance);
            var distanceStr = distance.Split(": ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1];

            var inputDistance = numberRegex.Matches(distanceStr).Select(match => match.Value).ToList();
            for (int i = 0; i < inputDistance.Count; i++)
            {
                Number num = new Number();
                num.Time = int.Parse(inputTime[i]);
                num.Dist = int.Parse(inputDistance[i]);
                numbers.Add(num);
                Console.WriteLine(inputDistance[i]);

            }
            for (int i = 0; i < numbers.Count; i++)
            {
                var win = 0;
                for (int j = 0; j < numbers[i].Time; j++)
                {
                    if ((numbers[i].Time - j) * j > numbers[i].Dist)
                        win++;

                }
                total *= win;
                Console.WriteLine(inputDistance[i]);

            }

            Console.WriteLine(total);
            return total;
        }
    }
}
