using YamlDotNet.Serialization;

namespace AdventOfCode;

public class Day11
{
    [TestCase("Day11ExampleInput.yml")]
    public void Part1(string file)
    {
        new DeserializerBuilder()
            .WithNamingConvention(new SpacesNamingConvention())
            .Build()
            .Deserialize<Dictionary<string, MonkeyBehaviour>>(File.ReadAllText(file));
    }

    private class MonkeyBehaviour
    {
        public string Starting_items;
        public string Operation;
        public Test Test;
    }

    private class Test
    {
        public string If_true;
        public string If_false;
    }
}

public class SpacesNamingConvention : INamingConvention
{
    public string Apply(string value) => value.Replace("_", " ");
}