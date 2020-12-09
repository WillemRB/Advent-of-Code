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
                .ToArray();

            for (int i = preambleSize; i < xmas.Length; i++)
            {
                var preamble = xmas.Skip(i - preambleSize).Take(preambleSize);

                if (!preamble.Any(l => preamble.Contains(xmas[i] - l)))
                {
                    // Part A
                    //Console.WriteLine($"Invalid value: {xmas[i]}");

                    // Part B
                    for (int j = 0; j < i; j++)
                    {
                        for (int k = j + 1; k < i; k++)
                        {
                            if (xmas[j..k].Sum() == xmas[i])
                                Console.WriteLine($"Encryption weakness: {xmas[j..k].Min() + xmas[j..k].Max()}");
                        }
                    }
                }
            }
        }
    }
}
