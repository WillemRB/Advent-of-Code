using System;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day15()
        {
            var turns = 2020;
            var game = "8,13,1,0,18,9".Split(',').Select(int.Parse).ToList();

            while (game.Count < turns)
            {
                var timesSpoken = game.Count(c => c == game.Last());

                if (timesSpoken == 1)
                    game.Add(0);
                else
                    game.Add(game.Count - (game.Take(game.Count - 1).ToList().LastIndexOf(game.Last()) + 1));
            }

            Console.WriteLine($"Answer at turn {turns}: {game.Last()}");
        }
    }
}
