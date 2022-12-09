namespace AdventOfCode;

public class Day9
{
    private Point Up = new Point(0, -1);
    private Point Down = new Point(0, 1);
    private Point Left = new Point(-1, 0);
    private Point Right = new Point(1, 0);

    [Test]
    public void Part1()
    {
        var input = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2".Split(Environment.NewLine);
        input = File.ReadAllLines("Day9Input.txt");

        var headPosition = new Point(0, 0);
        var tailPosition = new Point(0, 0);
        var tailPositions = new HashSet<Point> {tailPosition};

        foreach (var line in input)
        {
            var magnitude = int.Parse(line.Split(" ")[1]);
            for (var i = 0; i < magnitude; i++)
            {
                headPosition += Direction(line);
                var tailVector = headPosition - tailPosition;
                if (tailVector.Magnitude > 1.5d)
                {
                    tailPosition += Clamp(tailVector);
                }

                tailPositions.Add(tailPosition);
                
                //Console.WriteLine($"{line} {headPosition} {tailPosition}");
            }
        }
        
        Console.WriteLine(tailPositions.Count);
    }
    
    [Test]
    public void Part2()
    {
        var input = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2".Split(Environment.NewLine);
        input = @"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20".Split(Environment.NewLine);
        input = File.ReadAllLines("Day9Input.txt");

        var rope = Enumerable.Range(0, 10).Select(_ => new Point(0, 0)).ToArray();
        var tailPositions = new HashSet<Point> {rope.Last()};

        foreach (var line in input)
        {
            var magnitude = int.Parse(line.Split(" ")[1]);
            for (var i = 0; i < magnitude; i++)
            {
                rope[0] += Direction(line);
                for (int s = 1; s < rope.Length; s++)
                {
                    var delta = rope[s-1] - rope[s];
                    if (delta.Magnitude > 1.5d)
                    {
                        rope[s] += Clamp(delta);
                    }
                }

                tailPositions.Add(rope.Last());
                
                //Console.WriteLine($"{line} {headPosition} {tailPosition}");
            }
        }
        
        Console.WriteLine(string.Join("; ", rope.Select((x, i) => $"{i}: {x}")));
        Console.WriteLine(tailPositions.Count);
    }

    private Point Clamp(Point tailVector)
    {
        if (tailVector.X > 1)
        {
            tailVector -= Right;
        }

        if (tailVector.X < -1)
        {
            tailVector -= Left;
        }

        if (tailVector.Y > 1)
        {
            tailVector -= Down;
        }

        if (tailVector.Y < -1)
        {
            tailVector -= Up;
        }

        return tailVector;
    }

    private Point Direction(string line)
    {
        return line.Split(" ")[0] switch
        {
            "U"=> Up,
            "D"=> Down,
            "L"=> Left,
            "R"=> Right,
            _ => throw new Exception("Unknown direction for " + line)
        };
    }

    private record Point(int X, int Y)
    {
        public static Point operator +(Point p1, Point p2) => new Point(p1.X + p2.X, p1.Y + p2.Y);
        public static Point operator -(Point p1, Point p2) => new Point(p1.X - p2.X, p1.Y - p2.Y);
        public double Magnitude => Math.Sqrt(X * X + Y * Y);
        public override string ToString() => $"({X}, {Y})";
    }
}