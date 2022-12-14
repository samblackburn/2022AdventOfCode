namespace AdventOfCode;

public class Day12
{
    private const string ExampleInput = @"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi";

    private const string MainInput = @"abaaacccccccccaaaaaaccccccccccccccccaacccccccccccaacaaaaaaaaaaaaaaaaaccaaaaacccaaaaccccccccccccccccccccccccccccccccccccccccccccccccccccccccaaaaa
abaaacccccccccaaaaaacccccccccccccccaaaaccccccccccaaaaaaaacaaaaaaaaaaaccaaaaaaccaaaacccccccccccccccccccccccccccccccccccccccccccccccccccccccaaaaaa
abaaaccccccccccaaaaacccccccccccccccaaaacccccccccccaaaaacccaaaaaaaaaacccaaaaaacccaaccccccccccccccaaaaacccccccccccccccccccccccccccccccccccccaaaaaa
abccccaaccccccaaaaacccccccccaaaaaccaaaaccccccccccccaaaaacaaaaaaaaacccccaaaaaccccccccccccccccccccaaaaacccccccccccccccccaaaccccaaaccccccccccaaacaa
abcccaaaacccccaaaaacccccccccaaaaacccccccccccccccccaaacaaaaaaaaaacccccccaaaaacccccccccccccccccccaaaaaacccccccccccccccccaaaaccaaaaccccccccccccccaa
abcccaaaaacacccccccccccccccaaaaaaccccccccccccccccccaaccaaaaacaaaaccccccccccccccccccccccccccccccaaaaaaccccccccccccccccccaaaaaaaacccccccccccccccaa
abaaaaaaaaaacccccccccccccccaaaaaaccccccccccccccccccccccaaaacccaaaccccccccccccccccccccccccccccccaaaaaacccccccccccccccciiiiijaaaaccccccccccccccccc
abaaaaaaaaaacccccccccccccccaaaaaacccccccccccccccccccccccccccccaaacccccccccccccccccccccccccccccccaaaccccccccccccccccciiiiiijjjaccccccccaaaccccccc
abccaaaaaaccccccccccccccccccaaaccccccccccccccccccccccccccccccccacccccccccccaacccccccccccccccccccccccccccccccccccccciiiiioijjjjaaccccccaaaaaacccc
abccaaaaaacccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccaaaaaacccccccccccccccccccccccccccccccccccciiinnooojjjjjaaccaaaaaaaacccc
abccaaaaaacccccccccccccccccccccccccccccccccccccaacccccaacccccccccccccccccaaaaaacccccccccccccccccccccccccccaaaccccciiinnnoooojjjjjjkkkaaaaaaacccc
abcaaaaaaaaccccccccccccccccccccccccccccccccccccaaaccaaaaaaccccaaacccccccccaaaacccccccccccccccccccccccccccccaaaaccciiinnnouooojjjjkkkkkaaaaaccccc
abccaccccccccccccccccccaaccccccaccccccccccccaaaaaaaaaaaaaacccaaaacccccccccaaaacccccccccccccccccccccccccccaaaaaacchhinnnttuuooooookkkkkkkaaaccccc
abccccccccccccccccccaacaaaccccaaaaaaaaccccccaaaaaaaacaaaaacccaaaacccccccccaccacccccccccccccccccccccccccccaaaaacchhhhnntttuuuooooppppkkkkcaaacccc
abccccccccaaacccccccaaaaaccccccaaaaaaccccccccaaaaaacaaaaaccccaaaaccccccccccccccccccccccccccccaccccccccccccaaaaahhhhnnntttxuuuooppppppkkkcccccccc
abccccccccaaaacccccccaaaaaaccccaaaaaaccaaacccaaaaaacaaaaaccccccccccccccaaccccccccccccccaaaaaaaacccccccccccaachhhhhnnnntttxxuuuuuuuupppkkkccccccc
abccccccccaaaacccccaaaaaaaacccaaaaaaaacaaacacaaaaaaccccccccccccccccccccaacccccccccccccccaaaaaacccccccccccccchhhhmnnnntttxxxxuuuuuuupppkkcccccccc
abacccccccaaaacccccaaaaacaaccaaaaaaaaaaaaaaaaaaccaacccccccccccccccccaaaaaaaaccccccccccccaaaaaaccccccccccccchhhhmmmntttttxxxxuyyyuvvpppklcccccccc
abacccccccccccccccccacaaaccaaaaaaaaaaaaaaaaaaaccccccccccccccccccccccaaaaaaaacccccccccccaaaaaaaaccccccccccccgghmmmtttttxxxxxxyyyyvvvpplllcccccccc
abaccccccccaacccccccccaaaccaaaaaaaacaaaaaaaaaaccccccccccccccccccccccccaaaaccccccccccccaaaaaaaaaaccccccaccccgggmmmtttxxxxxxxyyyyyvvppplllcccccccc
SbaaaccccccaaacaaaccccccccaaaaaaaaacaaaaaaaaacccccccccccccccccccccccccaaaaacccccccccccaaaaaaaaaaaaacaaaccaagggmmmtttxxxEzzzzyyyvvppplllccccccccc
abaacccccccaaaaaaacccccccaaaaaaacaaccaaaaaaaccccccccccccccaaaccccccccaaaaaacccccccccccacacaaacccaaaaaaacaaagggmmmsssxxxxxyyyyyvvvqqqlllccccccccc
abaccccccccaaaaaaccacccaaaaaaaaacccccccaaaaaaccccccccccccaaaaccccccccaaccaacccccccccccccccaaaccccaaaaaaccaagggmmmssssxxwwyyyyyyvvqqqlllccccccccc
abaccccccaaaaaaaaccaacaaaccaaaaaacccccaaaaaaaccccccccccccaaaaccccccccccaacccccccccccccccccaacccccaaaaaaaaaaggggmmmssssswwyywyyyyvvqqlllccccccccc
abaccccccaaaaaaaaacaaaaacccaaaaaacccccaaacaaaccccccccccccaaaaccccccccaaaaaaccccccccccccaacccccccaaaaaaaaaaaaggggmmmossswwyywwyyvvvqqqllccccccccc
abcccccccaaaaaaaaaacaaaaaacaaccccccccaaacccccccccccccccccccccccccccccaaaaaaccccccccccccaaaaacccaaaaaaaaaaaaaaggggoooosswwywwwwvvvvqqqmlccccccccc
abccccccccccaaacaaaaaaaaaacccccccccccaaacaccccccccccccccccccccccccccccaaaaccccccccccccaaaaaccccaaacaaacccaaacagggfooosswwwwwrvvvvqqqqmmccccccccc
abccccccccccaaacccaaaaaaaacccccccccaacaaaaacccccccccccccccccccccccccccaaaaccccccccccccaaaaaacccccccaaacccaaccccfffooosswwwwrrrrrqqqqqmmccccccccc
abccccccccccaacccccccaaccccccccccccaaaaaaaacccccccccccccaaccccccccccccaccaccccccccccccccaaaacccccccaacccccccccccfffoossrwrrrrrrrqqqqmmmccccccccc
abccaaaccccccccccccccaacccccccccccccaaaaaccccccccccccaacaacccccccaaaaacccccccccccccccccaacccccccccccccccccccccccfffoossrrrrrnnnmqqmmmmmccccccccc
abcaaaaccccccccccccccccccccccccccccccaaaaacccccccccccaaaaacccccccaaaaacccaaaccccccccccccccccccccccccccccccccccccfffooorrrrrnnnnmmmmmmmccccaacccc
abcaaaacccccccccccccccccccccccccccccaaacaaccccacccccccaaaaaaccccaaaaaaccccaaaccacccccccccccccccccccccccccccccccccffoooonnnnnnnnmmmmmmccccaaacccc
abccaaacccccccccccccccccccccaaaaaccccaaccccaaaacccccaaaaaaaaccccaaaaaaccccaaaaaaaccccccccccccccccaccaccccccccccccfffooonnnnnnddddddddcccaaaccccc
abccccccccccccccccccccccccccaaaaaccccccccccaaaaaacccaaaaacaacccaaaaaaaccaaaaaaaacccccccccccccccccaaaaccccccccccccfffeonnnnneddddddddddcaaacccccc
abccccccccccaaaccccccccccccaaaaaacccccccccccaaaacccccacaaacccccaacaacccaaaaaaaaacccccccccccccccccaaaacccccccccccccffeeeeeeeeddddddddcccaaacccccc
abcccccccccaaaaccccacccccccaaaaaaccccccccccaaaaacccccccaaaacccaaacaccccaaaaaaaaaccccccccccccccccaaaaaaccccccccccccceeeeeeeeedacccccccccccccccccc
abaccccccccaaaaccccaaacaaacaaaaaaccccccccccaacaaccccccccaaaacaaaacaaacaaaaaaaaaacccccccccccccaacaaaaaacccccccccccccceeeeeeeaaacccccccccccccccaaa
abaaacccccccaaaccccaaaaaaaccaaaccccccccaaacccccccccccccccaaaaaaaacaaaaaaaaaaaaaaacacaaccaaacaaacccaacccccccccccccccccaacccaaaacccccccccccccccaaa
abaaaccccccccccccccaaaaaaccccccccccccccaaacccccccccccccccaaaaaaaccaaaaaaccaacccaccaaaaccaaaaaaaccccccccaaccccccccccccccccccaaacccccccccccccccaaa
abaaccccccccccccccaaaaaaacccccccccccaaaaaaaaccccccccccccccaaaaaaaaaaaaaacaaaccccccaaaaaccaaaaaaccccccaaaaccccccccccccccccccaaaccccccccccccaaaaaa
abaaaccccccccccccaaaaaaaaaacccccccccaaaaaaaacccccccccccaaaaaaaaaaaaaaaaaaacccccccaaaaaacaaaaaaaaaccccaaaaaacccccccccccccccccccccccccccccccaaaaaa";

    [TestCase(ExampleInput, ExpectedResult = 31, TestName = "Example input")]
    [TestCase(MainInput, ExpectedResult = 423, TestName = "Main input")]
    public int Part1(string inputStr)
    {
        var input = inputStr.Split(Environment.NewLine);

        var height = input.Length;
        var width = input[0].Length;

        var scores = new int[height, width];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (input[y][x] == 'S')
                {
                    scores[y, x] = 1;
                }
            }
        }

        var step = 1;
        while (!ScanAll(input, scores, step++) && step < 1000)
        {
            
        }

        //DumpMap(scores);
        return step - 1;
    }
    
    [TestCase(ExampleInput, ExpectedResult = 28, TestName = "Example input")]
    [TestCase(MainInput, ExpectedResult = 416, TestName = "Main input")]
    public int Part2(string inputStr)
    {
        var input = inputStr.Replace('S', 'a').Split(Environment.NewLine);
        var result = int.MaxValue;
        
        var width = input[0].Length;
        var height = input.Length;
        for (var y = 0; y < height; y++)
        {
            //for (var x = 0; x < width; x++)
            var x = 0;
            {
                var originalLine = input[y];
                var line = originalLine.ToArray();
                line[x] = 'S';
                input[y] = new string(line);

                var steps = Part1(string.Join(Environment.NewLine, input));
                if (steps < result) result = steps;
                Console.WriteLine(steps);
                
                input[y] = originalLine;
            }
        }

        return result;
    }

    private void DumpMap(int[,] scores)
    {
        var width = scores.GetLength(1);
        var height = scores.GetLength(0);
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Console.Write(scores[y, x] % 10);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private bool ScanAll(string[] input, int[,] scores, int steps)
    {
        var width = input[0].Length;
        var height = input.Length;
        
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (scores[y, x] == steps && scores[y, x] != 0)
                {
                    var altitude = input[y][x];

                    var finished =
                    //Up
                    Bump(input, scores, x, y - 1, steps + 1, altitude) ||

                    //Down
                    Bump(input, scores, x, y + 1, steps + 1, altitude) ||

                    //Left
                    Bump(input, scores, x - 1, y, steps + 1, altitude) ||

                    //Right
                    Bump(input, scores, x + 1, y, steps + 1, altitude);

                    if (finished) return true;
                }
            }
        }
        
        return false;
    }

    private bool Bump(string[] input, int[,] scores, int x, int y, int steps, char prevSquare)
    {
        var width = input[0].Length;
        var height = input.Length;

        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            // Array out of bounds
            return false;
        }

        var thisSquare = input[y][x];
        var thisScore = scores[y, x];

        if (char.IsLower(thisSquare) && char.IsLower(prevSquare) && thisSquare > prevSquare + 1)
        {
            // Too steep
            return false;
        }

        if (thisSquare == 'E' && prevSquare < 'z')
        {
            // Too steep
            return false;
        }

        if (thisSquare == 'E')
        {
            // We've finished!
            return true;
        }

        if (thisScore < steps && thisScore > 0)
        {
            // Already been here by a shorter route
            return false;
        }

        scores[y, x] = steps;
        return false;
    }
}