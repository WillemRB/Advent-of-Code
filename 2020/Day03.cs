using System;
using System.IO;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day03()
        {
            var area = File.ReadAllLines("../../../input/day03.txt");

            // Part A
            //Console.WriteLine($"Trees encountered: {TreesEncountered(area, 3)}");

            // Part B
            var trees = TreesEncountered(area, 1) * TreesEncountered(area, 3) * TreesEncountered(area, 5) * TreesEncountered(area, 7) * TreesEncountered(area, 1, 2);
            Console.WriteLine($"Trees encountered: {trees}");
        }

        static long TreesEncountered(string[] area, int slopeX, int slopeY = 1)
        {
            int x = 0, y = 0, trees = 0;
            while (y < area.Length)
            {
                trees += area[y][x % area[y].Length] == '#' ? 1 : 0;
                x += slopeX;
                y += slopeY;
            }

            return trees;
        }
    }
}
