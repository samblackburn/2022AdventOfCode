namespace AdventOfCode;

[TestFixture]
public class Day7
{
    private const string c_SampleInput = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";

    private string m_Path;
    private IDictionary<string, long> m_DirectorySizes;

    private static string c_NestedInput = @"$ cd /
$ ls
dir a
$ cd a
$ ls
dir b
$ cd b
$ ls
584 c.txt";

    private static IEnumerable<TestCaseData> Day7Part1 => new[]
    {
        new TestCaseData(new object[] {File.ReadAllLines("Day7Input.txt")})
            {ExpectedResult = 1501149, TestName = "Day 7 input"},
        new TestCaseData(new object[] {c_SampleInput.Split(Environment.NewLine)})
            {ExpectedResult = 95437, TestName = "Example input"},
        new TestCaseData(new object[] {c_NestedInput.Split(Environment.NewLine)})
            {ExpectedResult = 584 * 3, TestName = "A folder that only contains a subfolder"},
    };
    
    private static IEnumerable<TestCaseData> Day7Part2 => new[]
    {
        new TestCaseData(new object[] {File.ReadAllLines("Day7Input.txt")})
            {ExpectedResult = 10096985, TestName = "Day 7 input"},
        new TestCaseData(new object[] {c_SampleInput.Split(Environment.NewLine)})
            {ExpectedResult = 24933642, TestName = "Example input"},
        new TestCaseData(new object[] {c_NestedInput.Split(Environment.NewLine)})
            {ExpectedResult = 584, TestName = "A folder that only contains a subfolder"},
    };

    private void ParseInput(string[] lines)
    {
        m_Path = ":";
        m_DirectorySizes = new Dictionary<string, long> {{":", 0}};

        foreach (var line in lines)
        {
            if (line.StartsWith("$ "))
            {
                ProcessCommand(line);
            }
            else
            {
                ProcessCommandOutput(line);
            }
        }
    }

    [TestCaseSource(nameof(Day7Part1))]
    public long Part1(string[] lines)
    {
        ParseInput(lines);
        
        var total = 0L;
        foreach (var dir in m_DirectorySizes)
        {
            var size = SizeWithDescendents(dir.Key);
            if (size > 100_000L) continue;
            total += size;
            Console.WriteLine($"Folder {dir.Key} is of size {size}");
        }

        Console.WriteLine(total);
        return total;
    }
    
    [TestCaseSource(nameof(Day7Part2))]
    public long Part2(string[] lines)
    {
        ParseInput(lines);

        var totalCapacity = 70000000;
        var needAvailable = 30000000;
        var currentlyUsed = m_DirectorySizes.Sum(x => x.Value);
        Console.WriteLine("Used: " + currentlyUsed);
        var needToFree = needAvailable - (totalCapacity - currentlyUsed);
        Console.WriteLine("Need to free: " + needToFree);
        
        var smallest = m_DirectorySizes.Select(x => SizeWithDescendents(x.Key)).OrderBy(x => x).First(x => x > needToFree);
        
        Console.WriteLine(smallest);
        return smallest;
    }

    private long SizeWithDescendents(string dir)
    {
        return m_DirectorySizes
            .Where(kvp => kvp.Key.StartsWith(dir))
            .Sum(kvp => kvp.Value);
    }

    private void ProcessCommandOutput(string line)
    {
        var parts = line.Split(" ");
        if (parts[0] == "dir")
        {
            m_DirectorySizes.Add(m_Path + "/" + parts[1], 0);
        }
        else if (!long.TryParse(parts[0], out var size))
        {
            throw new Exception("Could not parse long in " + line);
        }
        else
        {
            m_DirectorySizes[m_Path] += size;
        }
    }

    private void ProcessCommand(string line)
    {
        switch (line)
        {
            case "$ ls":
                break;
            case "$ cd /":
                m_Path = ":";
                break;
            case "$ cd ..":
                m_Path = Parent(m_Path);
                break;
            default:
            {
                if (!line.StartsWith("$ cd "))
                {
                    throw new Exception("Unexpected command " + line);
                }

                if (line.Split(" ").Length != 3)
                {
                    throw new Exception("Wrong number of parts: " + line);
                }
                
                var subDir = line.Split(" ")[2];
                if (subDir.Contains('/'))
                {
                    throw new Exception($"Could not parse: {line}");
                }

                m_Path = m_Path + "/" + subDir;
                
                break;
            }
        }
    }

    [TestCase("/a/b/c", ExpectedResult = "/a/b")]
    [TestCase("a/b/c", ExpectedResult = "a/b")]
    [TestCase("a/b/b/c", ExpectedResult = "a/b/b")]
    public static string Parent(string path)
    {
        if (path.EndsWith("/")) throw new Exception("Path " + path + " should not end with /");
        return string.Join("/", path.Split("/").SkipLast(1));
    }
}