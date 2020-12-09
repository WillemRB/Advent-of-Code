using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day06()
        {
            var groups = File.ReadAllText("../../../input/day06.txt")
                .Split("\r\n\r\n")
                .Select(g => new Group(g.Split("\r\n")));

            // Part A
            //Console.WriteLine($"Sum of 'yes' answers: {groups.Sum(g => g.AnyYesAnswers)}");

            // Part B
            Console.WriteLine($"Sum of 'yes' answers: {groups.Sum(g => g.AllYesAnswers)}");
        }
    }

    class Group
    {
        Dictionary<char, int> answers = new Dictionary<char, int>();

        int groupSize = 0;

        public Group(string[] input)
        {
            groupSize = input.Length;
            foreach (char a in string.Join("", input))
            {
                if (!answers.TryAdd(a, 1))
                    answers[a]++;
            }
        }

        public int AnyYesAnswers => answers.Count;

        public int AllYesAnswers => answers.Count(kv => kv.Value == groupSize);
    }
}
