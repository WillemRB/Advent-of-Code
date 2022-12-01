namespace AdventOfCode
{
    public class Day01
    {
        private List<Elf> Elfs { get; set; }

        [SetUp]
        public void Setup()
        {
            Elfs = new();

            var lines = File.ReadAllLines(@"D:\day01.txt");

            Elf elf = new();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    Elfs.Add(elf);
                    elf = new();
                    continue;
                }

                elf.Calories.Add(int.Parse(line));
            }
        }

        [Test]
        public void PartA()
        {
            var result = Elfs.Max(e => e.TotalCalories);

            Assert.That(result, Is.EqualTo(24000));
        }

        [Test]
        public void PartB()
        {
            var result = Elfs.OrderBy(e => e.TotalCalories).TakeLast(3).Sum(e => e.TotalCalories);

            Assert.That(result, Is.EqualTo(45000));
        }
    }

    internal class Elf
    {
        public List<int> Calories { get; set; } = new();

        public int TotalCalories { get { return Calories.Sum(); } }
    }
}