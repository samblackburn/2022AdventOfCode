namespace AdventOfCode;

[TestFixture]
public class Day4
{
    [Test]
    public void Part1()
    {
        var input = File.ReadAllLines("Day4Input.txt");
        var pairsWhereOneRangeFullyContainsTheOther = input.Where(OneRangeFullyContainsTheOther);
        Console.WriteLine(pairsWhereOneRangeFullyContainsTheOther.Count());
    }
    
    [Test]
    public void Part2()
    {
        var input = File.ReadAllLines("Day4Input.txt");
        var overlappingPairs = input.Where(Overlaps);
        Console.WriteLine(overlappingPairs.Count());
    }

    private bool Overlaps(string line)
    {
        var parts = line.Split(',', '-').Select(int.Parse).ToArray();
        var min = Math.Max(parts[0], parts[2]);
        var max = Math.Min(parts[1], parts[3]);
        //if (min <= max) Console.WriteLine($"{parts[0]}-{parts[1]} overlaps {parts[2]}-{parts[3]}");
        return min <= max;
    }

    private bool OneRangeFullyContainsTheOther(string line)
    {
        var parts = line.Split(',', '-').Select(int.Parse).ToArray();
        var firstContainsSecond = parts[0] <= parts[2] && parts[1] >= parts[3];
        var secondContainsFirst = parts[0] >= parts[2] && parts[1] <= parts[3];
        //if (firstContainsSecond) Console.WriteLine($"{parts[0]}-{parts[1]} contains {parts[2]}-{parts[3]}");
        //if (secondContainsFirst) Console.WriteLine($"{parts[0]}-{parts[1]} is contained in {parts[2]}-{parts[3]}");
        
        return firstContainsSecond || secondContainsFirst;
    }
}