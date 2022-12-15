using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Xml.Xsl;

namespace AdventOfCode;

public class Day15
{
    private const string ExampleInput = @"Sensor at x=2, y=18: closest beacon is at x=-2, y=15
Sensor at x=9, y=16: closest beacon is at x=10, y=16
Sensor at x=13, y=2: closest beacon is at x=15, y=3
Sensor at x=12, y=14: closest beacon is at x=10, y=16
Sensor at x=10, y=20: closest beacon is at x=10, y=16
Sensor at x=14, y=17: closest beacon is at x=10, y=16
Sensor at x=8, y=7: closest beacon is at x=2, y=10
Sensor at x=2, y=0: closest beacon is at x=2, y=10
Sensor at x=0, y=11: closest beacon is at x=2, y=10
Sensor at x=20, y=14: closest beacon is at x=25, y=17
Sensor at x=17, y=20: closest beacon is at x=21, y=22
Sensor at x=16, y=7: closest beacon is at x=15, y=3
Sensor at x=14, y=3: closest beacon is at x=15, y=3
Sensor at x=20, y=1: closest beacon is at x=15, y=3";

    [TestCase(@"Sensor at x=0, y=0: closest beacon is at x=1, y=1", 0, ExpectedResult = 5)]
    [TestCase(@"Sensor at x=9, y=16: closest beacon is at x=10, y=16", 10, ExpectedResult = 0)]
    [TestCase(ExampleInput, 10, ExpectedResult = 26, TestName = "Example input")]
    [TestCase(null, 2000000, ExpectedResult = 5564017, TestName = "Challenge input")]
    public int Part1(string? input, int y)
    {
        input ??= File.ReadAllText("Day15Input.txt");
        var sensors = input.Split(Environment.NewLine).Select(Parse).ToList();
        var minX = sensors.Min(sensor => sensor.X - sensor.Distance);
        var maxX = sensors.Max(sensor => sensor.X + sensor.Distance);

        var line = new bool[maxX - minX + 1];
        foreach (var sensor in sensors)
        {
            var yDifference = Math.Abs(y - sensor.Y);
            var xDistance = sensor.Distance - yDifference;
            for (var x = sensor.X - xDistance; x <= sensor.X + xDistance; x++)
            {
                line[x - minX] = true;
            }
        }

        var beaconsOnLine = sensors.Where(x => x.beaconY == y).Select(x => x.beaconX).Distinct().Count();

        return line.Count(isCovered => isCovered) - beaconsOnLine;
    }

    [TestCase(ExampleInput, 20, ExpectedResult = 56000011, TestName = "Example input")]
    [TestCase(@"Sensor at x=0, y=0: closest beacon is at x=1, y=1", 2, ExpectedResult = -1)]
    [TestCase(@"Sensor at x=0, y=0: closest beacon is at x=1, y=1", 3, ExpectedResult = 8000001)]
    [TestCase(null, 4000000, ExpectedResult = 11558423398893, Explicit = true)]
    public long Part2(string? input, int maxCoord)
    {
        input ??= File.ReadAllText("Day15Input.txt");
        var sensors = input.Split(Environment.NewLine).Select(Parse).ToList();

        var approxLocations = new HashSet<Point>();

        var boxSize = Math.Min(200, maxCoord);
        for (var y = boxSize/2; y < maxCoord; y+=boxSize)
        {
            for (var x = boxSize/2; x < maxCoord; x+=100)
            {
                if (sensors.Any(s => Math.Abs(x - s.X) + Math.Abs(y - s.Y) <= s.Distance - boxSize)) continue;
                approxLocations.Add(new Point(x, y));
                //Console.WriteLine($"Possible match around ({x}, {y})");
            }
        }

        Console.WriteLine($"Found {approxLocations.Count} approx locations");

        foreach (var approxLocation in approxLocations)
        {
            for (var y = approxLocation.Y - boxSize/2; y < approxLocation.Y + (boxSize + 1)/2; y++)
            {
                for (var x = approxLocation.X - boxSize/2; x < approxLocation.X + (boxSize + 1)/2; x++)
                {
                    if (sensors.Any(s => s.beaconX == x && s.beaconY == y)) continue;
                    if (sensors.Any(s => Math.Abs(x - s.X) + Math.Abs(y - s.Y) <= s.Distance)) continue;
                    return (long) x * 4000000 + y;
                }
            }
        }

        return -1;
    }

    private Sensor Parse(string line)
    {
        var match = Regex.Match(line,
                "Sensor at x=(-?[0-9]*), y=(-?[0-9]*): closest beacon is at x=(-?[0-9]*), y=(-?[0-9]*)")
            .Groups
            .Values
            .Skip(1)
            .Select(g => int.Parse(g.Value))
            .ToList();

        var range = Math.Abs(match[0] - match[2]) + Math.Abs(match[1] - match[3]);
        return new Sensor(match[0], match[1], range, match[2], match[3]);
    }

    private record Sensor(int X, int Y, int Distance, int beaconX, int beaconY);
}