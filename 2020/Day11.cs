using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static List<char[]> layout;
        static int rowLength;

        static void Day11()
        {
            var data = File.ReadAllLines("../../../input/day11.txt");
            rowLength = data[0].Length;

            layout = data.Select(l => l.ToCharArray()).ToList();

            // Part A
            //int limit = 1;
            //var occupiedLimit = 4;

            // Part B
            int limit = int.MaxValue;
            var occupiedLimit = 5;

            bool changed;
            do
            {
                var newLayout = new List<char[]>();
                changed = false;

                for (int x = 0; x < layout.Count; x++)
                {
                    var row = string.Empty;

                    for (int y = 0; y < layout[0].Length; y++)
                    {
                        if (layout[x][y] == '.')
                        {
                            row += ".";
                            continue;
                        }

                        var sight = new List<char> {
                            GetSeat(x, y, -1, -1, limit), GetSeat(x, y, -1, 0, limit), GetSeat(x, y, -1, +1, limit),
                            GetSeat(x, y, 0, -1, limit), GetSeat(x, y, 0, +1, limit),
                            GetSeat(x, y, +1, -1, limit), GetSeat(x, y, +1, 0, limit), GetSeat(x, y, +1, +1, limit),
                        };

                        if (sight.Count(s => s == '#') == 0)
                            row += "#";
                        else if (sight.Count(s => s == '#') >= occupiedLimit)
                            row += "L";
                        else
                            row += layout[x][y];

                        changed |= layout[x][y] != row.Last();
                    }

                    newLayout.Add(row.ToCharArray());
                }

                layout = newLayout;
            }
            while (changed);

            Console.WriteLine($"Occupied seats: {layout.Sum(row => row.Count(seat => seat == '#'))}");
        }

        static char GetSeat(int x, int y, int dx, int dy, int limit)
        {
            do
            {
                x += dx;
                y += dy;

                if (x < 0 || y < 0 || x >= layout.Count || y >= rowLength)
                    return '.';

                if (layout[x][y] != '.')
                    break;

                limit--;
            }
            while (limit > 0);

            return layout[x][y];
        }
    }
}
