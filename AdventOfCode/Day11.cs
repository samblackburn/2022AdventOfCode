using System.Numerics;

namespace AdventOfCode;

public class Day11
{
    [TestCase("Day11ExampleInput.yml", ExpectedResult = 10605)]
    [TestCase("Day11Input.yml", ExpectedResult = 58786)]
    public long Part1(string file)
    {
        var monkeys = Parse(file);

        for (var round = 0; round < 20; round++)
        {
            DoRound(monkeys);
            
            Console.WriteLine("Round " + round);
            foreach (var monkey in monkeys)
            {
                Console.WriteLine(String.Join(", ", monkey.StartingItems));
            }
        }

        var inspected = monkeys.Select(m => m.ItemsInspected).OrderByDescending(x => x).ToArray();
        
        Console.WriteLine("Inspections performed by each monkey: " + string.Join(", ", inspected));
        var product = inspected[0] * inspected[1];
        Console.WriteLine("Product of top 2 monkeys: " + product);
        return product;
    }

    private void DoRound(List<MonkeyBehaviour> monkeyBehaviours)
    {
        foreach (var monkey in monkeyBehaviours)
        {
            foreach (var item in monkey.StartingItems)
            {
                monkey.ItemsInspected++;
                var newWorryLevel = monkey.Operation(item) / 3;
                var recipient = newWorryLevel % monkey.Modulus == 0 ? monkey.IfTrue : monkey.IfFalse;
                monkeyBehaviours[recipient].StartingItems.Add(newWorryLevel);
                //Console.WriteLine($"{monkey.MonkeyId} inspects {item} => {newWorryLevel}, remainder {newWorryLevel / monkey.Modulus}, throws to {recipient}");
            }
            
            monkey.StartingItems.Clear();
        }
    }

    [TestCase("Day11ExampleInput.yml", ExpectedResult = 2713310158L)]
    [TestCase("Day11Input.yml", ExpectedResult = 14952185856)]
    public long Part2(string file)
    {
        checked
        {
            var monkeys = Parse(file);

            for (var round = 0; round < 10000; round++)
            {
                DoRoundPart2(monkeys);

                switch (round)
                {
                    case 0: case 19:
                    case 999: case 1999: case 2999: case 3999: case 4999:
                    case 5999: case 6999: case 7999: case 8999: case 9999:
                        Console.WriteLine("Round " + round);
                        foreach (var monkey in monkeys)
                        {
                            Console.WriteLine($"Monkey {monkey.MonkeyId} inspected items {monkey.ItemsInspected} times, now has {String.Join(", ", monkey.StartingItems)}");
                        }

                        break;
                }
            }

            var inspected = monkeys.Select(m => m.ItemsInspected).OrderByDescending(x => x).ToArray();

            Console.WriteLine("Inspections performed by each monkey: " + string.Join(", ", inspected));
            var product = (long) inspected[0] * inspected[1];
            Console.WriteLine("Product of top 2 monkeys: " + product);
            return product;
        }
    }
    
    private void DoRoundPart2(List<MonkeyBehaviour> monkeyBehaviours)
    {
        checked
        {
            var productOfAllModuli = monkeyBehaviours.Select(m => m.Modulus).Aggregate((m, a) => m * a);
            
            foreach (var monkey in monkeyBehaviours)
            {
                foreach (var item in monkey.StartingItems)
                {
                    monkey.ItemsInspected++;
                    var newWorryLevel = monkey.Operation(item) % productOfAllModuli;
                    var recipient = newWorryLevel % monkey.Modulus == 0 ? monkey.IfTrue : monkey.IfFalse;
                    monkeyBehaviours[recipient].StartingItems.Add(newWorryLevel);
                    //Console.WriteLine($"{monkey.MonkeyId} inspects {item} => {newWorryLevel}, remainder {newWorryLevel / monkey.Modulus}, throws to {recipient}");
                }

                monkey.StartingItems.Clear();
            }
        }
    }

    private static List<MonkeyBehaviour> Parse(string file)
    {
        var monkeys = new List<MonkeyBehaviour>();

        foreach (var line in File.ReadAllLines(file).Where(l => !string.IsNullOrWhiteSpace(l)))
        {
            var afterColon = line.Split(":").LastOrDefault();
            var latterWords = afterColon.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var allWords = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            switch (allWords[0])
            {
                case "Monkey":
                    monkeys.Add(new MonkeyBehaviour{MonkeyId = int.Parse(allWords[1].TrimEnd(':'))});
                    break;
                case "Starting":
                    monkeys.Last().StartingItems = afterColon.Split(",").Select(long.Parse).ToList();
                    break;
                case "Operation:":
                    Assert.That(latterWords[0], Is.EqualTo("new"));
                    Assert.That(latterWords[1], Is.EqualTo("="));
                    Assert.That(latterWords[2], Is.EqualTo("old"));

                    if (latterWords[4] == "old")
                    {
                        monkeys.Last().Operation = latterWords[3] switch
                        {
                            "+" => x => x + x,
                            "*" => x => x * x,
                            _ => throw new Exception("Unknown operator")
                        };
                    }
                    else
                    {
                        var constant = long.Parse(latterWords[4]);
                        monkeys.Last().Operation = latterWords[3] switch
                        {
                            "+" => x => x + constant,
                            "*" => x => x * constant,
                            _ => throw new Exception("Unknown operator")
                        };
                    }

                    break;
                case "Test:":
                    Assert.That(latterWords[0], Is.EqualTo("divisible"));
                    Assert.That(latterWords[1], Is.EqualTo("by"));
                    monkeys.Last().Modulus = int.Parse(latterWords[2]);
                    break;
                case "If":
                    Assert.That(latterWords[0], Is.EqualTo("throw"));
                    Assert.That(latterWords[1], Is.EqualTo("to"));
                    Assert.That(latterWords[2], Is.EqualTo("monkey"));
                    if (allWords[1] == "true:")
                    {
                        monkeys.Last().IfTrue = int.Parse(latterWords[3]);
                    }
                    else if (allWords[1] == "false:")
                    {
                        monkeys.Last().IfFalse = int.Parse(latterWords[3]);
                    }
                    else throw new Exception("Unexpected if block");

                    break;
            }
        }

        return monkeys;
    }

    private class MonkeyBehaviour
    {
        public int MonkeyId;
        public int ItemsInspected;
        public List<long> StartingItems;
        public Func<long, long> Operation;
        public int Modulus;
        public int IfTrue;
        public int IfFalse;
    }
}