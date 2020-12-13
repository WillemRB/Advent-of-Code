using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day13()
        {
            var lines = File.ReadAllLines("../../../input/day13.txt");
            var data = lines[1].Split(',');

            var earliest = int.Parse(lines[0]);
            var buses = new List<(int Index, int Bus, int Departure)>();

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == "x")
                    continue;
                buses.Add((i, int.Parse(data[i]), int.Parse(data[i]) - (earliest % int.Parse(data[i]))));
            }

            //Part A
            //var firstBus = buses.OrderBy(b => b.Departure).First();
            //Console.WriteLine($"First bus: {firstBus.Bus * firstBus.Departure}");

            // Part B
            var timestamp = 1L;
            var step = 1L;

            buses.ForEach(b =>
            {
                while ((timestamp + b.Index) % b.Bus != 0)
                    timestamp += step;
                step *= b.Bus;
            });

            Console.WriteLine($"Timestamp: {timestamp}");
        }
    }
}
