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
            var data = File.ReadAllLines("../../../input/day11a.txt");
            rowLength = data[0].Length + 2;

            layout = data
                .Select(l => $".{l}.".ToCharArray()).ToList()
                .Prepend(new String('.', rowLength).ToCharArray())
                .Append(new String('.', rowLength).ToCharArray())
                .ToList();

            bool changed;
            do
            {
                var newLayout = new List<char[]>();
                changed = false;

                newLayout.Add(new String('.', layout[0].Length).ToCharArray());

                for (int x = 1; x < layout.Count - 1; x++)
                {
                    var row = string.Empty;

                    for (int y = 1; y < layout[0].Length - 1; y++)
                    {
                        if (layout[x][y] == '.')
                        {
                            row += ".";
                            continue;
                        }

                        /* Part A
                        var adjacent = new List<char> {
                            layout[x-1][y-1], layout[x-1][y], layout[x-1][y+1],
                            layout[x][y-1], layout[x][y+1],
                            layout[x+1][y-1], layout[x+1][y], layout[x+1][y+1],
                        };

                        if (adjacent.Count(s => s == '#') == 0)
                            row += "#";
                        else if (adjacent.Count(s => s == '#') >= 4)
                            row += "L";
                        else
                            row += layout[x][y];
                        //*/

                        //* Part B
                        var sight = new List<char> {
                            GetSeat(x, y, -1, -1), GetSeat(x, y, -1, 0), GetSeat(x, y, -1, +1),
                            GetSeat(x, y, 0, -1), GetSeat(x, y, 0, +1),
                            GetSeat(x, y, +1, -1), GetSeat(x, y, +1, 0), GetSeat(x, y, +1, +1),
                        };

                        if (sight.Count(s => s == '#') == 0)
                            row += "#";
                        else if (sight.Count(s => s == '#') >= 5)
                            row += "L";
                        else
                            row += layout[x][y];
                        //*/

                        changed |= layout[x][y] != row.Last();
                    }

                    newLayout.Add($".{row}.".ToCharArray());
                }

                newLayout.Add(new String('.', layout[0].Length).ToCharArray());

                layout = newLayout;
            }
            while (changed);

            Console.WriteLine($"Occupied seats: {layout.Sum(row => row.Count(seat => seat == '#'))}");
        }

        static char GetSeat(int x, int y, int dx, int dy)
        {
            while (true)
            {
                x += dx;
                y += dy;

                if (x < 0 || y < 0 || x >= layout.Count || y >= rowLength)
                    return '.';

                if (layout[x][y] != '.')
                    return layout[x][y];
            }
        }
    }
}
