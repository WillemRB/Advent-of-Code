using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day22()
        {
            var decks = File.ReadAllText("../../../input/day22.txt")
                .Split(Environment.NewLine + Environment.NewLine);

            var player1 = new Queue<long>();
            var player2 = new Queue<long>();

            foreach (var card in decks[0].Split(Environment.NewLine).Skip(1).Select(long.Parse))
                player1.Enqueue(card);

            foreach (var card in decks[1].Split(Environment.NewLine).Skip(1).Select(long.Parse))
                player2.Enqueue(card);

            var round = 0;
            while (player1.Any() && player2.Any())
            {
                var card1 = player1.Dequeue();
                var card2 = player2.Dequeue();

                if (card1 > card2)
                {
                    player1.Enqueue(card1);
                    player1.Enqueue(card2);
                }
                else
                {
                    player2.Enqueue(card2);
                    player2.Enqueue(card1);
                }

                round++;
            }

            var winner = player1.Any() ? player1 : player2;
            var score = 0L;
            while (winner.Any())
                score += winner.Count * winner.Dequeue();

            Console.WriteLine($"Winning score: {score} after {round} rounds");
        }
    }
}
