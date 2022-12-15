using System.Text.RegularExpressions;
using System.Xml.Xsl;

namespace AdventOfCode;

public class Day15
{
    [TestCase(@"Sensor at x=0, y=0: closest beacon is at x=1, y=1", 0, ExpectedResult = 5)]
    [TestCase(@"Sensor at x=9, y=16: closest beacon is at x=10, y=16", 10, ExpectedResult = 0)]
    [TestCase(@"Sensor at x=2, y=18: closest beacon is at x=-2, y=15
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
Sensor at x=20, y=1: closest beacon is at x=15, y=3", 10, ExpectedResult = 26, TestName = "Example input")]
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