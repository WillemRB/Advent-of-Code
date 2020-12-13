using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day13()
        {
            var lines = File.ReadAllLines("../../../input/day13.txt");

            // Part A
            var earliest = int.Parse(lines[0]);
            var buses = lines[1].Split(',')
                .Where(b => b != "x")
                .Select(b => (Bus: int.Parse(b), Departure: int.Parse(b) - (earliest % int.Parse(b))))
                .ToList();

            var firstBus = buses.OrderBy(b => b.Departure).First();
            Console.WriteLine($"First bus: {firstBus.Bus * firstBus.Departure}");
        }
    }
}
