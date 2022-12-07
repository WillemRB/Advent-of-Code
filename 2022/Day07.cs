using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day07
    {
        private FileSystemNode _fs;

        [SetUp]
        public void Setup()
        {
            var lines = File.ReadAllLines(@"day07.txt");

            FileSystemNode current = null;
            foreach (var line in lines)
            {
                // Root
                if (line.Equals("$ cd /"))
                {
                    current = new FileSystemNode("/", null);
                    continue;
                }

                if (line.StartsWith("$ cd"))
                {
                    if (line[5..].Equals(".."))
                        current = current.Parent;
                    else
                        current = current.Directories.First(d => d.Name.Equals(line[5..]));

                    continue;
                }

                if (line.StartsWith("dir"))
                {
                    current.Directories.Add(new FileSystemNode(line[4..], current));
                }
                else if (Regex.IsMatch(line, @"^\d+"))
                {
                    var split = line.Split(' ');
                    current.Files.Add(new FileSystemNode(split[1], current, int.Parse(split[0])));
                }

                // Ignore "$ ls"
            }

            while (current.Parent != null)
                current = current.Parent;

            _fs = current;
        }

        [Test]
        public void PartA()
        {
            var size = RecurseFolder(_fs);

            Assert.That(size, Is.EqualTo(95437));
        }

        [Test]
        public void PartB()
        {
            int spaceNeeded = 30_000_000 - (70_000_000 - _fs.DirectorySize());
                
            var size = FindFolderToDelete(_fs, spaceNeeded, int.MaxValue);

            Assert.That(size, Is.EqualTo(24933642));
        }

        private int RecurseFolder(FileSystemNode node)
        {
            int size = 0;

            var dirsize = node.DirectorySize();
            if (dirsize < 100_000)
                size += dirsize;

            if (node.Directories.Any())
                foreach (var dir in node.Directories)
                    size += RecurseFolder(dir);

            return size;
        }

        private int FindFolderToDelete(FileSystemNode node, int spaceNeeded, int bestMatch)
        {
            var dirsize = node.DirectorySize();
            if (dirsize > spaceNeeded && dirsize < bestMatch)
                bestMatch = dirsize;

            if (node.Directories.Any())
                foreach (var dir in node.Directories)
                    bestMatch = FindFolderToDelete(dir, spaceNeeded, bestMatch);

            return bestMatch;
        }
    }

    internal class FileSystemNode
    {
        public FileSystemNode Parent { get;  set; }
        public List<FileSystemNode> Directories { get; set; }
        public List<FileSystemNode> Files { get; set; }

        public int? Size { get; set; }
        public string Name { get; set; }

        public int DirectorySize()
        {
            return Directories.Sum(d => d.DirectorySize()) + Files.Sum(f => f.Size.Value);
        }

        public FileSystemNode(string name, FileSystemNode parent, int? size = null)
        {
            Directories = new();
            Files = new();

            Name = name;
            Parent = parent;
            Size = size;
        }
    }
}
