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

            // Part A
            //Console.WriteLine($"Bags that can contain a shiny gold: " +
            //    $"{rules.Where(b => b.Key != "shiny gold").Count(kv => ContainsShinyGold(kv.Value))}");

            // Part B
            Console.WriteLine($"Bags required inside a shiny gold: {BagCount(rules["shiny gold"])}");
        }

        static bool ContainsShinyGold(Bag bag)
        {
            return bag.Contains.Any(b => b.Color == "shiny gold") ||
                bag.Contains.Any(rule => ContainsShinyGold(rules[rule.Color]));
        }

        static int BagCount(Bag bag)
        {
            return (bag.Color == "shiny gold" ? 0 : 1) +
                bag.Contains.Sum(b => b.Quantity * BagCount(rules[b.Color]));
        }
    }

    class Bag
    {
        public string Color { get; set; }
        public int Quantity { get; set; } = 1;
        public List<Bag> Contains { get; } = new List<Bag>();

        public Bag() { }

        public Bag(string input)
        {
            var split = input.Split(' ');

            Color = string.Join(" ", split.Take(2));

            if (split[4] == "no")
                return;

            for (int i = 4; i < split.Length; i += 4)
            {
                Contains.Add(new Bag { 
                    Quantity = int.Parse(split[i]), 
                    Color = string.Join(" ", split.Skip(i + 1).Take(2))
                });
            }
        }
    }
}
