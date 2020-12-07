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
            var answers = File.ReadAllLines("../../../input/day06.txt");

            var groups = new List<Group>();

            var current = new Group();
            foreach (var line in answers)
            {
                if (string.IsNullOrEmpty(line))
                {
                    groups.Add(current);
                    current = new Group();
                    continue;
                }

                current.AddAnswers(line);
            }
            groups.Add(current);

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

        public void AddAnswers(string input)
        {
            foreach (char a in input)
            {
                if (!answers.TryAdd(a, 1))
                    answers[a]++;
            }

            groupSize++;
        }

        public int AnyYesAnswers => answers.Count;

        public int AllYesAnswers => answers.Count(kv => kv.Value == groupSize);
    }
}
