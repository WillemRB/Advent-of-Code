using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    partial class AdventOfCode
    {
        static void Day08()
        {
            var instructions = File.ReadAllLines("../../../input/day08.txt")
                .Select(r => new Instruction(r))
                .ToList();

            int head = 0, value = 0;
            while (!instructions[head].Processed)
            {
                instructions[head].Processed = true;

                switch(instructions[head].Operation)
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
            }

            // Part A
            Console.WriteLine($"Value before a loop starts: {value}");
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
