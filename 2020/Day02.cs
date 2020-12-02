using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day02()
        {
            var regex = new Regex(@"^(?<min>\d+)-(?<max>\d+) (?<char>.): (?<password>.+)$");

            var validPasswords = File.ReadAllLines("input/day02.txt")
                .Where(line =>
                {
                    var match = regex.Match(line);
                    var min = int.Parse(match.Groups["min"].Value);
                    var max = int.Parse(match.Groups["max"].Value);
                    var c = match.Groups["char"].Value.First();
                    var password = match.Groups["password"].Value;

                    // Part A
                    //var count = password.Count(p => p == c);
                    //return count >= min && count <= max;

                    // Part B
                    return password[min - 1] == c ^ password[max - 1] == c;
                }).Count();

            Console.WriteLine($"Valid passwords: {validPasswords}");
        }
    }
}
