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
            var passports = File.ReadAllText("../../../input/day04.txt")
                .Split("\r\n\r\n")
                .Select(p => new Passport(p.Replace("\r\n", " ")));

            // Part A
            //Console.WriteLine($"Valid passports: {passports.Count(p => p.HasRequiredFields)}");

            // Part B
            Console.WriteLine($"Valid passports: {passports.Count(p => p.IsValid())}");
        }
    }

    class Passport
    {
        Dictionary<string, string> fields = new Dictionary<string, string>();

        public Passport(string data)
        {
            data
                .Split(' ').ToList()
                .ForEach(s => fields.Add(s.Split(':')[0], s.Split(':')[1]));
        }

        public bool HasRequiredFields => new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" }.All(fields.ContainsKey);

        public bool IsValid()
        {
            if (!HasRequiredFields)
                return false;

            var valid = true;
            valid &= Regex.IsMatch(fields["byr"], @"^(19[2-9]\d|200[0-2])$");
            valid &= Regex.IsMatch(fields["iyr"], @"^20(1\d|20)$");
            valid &= Regex.IsMatch(fields["eyr"], @"^20(2\d|30)$");
            valid &= Regex.IsMatch(fields["hgt"], @"^(\d{3}cm|\d{2}in)$");
            valid &= Regex.IsMatch(fields["hcl"], @"^#[0-9a-f]{6}$");
            valid &= new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(fields["ecl"]);
            valid &= Regex.IsMatch(fields["pid"], @"^\d{9}$");

            return valid;
        }
    }
}
