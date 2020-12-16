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

            var myTicket = new Ticket(lines
                .Skip(ticketRules.Count() + 2)
                .First());

            var tickets = lines
                .Skip(ticketRules.Count() + 5)
                .Select(t => new Ticket(t))
                .ToList();

            var errorRate = 0;
            foreach (var ticket in tickets)
            {
                for(int i = 0; i < ticket.Fields.Count(); i++)
                {
                    if (ticketRules.Any(rule => rule.IsValid(ticket.Fields[i])))
                        continue;

                    errorRate += ticket.Fields[i];
                }
            }

            Console.WriteLine($"Ticket scanning error rate: {errorRate}");
        }

        class Ticket
        {
            public int[] Fields { get; set; }

            public Ticket(string fields)
            {
                Fields = fields.Split(',').Select(int.Parse).ToArray();
            }
        }

        class TicketRule
        {
            public string Name { get; private set; }
            public List<int> Range { get; } = new List<int>();

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

            public bool IsValid(int value) => Range.Contains(value);
        }
    }
}
