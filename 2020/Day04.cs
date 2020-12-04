using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day04()
        {
            var passportsData = File.ReadAllLines("../../../input/day04.txt");

            var passports = new List<Passport>();

            var current = new Passport();
            foreach (var line in passportsData)
            {
                if (string.IsNullOrEmpty(line))
                {
                    passports.Add(current);
                    current = new Passport();
                    continue;
                }

                current.Add(line);
            }

            passports.Add(current);

            // Part A
            Console.WriteLine($"Valid passports: {passports.Count(p => p.IsValid)}");
        }
    }

    class Passport
    {
        Dictionary<string, string> values = new Dictionary<string, string>();

        public void Add(string input)
        {
            input
                .Split(' ').ToList()
                .ForEach(s => values.Add(s.Split(':')[0], s.Split(':')[1]));
        }

        public bool IsValid
        {
            get
            {
                return new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" }.All(values.ContainsKey);
            }
        }
    }
}
