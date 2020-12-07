using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static Dictionary<string, Bag> rules;

        static void Day07()
        {
            rules = File.ReadAllLines("../../../input/day07.txt")
                .Select(r => new Bag(r))
                .ToDictionary(b => b.Color);

            Console.WriteLine($"Bags that can contain a shiny gold: " +
                $"{rules.Where(b => b.Key != "shiny gold").Count(kv => ContainsShinyGold(kv.Value))}");
        }

        static bool ContainsShinyGold(Bag bag)
        {
            if (bag.Contains.Any(b => b.Color == "shiny gold"))
                return true;

            return bag.Contains.Any(rule => ContainsShinyGold(rules[rule.Color]));
        }
    }

    class Bag
    {
        public string Color { get; }

        public int Quantity { get; }

        public List<Bag> Contains { get; } = new List<Bag>();

        public Bag(string input)
        {
            var split = input.Split(' ');

            Color = string.Join(" ", split.Take(2));

            if (split[4] == "no")
                return;

            for (int i = 4; i < split.Length; i += 4)
            {
                Contains.Add(new Bag(int.Parse(split[i]), string.Join(" ", split.Skip(i + 1).Take(2))));
            }
        }

        public Bag(int quantity, string color)
        {
            Color = color;
            Quantity = quantity;
        }
    }
}
