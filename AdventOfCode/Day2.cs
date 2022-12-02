using static AdventOfCode.Day2.Rps;

namespace AdventOfCode;

public class Day2
{
    [Test]
    public void Part1()
    {
        //var input = new[] {"A Y", "B X", "C Z"};
        var input = File.ReadAllLines("Day2Input.txt");
        
        var scores = input.Select(Score);

        foreach (var score in scores)
        {
           // Console.WriteLine(score);
        }

        var totalScore = scores.Sum();
        
        Console.WriteLine($"Total: {totalScore}");
    }

    private int Score(string input)
    {
        var theirInput = input[0] switch
        {
            'A' => Rock, 'B' => Paper, 'C' => Scissors,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var myInput = input[2] switch
        {
            'X' => Rock, 'Y' => Paper, 'Z' => Scissors,
            _ => throw new ArgumentOutOfRangeException()
        };

        var rpsScore = (theirInput, myInput) switch
        {
            (Rock, Rock) => 3,
            (Rock, Paper) => 6,
            (Rock, Scissors) => 0,
            (Paper, Rock) => 0,
            (Paper, Paper) => 3,
            (Paper, Scissors) => 6,
            (Scissors, Rock) => 6,
            (Scissors, Paper) => 0,
            (Scissors, Scissors) => 3,
        };

        var selectedScore = (int) myInput;

        return selectedScore + rpsScore;
    }

    internal enum Rps
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }
}