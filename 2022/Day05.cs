using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day05
    {
        private List<Stack<char>> _stacks;
        private Stack<char> _crane;
        private List<(int amount, int from, int to)> _instructions;

        [SetUp]
        public void Setup()
        {
            _crane = new();
            _stacks = new();

            var lines = File.ReadAllLines(@"day05.txt");

            var stacks = lines.TakeWhile(line => !line.StartsWith(" 1")).Reverse().ToList();
            var stackRegex = new Regex(@"(\s{4}|\[.\]\s?)");

            for (int i = 0; i < stackRegex.Matches(stacks.First()).Count; i++)
                _stacks.Add(new Stack<char>());

            stacks.ForEach(row =>
            {
                var matches = stackRegex.Matches(row);
                for (int i = 0; i < matches.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(matches[i].Value))
                        _stacks[i].Push(matches[i].Value[1]);
                }
            });

            var instructions = lines.SkipWhile(line => !line.StartsWith("move")).ToList();
            var instructionRegex = new Regex(@"move (?<amount>\d+) from (?<from>\d+) to (?<to>\d+)");

            _instructions = instructions.Select(i =>
            {
                var match = instructionRegex.Match(i);

                var amount = int.Parse(match.Groups["amount"].Value);
                var from = int.Parse(match.Groups["from"].Value) - 1;
                var to = int.Parse(match.Groups["to"].Value) - 1;

                return (amount, from, to);
            }).ToList();
        }

        [Test]
        public void PartA()
        {
            _instructions.ForEach(i =>
            {
                for (int a = 0; a < i.amount; a++)
                    _stacks[i.to].Push(_stacks[i.from].Pop());
            });

            var result = string.Concat(_stacks.Select(stack => stack.Peek()));

            Assert.That(result, Is.EqualTo("CMZ"));
        }

        [Test]
        public void PartB()
        {
            _instructions.ForEach(i =>
            {
                while (_crane.Count < i.amount)
                    _crane.Push(_stacks[i.from].Pop());
                while (_crane.Any())
                    _stacks[i.to].Push(_crane.Pop());
            });

            var result = string.Concat(_stacks.Select(stack => stack.Peek()));

            Assert.That(result, Is.EqualTo("MCD"));
        }
    }
}
