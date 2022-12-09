namespace AdventOfCode;

public class Day8
{
    [Test]
    public void Part1()
    {
        var input = File.ReadAllLines("Day8Input.txt");
        var count = 0;
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                var isVisible = IsVisible(input, x, y, 1, 0, input[y][x])
                    || IsVisible(input, x, y, -1, 0, input[y][x])
                    || IsVisible(input, x, y, 0, 1, input[y][x])
                    || IsVisible(input, x, y, 0, -1, input[y][x]);
                if (isVisible)
                {
                    count++;
                }
            }
        }
        
        Console.WriteLine(count);
    }

    private static bool IsVisible(string[] forest, int x, int y, int dx, int dy, int height)
    {
        x += dx;
        y += dy;
        if (y < 0) return true;
        if (y >= forest.Length) return true;
        if (x < 0) return true;
        if (x >= forest[y].Length) return true;
        if (forest[y][x] >= height) return false;
        return IsVisible(forest, x, y, dx, dy, height);
    }

    [Test]
    public void Part2()
    {
        var input = @"30373
25512
65332
33549
35390".Split(Environment.NewLine);
        
        input = File.ReadAllLines("Day8Input.txt");
        var maxScore = 0;
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                var score = Score(input, x, y, 1, 0, input[y][x])
                    * Score(input, x, y, -1, 0, input[y][x])
                    * Score(input, x, y, 0, 1, input[y][x])
                    * Score(input, x, y, 0, -1, input[y][x]);
                maxScore = Math.Max(maxScore, score);
                //Console.WriteLine(input[y][x] + ": " + score);
            }
        }
        
        Console.WriteLine(maxScore);
    }

    private int Score(string[] forest, int x, int y, int dx, int dy, int height)
    {
        x += dx;
        y += dy;
        if (y < 0) return 0;
        if (y >= forest.Length) return 0;
        if (x < 0) return 0;
        if (x >= forest[y].Length) return 0;
        if (forest[y][x] >= height) return 1;
        return Score(forest, x, y, dx, dy, height) + 1;
    }
}