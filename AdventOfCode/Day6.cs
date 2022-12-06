namespace AdventOfCode;

[TestFixture]
public class Day6
{
    [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 4, ExpectedResult = 7)]
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 4, ExpectedResult = 5)]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 4, ExpectedResult = 6)]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 4, ExpectedResult = 10)]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 4, ExpectedResult = 11)]
    [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 4, ExpectedResult = -1)]
    public int FindNonRepeatingCharacters(string input, int length)
    {
        for (var i = 0; i < input.Length - length; i++)
        {
            var numUniq = input.Substring(i, length).ToHashSet().Count;
            if (numUniq == length) return i + length;
        }

        return -1;
    }

    [Test]
    public void Part1() => Console.WriteLine(FindNonRepeatingCharacters(File.ReadAllText("Day6Input.txt"), 4));

}