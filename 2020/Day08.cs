using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static List<Instruction> instructions;

        static void Day08()
        {
            instructions = File.ReadAllLines("../../../input/day08.txt")
                .Select(r => new Instruction(r))
                .ToList();

            // Part A
            //Console.WriteLine($"Value before a loop starts: {RunInstructions().Item1}");

            // Part B
            for (int i = 0; i < instructions.Count; i++)
            {
                instructions.ForEach(op => op.Processed = false);

                if (instructions[i].Operation == "acc")
                    continue;

                var op = instructions[i].Operation;
                instructions[i].Operation = op == "jmp" ? "nop" : "jmp";

                var result = RunInstructions();
                if (result.Item2)
                {
                    Console.WriteLine($"Value when program terminates: {result.Item1}");
                    break;
                }

                instructions[i].Operation = op;
            }
        }

        static Tuple<int, bool> RunInstructions()
        {
            int head = 0, value = 0;
            while (!instructions[head].Processed)
            {
                instructions[head].Processed = true;

                switch (instructions[head].Operation)
                {
                    case "acc":
                        value += instructions[head].Argument;
                        head++;
                        break;
                    case "jmp":
                        head += instructions[head].Argument;
                        break;
                    case "nop":
                        head++;
                        break;
                }

                if (head == instructions.Count)
                    return Tuple.Create(value, true);
            }

            return Tuple.Create(value, false);
        }
    }

    class Instruction
    {
        public bool Processed { get; set; } = false;
        public string Operation { get; set; }
        public int Argument { get; set; }

        public Instruction(string instruction)
        {
            Operation = instruction.Split(' ')[0];
            Argument = int.Parse(instruction.Split(' ')[1]);
        }
    }
}
