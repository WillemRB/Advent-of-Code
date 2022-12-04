using System.Numerics;

namespace AdventOfCode
{
    public class Day04
    {
        private List<SectionAssignment> _assignments;

        [SetUp]
        public void Setup()
        {
            var lines = File.ReadAllLines(@"day04.txt");

            _assignments = lines.Select(line => new SectionAssignment(line)).ToList();
        }

        [Test]
        public void PartA()
        {
            var result = _assignments.Count(a => a.IsFullyContained());

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void PartB()
        {
            var result = _assignments.Count(a => a.HasOverlap());

            Assert.That(result, Is.EqualTo(4));
        }
    }

    internal class SectionAssignment
    {
        private BigInteger _firstAssignment = new();
        private BigInteger _secondAssignment = new();

        public SectionAssignment(string pair)
        {
            var assignments = pair.Split(',').Select(Parse).ToArray();

            _firstAssignment = assignments[0];
            _secondAssignment = assignments[1];
        }

        public bool IsFullyContained()
        {
            var xor = _firstAssignment ^ _secondAssignment;
            return (xor & _firstAssignment) == 0 || (xor & _secondAssignment) == 0;
        }

        public bool HasOverlap()
        {
            return (_firstAssignment & _secondAssignment) > 0;
        }

        private BigInteger Parse(string input)
        {
            BigInteger assignment = new();
            var sections = input.Split('-').Select(int.Parse).ToArray();

            for (var i = sections[0] - 1; i < sections[1]; i++)
                assignment |= (BigInteger)Math.Pow(2, i);

            return assignment;
        }
    }
}
