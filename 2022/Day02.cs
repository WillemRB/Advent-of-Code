using System.ComponentModel;

namespace AdventOfCode
{
    public class Day02
    {
        private List<Game> games;

        [SetUp]
        public void Setup()
        {
            games = File.ReadAllLines(@"day02.txt").Select(l => new Game(l)).ToList();
        }

        [Test]
        public void PartA()
        {
            var result = games.Sum(g =>
            {
                if (g.Opponent == g.Your)
                    return g.Your + 3;
                else if (g.Your - 1 == g.Opponent % 3)
                    return g.Your + 6;
                return g.Your;
            });

            Assert.That(result, Is.EqualTo(15));
        }

        [Test]
        public void PartB()
        {
            var result = games.Sum(g =>
            {
                if (g.Your == 1) // Lose
                    return g.Opponent > 1 ? g.Opponent - 1 : 3;
                else if (g.Your == 2) // Draw
                    return g.Opponent + 3;
                return (g.Opponent % 3) + 1 + 6; // Win
            });

            Assert.That(result, Is.EqualTo(12));
        }
    }

    internal class Game
    {
        public int Opponent { get; set; }
        public int Your { get; set; }

        public Game(string game)
        {
            var chars = game.ToCharArray();
            Opponent = chars[0] - 64;
            Your = chars[2] - 87;
        }
    }
}
