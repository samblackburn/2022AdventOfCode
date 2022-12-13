﻿namespace AdventOfCode;

public class Day11
{
    [TestCase("Day11ExampleInput.yml")]
    public void Part1(string file)
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
        
        Console.WriteLine(string.Join(", ", inspected));
        Console.WriteLine(inspected[0] * inspected[1]);
    }

    private void DoRound(List<MonkeyBehaviour> monkeyBehaviours)
    {
        foreach (var monkey in monkeyBehaviours)
        {
            foreach (var item in monkey.StartingItems)
            {
                monkey.ItemsInspected++;
                var newWorryLevel = monkey.Operation(item) / 3;
                var recipient = newWorryLevel / monkey.Modulus == 0 ? monkey.IfTrue : monkey.IfFalse;
                monkeyBehaviours[recipient].StartingItems.Add(newWorryLevel);
                //Console.WriteLine($"{item} becomes a {newWorryLevel} and is thrown to {recipient}");
            }
            
            monkey.StartingItems.Clear();
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
                    monkeys.Add(new MonkeyBehaviour());
                    break;
                case "Starting":
                    monkeys.Last().StartingItems = afterColon.Split(",").Select(int.Parse).ToList();
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
                        var constant = int.Parse(latterWords[4]);
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
        public int ItemsInspected;
        public List<int> StartingItems;
        public Func<int, int> Operation;
        public int Modulus;
        public int IfTrue;
        public int IfFalse;
    }
}