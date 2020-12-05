using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day05()
        {
            var boardingPasses = File.ReadAllLines("../../../input/day05.txt").Select(l => new Seat(l));

            // Part A
            Console.WriteLine($"Hightest seat ID: {boardingPasses.Max(b => b.Id)}");
        }
    }

    class Seat
    {
        string _partition;

        public int Id => (Row * 8) + Column;

        public int Row => Convert.ToInt32(string.Join("", _partition.Take(7).Select(c => c == 'B' ? '1' : '0')), fromBase: 2);

        public int Column => Convert.ToInt32(string.Join("", _partition.Skip(7).Take(3).Select(c => c == 'R' ? '1' : '0')), fromBase: 2);

        public Seat(string partition)
        {
            _partition = partition;
        }
    }
}
