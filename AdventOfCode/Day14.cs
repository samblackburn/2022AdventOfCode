using System.Collections;
using System.Xml.Xsl;

namespace AdventOfCode;

public class Day14
{
    [TestCase(@"498,4 -> 498,6 -> 496,6
503,4 -> 502,4 -> 502,9 -> 494,9", ExpectedResult = 24, TestName = "Example input")]
    [TestCase(null, ExpectedResult = 1068, TestName = "Challenge input")]
    public int Part1(string? inputStr)
    {
        var grid = ConstructGrid(inputStr, out var translatedSource);

        bool itFellOut;
        var grains = 0;
        do
        {
            itFellOut = DropOneGrain(grid, translatedSource);
            ++grains;
        } while (!itFellOut);
        
        Draw(grid);
        Console.WriteLine("Total grains: " + (grains - 1));
        return grains - 1;
    }
    
    [TestCase(@"498,4 -> 498,6 -> 496,6
503,4 -> 502,4 -> 502,9 -> 494,9", ExpectedResult = 93, TestName = "Example input")]
    [TestCase(null, ExpectedResult = 27936, TestName = "Challenge input")]
    public int Part2(string? inputStr)
    {
        var grid = ConstructGrid(inputStr, out var translatedSource);
        var floor = grid.GetLength(1) - 1;
        for (var x = 0; x < grid.GetLength(0); x++)
        {
            grid[x, floor] = Cell.Floor;
        }

        bool itFellOut;
        var grains = 0;
        do
        {
            itFellOut = DropOneGrain(grid, translatedSource);
            ++grains;
        } while (!itFellOut);
        
        Draw(grid);
        Console.WriteLine("Total grains: " + (grains - 1));
        return grains;
    }

    private Cell[,] ConstructGrid(string? inputStr, out Point translatedSource)
    {
        const int heightAboveFloor = 2;
        var input = (inputStr ?? File.ReadAllText("Day14Input.txt"))
            .Split(Environment.NewLine)
            .Select(line => line
                .Split(" -> ")
                .Select(coord => coord
                    .Split(",")
                    .Select(int.Parse)
                    .ToArray())
                .Select(coord => new Point(coord[0], coord[1])))
            .ToList();

        var source = new Point(500, 0);

        var top = source.Y;
        var bottom = input.Max(wiggle => wiggle.Max(c => c.Y));
        var height = bottom - top + 1 + heightAboveFloor;
        var left = source.X - height;
        var right = source.X + height;
        var width = right - left + 1;
        
        translatedSource = new Point(source.X - left, source.Y - top);

        var grid = new Cell[width, height];
        grid[source.X - left, source.Y - top] = Cell.Source;

        foreach (var wigglyLine in input)
        {
            Point? previousCoord = null;
            foreach (var coord in wigglyLine)
            {
                if (previousCoord != null)
                {
                    foreach (var cell in LineSegment(previousCoord, coord))
                    {
                        grid[cell.X - left, cell.Y - top] = Cell.Rock;
                    }
                }

                previousCoord = coord;
            }
        }

        return grid;
    }

    private bool DropOneGrain(Cell[,] grid, Point source)
    {
        do
        {
            var newPosition = LetGrainFall(grid, source);
            
            if (newPosition.Y == 0)
            {
                // It got stuck at the source!
                return true;
            }
            if (newPosition == source)
            {
                grid[newPosition.X, newPosition.Y] = Cell.Sand;
                // It settled
                return false;
            }

            source = newPosition;
        } while (source.Y < grid.GetLength(1) - 1);

        // It fell out
        return true;
    }

    private Point LetGrainFall(Cell[,] grid, Point position)
    {
        if (grid[position.X, position.Y + 1] == Cell.Empty) return new Point(position.X, position.Y + 1);
        if (grid[position.X - 1, position.Y + 1] == Cell.Empty) return new Point(position.X - 1, position.Y + 1);
        if (grid[position.X + 1, position.Y + 1] == Cell.Empty) return new Point(position.X + 1, position.Y + 1);
        return position;
    }

    private static void Draw(Cell[,] grid)
    {
        for (var y = 0; y < grid.GetLength(1); y++)
        {
            Console.Write(y.ToString("00 "));
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                Console.Write(grid[x, y] switch
                {
                    Cell.Empty => '.',
                    Cell.Rock => '#',
                    Cell.Sand => 'o',
                    Cell.Source => '+',
                    Cell.Floor => '=',
                });
            }

            Console.WriteLine();
        }
    }

    private IEnumerable<Point> LineSegment(Point start, Point end)
    {
        var xValues = Enumerable.Range(Math.Min(start.X, end.X), Math.Abs(start.X - end.X) + 1);
        var yValues = Enumerable.Range(Math.Min(start.Y, end.Y), Math.Abs(start.Y - end.Y) + 1);
        return xValues.SelectMany(x => yValues.Select(y => new Point(x, y)));
    }

    private record Point(int X, int Y);

    private enum Cell
    {
        Empty = 0,
        Rock = 1,
        Sand = 2,
        Source = 3,
        Floor = 4,
    }
}