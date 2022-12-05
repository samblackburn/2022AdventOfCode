namespace AdventOfCode;

[TestFixture]
public class Day5
{
    [Test]
    public void Part1()
    {
        var stacks = ParseInput(out var moves);

        Drawify(stacks);
        
        foreach (var move in moves)
        {
            for (var step = 0; step < move.HowMany; step++)
            {
                var crate = stacks[move.From].Pop();
                //Console.WriteLine($"Move {crate} from {move.From} to {move.To}");
                stacks[move.To].Push(crate);
            }
        }

        Drawify(stacks);
    }
    
    [Test]
    public void Part2()
    {
        var stacks = ParseInput(out var moves);

        Drawify(stacks);
        
        foreach (var move in moves)
        {
            var toMove = new Stack<char>();
            
            for (var step = 0; step < move.HowMany; step++)
            {
                var crate = stacks[move.From].Pop();
                toMove.Push(crate);
            }
            
            for (var step = 0; step < move.HowMany; step++)
            {
                var crate = toMove.Pop();
                stacks[move.To].Push(crate);
            }
        }

        Drawify(stacks);
    }

    private static Stack<char>[] ParseInput(out List<Move> moves)
    {
        var lines = File.ReadAllLines("Day5Input.txt");
        var stacks = new Stack<char>[11];
        for (int i = 0; i < stacks.Length; i++)
        {
            stacks[i] = new Stack<char>();
        }

        moves = new List<Move>();

        foreach (var line in lines.Reverse())
        {
            if (line.StartsWith("[") || line.StartsWith((" ")))
            {
                for (var stackId = 1; stackId * 4 < line.Length + 2; stackId++)
                {
                    var crate = line[stackId * 4 - 3];
                    if (crate != ' ') stacks[stackId].Push(crate);
                }
            }
            else if (line.StartsWith("move "))
            {
                var parts = line.Split();
                var howMany = parts[1];
                var from = parts[3];
                var to = parts[5];

                moves.Add(new Move {HowMany = int.Parse(howMany), From = int.Parse(@from), To = int.Parse(to)});
            }
        }

        moves.Reverse();
        return stacks;
    }

    private static void Drawify(IEnumerable<Stack<char>> stacks)
    {
        var i = 0;
        foreach (var stack in stacks)
        {
            Console.Write($"Stack ");
            Console.Write(string.Join("", stack.Reverse()));
            Console.WriteLine();
        }
    }

    private class Move
    {
        public int HowMany;
        public int From;
        public int To;
    }
}