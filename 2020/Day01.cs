using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day01()
        {
            var expenses = File.ReadAllLines("input/day01.txt").Select(int.Parse).ToList();

            for (int i = 0; i < expenses.Count - 1; i++)
            {
                for (int j = i + 1; j < expenses.Count; j++)
                {
                    if ((expenses[i] + expenses[j]) == 2020)
                        Console.WriteLine($"{expenses[i] * expenses[j]}");
                }
            }
        }
    }
}
