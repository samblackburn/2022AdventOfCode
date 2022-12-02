namespace AdventOfCode;

public class Tests
{
    [Test]
    public void Part1_TheMostCalories()
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
    
    [Test]
    public void Part2_TopThreeElves_TotalCalories()
    {
        var input = File.ReadAllLines("input.txt");
        var elves = new List<Elf> {new Elf()};
        
        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                elves.Add(new Elf{Id = elves.Count});
                continue;
            }
            
            var snack = int.Parse(line);
            elves[^1].Calories += snack;
        }

        var sorted = elves.OrderByDescending(elf => elf.Calories);

        var top3 = sorted.Take(3);

        foreach (var elf in top3)
        {
            Console.WriteLine($"elf {elf.Id} has {elf.Calories} calories");
        }
        
        Console.WriteLine(top3.Sum(t => t.Calories));
    }

    private class Elf
    {
        public int Id;
        public int Calories;
    }
}