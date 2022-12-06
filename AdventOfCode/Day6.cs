namespace AdventOfCode;

[TestFixture]
public class Day6
{
    [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", ExpectedResult = 7)]
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", ExpectedResult = 5)]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg", ExpectedResult = 6)]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", ExpectedResult = 10)]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", ExpectedResult = 11)]
    [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", ExpectedResult = -1)]
    public int Part1(string input)
    {
        for (var i = 0; i < input.Length - 4; i++)
        {
            var numUniq = input.Substring(i, 4).ToHashSet().Count;
            if (numUniq == 4) return i + 4;
        }

        return -1;
    }

    [Test]
    public void Part1Actual() => Console.WriteLine(Part1(File.ReadAllText("Day6Input.txt")));

}