using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace star5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("STAR5");
            using (var reader = new StreamReader(@"C:\Stars\adventofcode\star5\input.txt"))
            {
                //long result = Program.Part1(reader);
               // Console.WriteLine(result);
                long result2 = Program.Part2(reader);
                Console.WriteLine(result2);
            }

        }

        static long Part1(StreamReader reader)
        {

            Console.WriteLine("STAR5.1");

            string seedsLine = reader.ReadLine();
            Console.WriteLine(seedsLine);
            var reveals = seedsLine.Split(": ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1];
            var numberRegex = new Regex(@"\d+");
            var inputSeeds = numberRegex.Matches(reveals).Select(match => match.Value);
            List<long> seeds = new List<long>();
            foreach (var seed in inputSeeds)
            {
                seeds.Add(long.Parse(seed));
            }

            var seedToSoilMap = Program.GetMap(reader, "seed-to-soil map:");
            var soilToFertilizerMap = Program.GetMap(reader, "soil-to-fertilizer map:");
            var fertilizerToWaterMap = Program.GetMap(reader, "fertilizer-to-water map:");
            var waterToLightMap = Program.GetMap(reader, "water-to-light map:");
            var lightToTemperatureMap = Program.GetMap(reader, "light-to-temperature map:");
            var temperatureToHumidityMap = Program.GetMap(reader, "temperature-to-humidity map:");
            var humidityToLocationMap = Program.GetMap(reader, "humidity-to-location map:");

            long lowestLocation = long.MaxValue;

            foreach (long seed in seeds)
            {
                long tempSeed = seed;
                tempSeed = Program.CalculateNextSource(tempSeed, seedToSoilMap);
                tempSeed = Program.CalculateNextSource(tempSeed, soilToFertilizerMap);
                tempSeed = Program.CalculateNextSource(tempSeed, fertilizerToWaterMap);
                tempSeed = Program.CalculateNextSource(tempSeed, waterToLightMap);
                tempSeed = Program.CalculateNextSource(tempSeed, lightToTemperatureMap);
                tempSeed = Program.CalculateNextSource(tempSeed, temperatureToHumidityMap);
                tempSeed = Program.CalculateNextSource(tempSeed, humidityToLocationMap);
                lowestLocation = Math.Min(lowestLocation, tempSeed);
            }

            return lowestLocation;
        }

        static long Part2(StreamReader reader)
        {
            Console.WriteLine("STAR5.2");

            var seedsLine = reader.ReadLine();

            List<Tuple<long, long>> seedRanges = new List<Tuple<long, long>>();
            Console.WriteLine(seedsLine);
            var reveals = seedsLine.Split(": ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1];
            var numberRegex = new Regex(@"\d+");
            var inputSeeds = numberRegex.Matches(reveals).Select(match => match.Value).ToList();

            for (int i = 0; i < inputSeeds.Count; i += 2)
            {
                if (i + 1 < inputSeeds.Count)
                {
                    seedRanges.Add(Tuple.Create(long.Parse(inputSeeds[i]), long.Parse((inputSeeds[i + 1]))));
                    Console.WriteLine(inputSeeds[i] + "-" + inputSeeds[i + 1]);
                }
            }

            var seedToSoilMap = Program.GetMap(reader, "seed-to-soil map:");
            var soilToFertilizerMap = Program.GetMap(reader, "soil-to-fertilizer map:");
            var fertilizerToWaterMap = Program.GetMap(reader, "fertilizer-to-water map:");
            var waterToLightMap = Program.GetMap(reader, "water-to-light map:");
            var lightToTemperatureMap = Program.GetMap(reader, "light-to-temperature map:");
            var temperatureToHumidityMap = Program.GetMap(reader, "temperature-to-humidity map:");
            var humidityToLocationMap = Program.GetMap(reader, "humidity-to-location map:");

            long lowestLocation = long.MaxValue;

            foreach (var seedRange in seedRanges)
            {
                for (long i = 0; i < seedRange.Item2; i++)
                {
                    long tempSeed = seedRange.Item1 + i;
                    tempSeed = Program.CalculateNextSource(tempSeed, seedToSoilMap);
                    tempSeed = Program.CalculateNextSource(tempSeed, soilToFertilizerMap);
                    tempSeed = Program.CalculateNextSource(tempSeed, fertilizerToWaterMap);
                    tempSeed = Program.CalculateNextSource(tempSeed, waterToLightMap);
                    tempSeed = Program.CalculateNextSource(tempSeed, lightToTemperatureMap);
                    tempSeed = Program.CalculateNextSource(tempSeed, temperatureToHumidityMap);
                    tempSeed = Program.CalculateNextSource(tempSeed, humidityToLocationMap);
                    lowestLocation = Math.Min(lowestLocation, tempSeed);
                }
            }

            return lowestLocation;
        }

        static long CalculateNextSource(long seed, List<Tuple<long, long, long>> map)
        {
            foreach (var mapEntry in map)
            {
                if (seed >= mapEntry.Item2 && seed < mapEntry.Item2 + mapEntry.Item3)
                {
                    return mapEntry.Item1 + (seed - mapEntry.Item2);
                }
            }
            return seed;
        }

        static List<Tuple<long, long, long>> GetMap(StreamReader reader, string mapName)
        {
            List<Tuple<long, long, long>> map = new List<Tuple<long, long, long>>();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith(mapName))
                {
                    break;
                }
            }

            while ((line = reader.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
            {
                var parts = line.Split(' ');
                map.Add(Tuple.Create(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2])));
            }

            return map;
        }

    }
}
