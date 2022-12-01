namespace ElfStarFruit;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var input = File.ReadAllLines("input.txt");
        var elves = new List<int> {0};
        
        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                elves.Add(0);
                continue;
            }
            
            var snack = int.Parse(line);
            elves[^1] += snack;
        }

        var max = elves.Max();
        
        Console.WriteLine(max);
    }
}