namespace AdventOfCode
{
    public class Day03
    {
        private List<Rucksack> _rucksacks;

        [SetUp]
        public void Setup()
        {
            var lines = File.ReadAllLines(@"day03.txt");

            _rucksacks = lines.Select(l => new Rucksack(l)).ToList();
        }

        [Test]
        public void PartA()
        {
            var result = _rucksacks.Sum(r =>
            {
                var c = r.CommonComponent();
                return char.IsLower(c) ? c - 96 : c - 38;
            });

            Assert.That(result, Is.EqualTo(157));
        }

        [Test]
        public void PartB()
        {
            var result = 0;

            for (var i = 0; i < _rucksacks.Count; i += 3)
            {
                var rucksack1 = _rucksacks.Skip(i).First();
                var rucksack2 = _rucksacks.Skip(i + 1).First();
                var rucksack3 = _rucksacks.Skip(i + 2).First();

                var intersect = rucksack1.Contents.Intersect(rucksack2.Contents);
                var common = intersect.Intersect(rucksack3.Contents).Single();

                result += char.IsLower(common) ? common - 96 : common - 38;
            }
            
            Assert.That(result, Is.EqualTo(70));
        }
    }

    internal class Rucksack
    {
        public char[] Contents { get; private set; }

        public char[] FirstCompartment { get; private set; }

        public char[] SecondCompartment { get; private set; }

        public Rucksack(string contents)
        {
            Contents = contents.ToCharArray();
            FirstCompartment = contents.Substring(0, contents.Length / 2).ToCharArray();
            SecondCompartment = contents.Substring(contents.Length / 2).ToCharArray();
        }

        public char CommonComponent()
        {
            return FirstCompartment.Intersect(SecondCompartment).Single();
        }
    }
}
