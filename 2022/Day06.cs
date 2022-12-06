namespace AdventOfCode
{
    public class Day06
    {
        private string _buffer;

        [SetUp]
        public void Setup()
        {
            _buffer = File.ReadAllText(@"day06.txt");
        }

        [Test]
        public void PartA()
        {
            var marker = GetMarker(size: 4);

            Assert.That(marker, Is.EqualTo(7));
        }

        [Test]
        public void PartB()
        {
            var marker = GetMarker(size: 14);

            Assert.That(marker, Is.EqualTo(19));
        }

        private int GetMarker(int size)
        {
            for (int i = 0; i <= _buffer.Length - size; i++)
            {
                var set = _buffer.Substring(i, size).ToHashSet();
                if (set.Count == size)
                {
                    return i + size;
                }
            }

            return -1;
        }
    }
}
