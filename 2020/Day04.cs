using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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
            //Console.WriteLine($"Valid passports: {passports.Count(p => p.HasRequiredFields)}");

            // Part B
            Console.WriteLine($"Valid passports: {passports.Count(p => p.IsValid())}");
        }
    }

    class Passport
    {
        Dictionary<string, string> fields = new Dictionary<string, string>();

        public void Add(string input)
        {
            input
                .Split(' ').ToList()
                .ForEach(s => fields.Add(s.Split(':')[0], s.Split(':')[1]));
        }

        public bool HasRequiredFields => new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" }.All(fields.ContainsKey);

        public bool IsValid()
        {
            if (!HasRequiredFields)
                return false;

            var valid = true;
            valid &= int.Parse(fields["byr"]) >= 1920 && int.Parse(fields["byr"]) <= 2002;
            valid &= int.Parse(fields["iyr"]) >= 2010 && int.Parse(fields["iyr"]) <= 2020;
            valid &= int.Parse(fields["eyr"]) >= 2020 && int.Parse(fields["eyr"]) <= 2030;
            valid &= Regex.IsMatch(fields["hgt"], @"^(\d{3}cm|\d{2}in)$");
            valid &= Regex.IsMatch(fields["hcl"], @"^#[0-9a-f]{6}$");
            valid &= new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(fields["ecl"]);
            valid &= Regex.IsMatch(fields["pid"], @"^\d{9}$");

            return valid;
        }
    }
}
