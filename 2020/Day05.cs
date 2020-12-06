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
            //Console.WriteLine($"Hightest seat ID: {boardingPasses.Max(b => b.Id)}");

            // Part B
            var seatId = Enumerable
                .Range(boardingPasses.Min(b => b.Id), boardingPasses.Max(b => b.Id))
                .First(seatId => !boardingPasses.Any(p => p.Id == seatId));

            Console.WriteLine($"ID of seat: {seatId}");
        }
    }

    class Seat
    {
        readonly string _partition;

        public int Id => (Row * 8) + Column;

        public int Row => Convert.ToInt32(_partition[0..7].Replace('B', '1').Replace('F', '0'), fromBase: 2);

        public int Column => Convert.ToInt32(_partition[8..].Replace('R', '1').Replace('L', '0'), fromBase: 2);

        public Seat(string partition)
        {
            _partition = partition;
        }
    }
}
