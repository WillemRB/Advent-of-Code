namespace AdventOfCode
{
    public class Day10
    {
        private List<int?> _operations;
        private List<int> _checks;
        private int _registerValue;
        private int _cycle;

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
        }

        [Test]
        public void PartA()
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

            Assert.That(result, Is.EqualTo(13140));
        }

        [Test]
        public void PartB()
        {

        }

        private void Tick(ref int result)
        {
            _cycle++;

            if (_checks.Contains(_cycle))
                result += _cycle * _registerValue;
        }
    }
}
