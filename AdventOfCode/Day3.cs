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
    
    [Test]
    public void Part2()
    {
        var rucksacks = File.ReadAllLines("Day3Input.txt");
        var total = 0;
        var groups = 0;

        var allItemTypes = File.ReadAllText("Day3Input.txt").ToHashSet();

        for (var index = 0; index < rucksacks.Length - 2; index+=3)
        {
            var elf1 = rucksacks[index];
            var elf2 = rucksacks[index + 1];
            var elf3 = rucksacks[index + 2];

            var common = elf1.ToHashSet()
                .Intersect(elf2.ToHashSet())
                .Intersect(elf3.ToHashSet());

            var badgePriority = Priority(common.Single());
            groups++;
            total += badgePriority;
        }
        
        Console.WriteLine("Total: " + total);
        Console.WriteLine("Groups: " + groups);
        Console.WriteLine("Rucksacks / 3: " + rucksacks.Length / 3);
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