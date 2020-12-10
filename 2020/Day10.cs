using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day10()
        {
            var jolts = File.ReadAllLines("../../../input/day10.txt").Select(int.Parse).ToList();
            
            jolts.Sort();

            jolts = jolts.Prepend(0).Append(jolts.Last() + 3).ToList();

            int jump1 = 0, jump3 = 0;
            for (int i = 1; i < jolts.Count; i++)
            {
                jump1 += (jolts[i] - jolts[i - 1] == 1) ? 1 : 0;
                jump3 += (jolts[i] - jolts[i - 1] == 3) ? 1 : 0;
            }

            Console.WriteLine($"Jolt differences: {jump1 * jump3}");
        }
    }
}
