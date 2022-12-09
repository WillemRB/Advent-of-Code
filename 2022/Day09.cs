using System.Numerics;

namespace AdventOfCode
{
    public class Day09
    {
        private Dictionary<Vector2, bool> _history;
        private List<(char direction, int step)> _motions;

        [SetUp]
        public void Setup()
        {
            _history = new();
            
            var lines = File.ReadAllLines(@"day09.txt");
            _motions = lines.Select(l => (direction: l[0], step: int.Parse(l[2..]))).ToList();
        }

        [Test]
        public void PartA()
        {
            ProcessMotions(tailSize: 1);

            var visited = _history.Count;

            Assert.That(visited, Is.EqualTo(13));
        }

        [Test]
        public void PartB()
        {
            ProcessMotions(tailSize: 9);

            var visited = _history.Count;

            Assert.That(visited, Is.EqualTo(1));
        }

        private void ProcessMotions(int tailSize)
        {
            List<Vector2> rope = Enumerable.Repeat(new Vector2(0, 0), tailSize + 1).ToList();
            _history[rope.Last()] = true;

            foreach (var motion in _motions)
            {
                for (int s = 0; s < motion.step; s++)
                {
                    rope[0] = motion.direction switch
                    {
                        'R' => rope[0].Move(1, 0),
                        'L' => rope[0].Move(-1, 0),
                        'U' => rope[0].Move(0, 1),
                        'D' => rope[0].Move(0, -1),
                    };

                    for (int i = 1; i < rope.Count; i++)
                    {
                        if (!rope[i].IsAdjacentTo(rope[i - 1]))
                            rope[i] = rope[i].Attach(rope[i - 1]);
                    }

                    _history[rope.Last()] = true;
                }
            }
        }
    }

    internal static class VectorExtensions
    {
        public static bool IsAdjacentTo(this Vector2 tail, Vector2 head)
        {
            return Math.Abs(head.X - tail.X) <= 1 && Math.Abs(head.Y - tail.Y) <= 1;
        }

        public static Vector2 Attach(this Vector2 tail, Vector2 head)
        {
            var diff = head - tail;

            tail.X += (diff.X < 0 ? -1 : 0) + (diff.X > 0 ? 1 : 0);
            tail.Y += (diff.Y < 0 ? -1 : 0) + (diff.Y > 0 ? 1 : 0);

            return tail;
        }

        public static Vector2 Move(this Vector2 head, int dx, int dy)
        {
            head.X += dx;
            head.Y += dy;

            return head;
        }
    }
}
