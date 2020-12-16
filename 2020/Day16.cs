using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day16()
        {
            var lines = File.ReadAllLines("../../../input/day16.txt");

            var ticketRules = lines
                .TakeWhile(l => !string.IsNullOrEmpty(l))
                .Select(rule => new TicketRule(rule)).ToList();

            var myTicket = lines
                .Skip(ticketRules.Count() + 2)
                .First().Split(',').Select(int.Parse).ToArray();

            var tickets = lines
                .Skip(ticketRules.Count() + 5)
                .Select(t => t.Split(',').Select(int.Parse).ToArray())
                .ToList();

            ticketRules.ForEach(t => t.InitializeValidPosition(myTicket.Length));

            var errorRate = 0;
            foreach (var ticketFields in tickets)
            {
                var isValid = true;
                for (int i = 0; i < ticketFields.Count(); i++)
                {
                    isValid &= ticketRules.Any(rule => rule.IsValid(ticketFields[i]));
                    errorRate += !isValid ? ticketFields[i] : 0;
                    if (!isValid)
                        break;
                }

                if (!isValid)
                    continue;

                for (int i = 0; i < ticketFields.Count(); i++)
                {
                    ticketRules.ForEach(rule =>
                    {
                        if (!rule.IsValid(ticketFields[i]))
                            rule.ValidPositions.Remove(i);
                    });
                }
            }

            // Part A
            Console.WriteLine($"Ticket scanning error rate: {errorRate}");

            // Part B
            var result = 1L;
            var validPositions = new List<int>();
            for (int i = 1; i < ticketRules.Count; i++)
            {
                var rule = ticketRules.Single(r => r.ValidPositions.Count == i);

                var position = rule.ValidPositions.Except(validPositions).Single();
                if (rule.Name.StartsWith("departure"))
                    result *= myTicket[position];

                validPositions = rule.ValidPositions;
            }
            
            Console.WriteLine($"Departure fields: {result}");
        }

        class TicketRule
        {
            public string Name { get; private set; }
            public List<int> Range { get; } = new List<int>();
            public List<int> ValidPositions { get; private set; }

            public TicketRule(string rule)
            {
                Name = rule.Split(':')[0];

                foreach (Match match in Regex.Matches(rule, @"(\d+-\d+)"))
                {
                    var start = int.Parse(match.Value.Split('-')[0]);
                    var end = int.Parse(match.Value.Split('-')[1]);
                    Range.AddRange(Enumerable.Range(start, (end - start) + 1));
                }
            }

            public void InitializeValidPosition(int l) => ValidPositions = Enumerable.Range(0, l).ToList();

            public bool IsValid(int value) => Range.Contains(value);
        }
    }
}
