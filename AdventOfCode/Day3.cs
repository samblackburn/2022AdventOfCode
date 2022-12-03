namespace AdventOfCode;

[TestFixture]
public class Day3
{
    [Test]
    public void Part1()
    {
        var rucksacks = File.ReadAllLines("Day3Input.txt");
        var total = 0;
        foreach (var rucksack in rucksacks)
        {
            var totalSize = rucksack.Length;
            var firstHalf = rucksack.Substring(0, totalSize / 2).ToHashSet();
            var secondHalf = rucksack.Substring(totalSize / 2).ToHashSet();
            var common = firstHalf.Intersect(secondHalf);
            var priorities = common.Select(Priority).ToArray();
            total += priorities.Sum();
        }
        
        Console.WriteLine("Total: " + total);
    }

    private int Priority(char item)
    {
        if (char.IsUpper(item))
        {
            return 27 + item - 'A';
        }
        else
        {
            return 1 + item - 'a';
        }
    }
}