using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day12()
        {
            var actions = File.ReadAllLines("../../../input/day12.txt")
                .Select(l => (l[0], int.Parse(l[1..])))
                .ToList();

            var ferry = new Ferry();
            actions.ForEach(a => ferry.Move(a.Item1, a.Item2));

            Console.WriteLine($"Distance: {ferry.ManhattanDistance}");
        }
    }

    class Ferry
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int ManhattanDistance => Math.Abs(X) + Math.Abs(Y);
        public char Direction { get; set; } = 'E';

        public void Move(char direction, int distance)
        {
            if (direction == 'R' || direction == 'L')
            {
                var dirs = new List<char> { 'N', 'E', 'S', 'W' };

                if (direction == 'L')
                    dirs.Reverse();

                Direction = dirs[(dirs.FindIndex(c => c == Direction) + (distance / 90)) % dirs.Count];
                
                return;
            }

            if (direction == 'F')
                direction = Direction;

            switch (direction)
            {
                case 'N':
                    Y += distance;
                    break;
                case 'E':
                    X += distance;
                    break;
                case 'S':
                    Y -= distance;
                    break;
                case 'W':
                    X -= distance;
                    break;
            }
        }
    }
}
