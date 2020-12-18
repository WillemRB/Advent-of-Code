using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day18()
        {
            var equations = File.ReadAllLines("../../../input/day18.txt");

            var sum = equations.Sum(e => ProcessEquation(e));
            Console.WriteLine($"Sum of all equations: {sum}");
        }

        static long Apply(List<MathSymbol> symbols)
        {
            // Part A
            //HandleOperators(symbols, "+", "*");

            // Part B
            HandleOperators(symbols, "+");
            HandleOperators(symbols, "*");

            return (long)symbols.First().Value;
        }

        static void HandleOperators(List<MathSymbol> symbols, params string[] ops)
        {
            while (symbols.Any(s => ops.Contains(s.Operation)))
            {
                var index = symbols.FindIndex(s => ops.Contains(s.Operation));

                var result = new MathSymbol();

                if (symbols[index].Operation == "+")
                    result.Value = symbols[index - 1].Value + symbols[index + 1].Value;
                else
                    result.Value = symbols[index - 1].Value * symbols[index + 1].Value;

                symbols.RemoveRange(index - 1, 3);
                symbols.Insert(index - 1, result);
            }
        }

        static List<MathSymbol> GetSymbols(string equation)
        {
            return Regex.Matches(equation, @"(\d+|[+*])")
                .Select(m => Regex.IsMatch(m.Value, @"[+*]") ? new MathSymbol { Operation = m.Value } : new MathSymbol { Value = long.Parse(m.Value) })
                .ToList();
        }

        static long ProcessEquation(string equation)
        {
            while (equation.Contains("("))
            {
                var close = equation.IndexOf(')');
                var open = equation.Substring(0, close).LastIndexOf('(');

                var subEquation = equation.Substring(open, (close - open) + 1);

                var symbols = GetSymbols(subEquation.Trim('(', ')'));
                var result = Apply(symbols);

                equation = equation.Substring(0, open) + result + equation.Substring(close + 1);
            }

            return Apply(GetSymbols(equation));
        }
    }

    public class MathSymbol
    {
        public long? Value { get; set; }
        public string Operation { get; set; }
    }
}
