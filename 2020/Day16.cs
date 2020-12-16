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
                .Select(r =>
                {
                    var range = new List<int>();
                    foreach (Match match in Regex.Matches(r, @"(\d+-\d+)"))
                    {
                        var start = int.Parse(match.Value.Split('-')[0]);
                        var end = int.Parse(match.Value.Split('-')[1]);
                        range.AddRange(Enumerable.Range(start, (end - start) + 1));
                    }
                    return range;
                }).ToList();

            var myTicket = lines
                .Skip(ticketRules.Count() + 2)
                .First().Split(',').Select(int.Parse).ToArray();

            var tickets = lines
                .Skip(ticketRules.Count() + 5)
                .Select(t => t.Split(',').Select(int.Parse).ToArray());

            var errorRate = 0;
            foreach (var ticket in tickets)
            {
                for(int i = 0; i < ticket.Count(); i++)
                {
                    if (ticketRules.Any(r => r.Contains(ticket[i])))
                        continue;

                    errorRate += ticket[i];
                }
            }

            Console.WriteLine($"Ticket scanning error rate: {errorRate}");
        }
    }
}
