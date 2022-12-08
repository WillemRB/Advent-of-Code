namespace AdventOfCode
{
    public class Day08
    {
        private Tree[][] _forest;

        [SetUp]
        public void Setup()
        {
            var lines = File.ReadAllLines(@"day08.txt");

            _forest = new Tree[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                _forest[i] = new Tree[lines[i].Length];
                for (int j = 0; j < lines[i].Length; j++)
                    _forest[i][j] = new Tree(lines[i][j] - 48);
            }
        }

        [Test]
        public void PartA()
        {
            for (int i = 0; i < _forest.Length; i++)
            {
                for (int j = 0; j < _forest[i].Length; j++)
                {
                    if (_forest[i][j].IsVisible)
                        continue;

                    var current = _forest[i][j].Height;

                    _forest[i][j].IsVisible =
                        _forest[0..i].All(row => row[j].Height < current) ||
                        _forest[(i + 1)..].All(row => row[j].Height < current) ||
                        _forest[i][0..j].All(t => t.Height < current) ||
                        _forest[i][(j + 1)..].All(t => t.Height < current);
                }
            }

            var visible = _forest.Sum(row => row.Count(t => t.IsVisible));

            Assert.That(visible, Is.EqualTo(21));
        }

        [Test]
        public void PartB()
        {
            for (int i = 0; i < _forest.Length; i++)
            {
                for (int j = 0; j < _forest[i].Length; j++)
                {
                    var current = _forest[i][j].Height;

                    var s1 = _forest[0..i].Reverse().TakeWhile(row => row[j].Height < current).Count();
                    s1 += s1 < _forest[0..i].Length ? 1 : 0;

                    var s2 = _forest[(i + 1)..].TakeWhile(row => row[j].Height < current).Count();
                    s2 += s2 < _forest[(i + 1)..].Length ? 1 : 0;

                    var s3 = _forest[i][0..j].Reverse().TakeWhile(t => t.Height < current).Count();
                    s3 += s3 < _forest[i][0..j].Length ? 1 : 0;

                    var s4 = _forest[i][(j + 1)..].TakeWhile(t => t.Height < current).Count();
                    s4 += s4 < _forest[i][(j + 1)..].Length ? 1 : 0;

                    _forest[i][j].ScenicScore = s1 * s2 * s3 * s4;
                }
            }

            var scenicScore = _forest.Max(row => row.Max(t => t.ScenicScore));

            Assert.That(scenicScore, Is.EqualTo(8));
        }
    }

    internal class Tree
    {
        public bool IsVisible { get; set; } = false;
        public int Height { get; set; }
        public int ScenicScore { get; set; }

        public Tree(int height)
        {
            Height = height;
        }
    }
}
