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

    private bool OneRangeFullyContainsTheOther(string line)
    {
        var parts = line.Split(',', '-').Select(int.Parse).ToArray();
        var firstContainsSecond = parts[0] >= parts[3] && parts[2] <= parts[4];
        var secondContainsFirst = parts[0] <= parts[3] && parts[2] >= parts[4];
        return firstContainsSecond || secondContainsFirst;
    }
}