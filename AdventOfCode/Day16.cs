namespace AdventOfCode;

public class Day16
{
    [Test]
    public void Part1()
    {
        var input = File.ReadAllText("Day16Input.txt");

        var nodes = input.Split(Environment.NewLine).Select(line => new Valve(line)).ToDictionary(n => n.Id);

        foreach (var node in nodes.Values.Where(IsNonTrivial).OrderByDescending(x => x.Tunnels.Count()))
        {
            Console.Write($"Valve {node.Id} has flow rate={node.FlowRate}; tunnels lead to valves ");
            
            foreach (var tunnel in node.Tunnels)
            {
                var prevTunnel = node.Id;
                var thisTunnel = tunnel;
                var distance = 1;
                while (!IsNonTrivial(nodes[thisTunnel]))
                {
                    var nextTunnel = nodes[thisTunnel].Tunnels.Single(t => t != prevTunnel);
                    prevTunnel = thisTunnel;
                    thisTunnel = nextTunnel;
                    distance++;
                }

                node.UsefulTunnels.Add((distance, thisTunnel));
                Console.Write($"{thisTunnel}@{distance} ");
            }
            
            Console.WriteLine();
        }

        // Guess computed the sad way, by drawing the graph on a piece of paper
        // and tracing a path by hand
        var myGuess = @"XQ VP VM TR VM DO KI HN".Split(" ").ToList();
        var opened = new HashSet<string>();
        var time = 0;
        var released = 0;
        var releaseRate = 0;
        var prevNode = "AA";
        foreach (var node in myGuess)
        {
            var distance = nodes[node].UsefulTunnels.Single(t => t.destination == prevNode).distance;
            if (time + distance > 30) break;
            time += distance;
            released += distance * releaseRate;
            Console.WriteLine($"t={time} Moved from {prevNode} to {node}. Valves {string.Join(", ", opened)} are open, releasing {releaseRate}/min for {released} total pressure.");
            prevNode = node;

            if (!opened.Contains(node))
            {
                time++;
                released += releaseRate;
                opened.Add(node);
                releaseRate += nodes[node].FlowRate;
                Console.WriteLine($"t={time} Opened valve {node}");
            }
            else
            {
                Console.WriteLine($"t={time} Valve {node}already open.");
            }
        }

        var remainingTime = 30 - time;
        released += releaseRate * remainingTime;
        Console.WriteLine($"t=30 Time up! Total released: {released}");
    }

    private bool IsNonTrivial(Valve valve) => valve.FlowRate > 0 || valve.Tunnels.Count() > 2;
}

public class Valve
{
    public Valve(string line)
    {
        var parts = line.Split(new[] {' ', '=', ';', ','}, StringSplitOptions.RemoveEmptyEntries);
        Id = parts[1];
        FlowRate = int.Parse(parts[5]);
        Tunnels = parts.Skip(10).ToArray();
    }
    
    public string Id { get; }
    public int FlowRate { get; }
    public IEnumerable<string> Tunnels { get; }
    public List<(int distance, string destination)> UsefulTunnels { get; } = new();
}