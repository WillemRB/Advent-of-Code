using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static readonly int preambleSize = 25;

        static void Day09()
        {
            var xmas = File.ReadAllLines("../../../input/day09.txt")
                .Select(long.Parse)
                .ToList();

            // Part A
            for (int i = preambleSize; i < xmas.Count; i++)
            {
                var preamble = xmas.Skip(i - preambleSize).Take(preambleSize);

                if (!preamble.Any(l => preamble.Contains(xmas[i] - l)))
                    Console.WriteLine($"Invalid value: {xmas[i]}");
            }
        }
    }
}
