using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day15()
        {
            // Part A
            var resultA = PlayMemoryGame(2020, "8,13,1,0,18,9");
            Console.WriteLine($"Answer at turn {resultA.turn}: {resultA.value}");

            // Part A
            var resultB = PlayMemoryGame(30_000_000, "8,13,1,0,18,9");
            Console.WriteLine($"Answer at turn {resultB.turn}: {resultB.value}");
        }

        static (int turn, int value) PlayMemoryGame(int turns, string start)
        {
            var game = start.Split(',').Select(int.Parse).ToList();
            var memory = new Dictionary<int, List<int>>();
            int value = default(int);

            for (int turn = 1; turn <= turns; turn++)
            {
                if ((turn - 1) < game.Count)
                {
                    value = game[turn - 1];
                    
                    memory.Add(value, new List<int>());
                    memory[value].Insert(0, turn);
                }
                else
                {
                    value = memory[value].Count == 1 ? 0 : memory[value][0] - memory[value][1];

                    if (!memory.ContainsKey(value))
                        memory.Add(value, new List<int>());

                    memory[value].Insert(0, turn);

                    if (memory[value].Count > 2)
                        memory[value].RemoveAt(2);
                }
            }

            return (turns, value);
        }
    }
}
