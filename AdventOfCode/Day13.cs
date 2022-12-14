using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdventOfCode;

public class Day13
{
    private const string ExampleInput = @"[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]";

    [TestCase(ExampleInput, ExpectedResult = 13, TestName = "Example input")]
    [TestCase(null, ExpectedResult = 5684, TestName = "Puzzle Input")]
    public int Part1(string? input)
    {
        input = input ?? File.ReadAllText("Day13Input.txt");
        var result = input.Split(Environment.NewLine)
            .Chunk(3)
            .Select(IsInCorrectOrder)
            .ToList();
        foreach (var line in result) Console.WriteLine(line);
        return result.Select((order, index) => order == Order.Correct ? index + 1 : 0).Sum();
    }

    [TestCase("[1,1,3,1,1]", "[1,1,5,1,1]", ExpectedResult = Order.Correct)]
    [TestCase("[[1],[2,3,4]]", "[[1],4]", ExpectedResult = Order.Correct)]
    [TestCase("1", "1", ExpectedResult = Order.Equal)]
    [TestCase("[]", "3", ExpectedResult = Order.Correct)]
    [TestCase("[[[]]]", "[[]]", ExpectedResult = Order.Incorrect)]
    public Order IsInCorrectOrder(params string[] threeLines)
    {
        var left = JsonConvert.DeserializeObject(threeLines[0]);
        var right = JsonConvert.DeserializeObject(threeLines[1]);
        //Assert.IsEmpty(threeLines[2]);

        return CompareObjects((left, right));
    }

    private Order CompareLists(JArray left, JArray right)
    {
        //return left.Zip(right).Select(CompareObjects).FirstOrDefault(x => x != Order.Equal);
        for (var i = 0; i < Math.Min(left.Count, right.Count); i++)
        {
            var order = CompareObjects((left[i], right[i]));
            if (order != Order.Equal) return order;
        }

        // Shorter list should come first
        return CompareInts(left.Count, right.Count);
    }

    private Order CompareObjects((object, object) pair) => pair switch
    {
        (JArray left, JArray right) => CompareLists(left, right),
        (JValue left, JValue right) => CompareInts((long)left, (long)right),
        (long left, long right) => CompareInts(left, right),
        (JArray left, JValue right) => CompareLists(left, new JArray {(int)right}),
        (JArray left, long right) => CompareLists(left, new JArray {(int)right}),
        (JValue left, JArray right) => CompareLists(new JArray {(int)left}, right),
        (long left, JArray right) => CompareLists(new JArray {(int)left}, right),
        _ => throw new ArgumentException($"{pair.Item1.GetType()}, {pair.Item2.GetType()}")
    };

    private Order CompareInts(long left, long right)
    {
        if (left < right) return Order.Correct;
        if (left == right) return Order.Equal;
        return Order.Incorrect;
    }

    public enum Order
    {
        Correct = -1,
        Equal = 0,
        Incorrect = 1,
    }
}