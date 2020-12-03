using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day03()
        {
            var area = File.ReadAllLines("../../../input/day03.txt").ToList();

            int x = 0, trees = 0;
            area.ForEach(l =>
            {
                trees += l[x % l.Length] == '#' ? 1 : 0;
                x += 3;
            });

            Console.WriteLine($"Trees encountered: {trees}");
        }
    }
}
