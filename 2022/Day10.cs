using System.Diagnostics;

namespace AdventOfCode
{
    public class Day10
    {
        private List<int?> _operations;
        private List<int> _checks;
        private int _registerValue;
        private int _cycle;
        private List<char> _crt;

        [SetUp]
        public void Setup()
        {
            var lines = File.ReadAllLines(@"day10.txt");
            _operations = lines.Select(l =>
            {
                var split = l.Split(' ');
                return split.Length == 1 ? (int?)null : int.Parse(split[1]);
            }).ToList();

            _checks = new List<int> { 20, 60, 100, 140, 180, 180, 220 };
            _registerValue = 1;
            _cycle = 0;
            _crt = new();
        }

        [Test]
        public void PartA()
        {
            var result = Run();

            Assert.That(result, Is.EqualTo(13140));
        }

        [Test]
        public void PartB()
        {
            Run();

            var screen = string.Concat(_crt);

            Assert.IsFalse(string.IsNullOrEmpty(screen));
        }

        private int Run()
        {
            var result = 0;

            do
            {
                Tick(ref result);

                var op = _operations.First();
                _operations.RemoveAt(0);

                if (op.HasValue)
                {
                    Tick(ref result);
                    _registerValue += op.Value;
                }
            }
            while (_operations.Any());

            return result;
        }

        private void Tick(ref int result)
        {
            var pixel = (_cycle % 40) >= _registerValue - 1 && (_cycle % 40) <= _registerValue + 1 ? '#' : '.';
            _crt.Add(pixel);

            _cycle++;

            if (_checks.Contains(_cycle))
                result += _cycle * _registerValue;

            if (_cycle % 40 == 0)
                _crt.Add('\n');
        }
    }
}
