﻿namespace AdventOfCode;

[TestFixture]
public class Day7
{
    private string m_Path;
    private IDictionary<string, long> m_FileSizes;

    [Test]
    public void Part1()
    {
        m_Path = ":";
        m_FileSizes = new Dictionary<string, long>();
        var lines = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k".Split(Environment.NewLine);
        
        lines = File.ReadAllLines("Day7Input.txt");

        foreach (var line in lines)
        {
            if (line.StartsWith("$ "))
            {
                ProcessCommand(line);
            }
            else
            {
                ProcessCommandOutput(line);
            }
        }

        foreach (var kvp in m_FileSizes)
        {
            Console.WriteLine($"{kvp.Key} is of size {kvp.Value}");
        }

        var allDirs = m_FileSizes.GroupBy(f => Parent(f.Key));
        foreach (var dir in allDirs)
        {
            if (SizeWithDescendants(dir.Key) > 100000) continue;
            Console.WriteLine($"Dir {dir.Key} is of size {SizeWithDescendants(dir.Key)}");
        }
        
        var result = allDirs.Select(g => SizeWithDescendants(g.Key))
            .Where(s => s <= 100000)
            .Sum();
        
        Console.WriteLine(result);
    }

    private long SizeWithDescendants(string dir)
    {
        return m_FileSizes
            .Where(kvp => kvp.Key.StartsWith(dir + '/'))
            .Sum(kvp => kvp.Value);
    }

    private void ProcessCommandOutput(string line)
    {
        var parts = line.Split(" ");
        if (parts[0] == "dir")
        {
            
        }
        else if (!long.TryParse(parts[0], out var size))
        {
            throw new Exception("Could not parse long in " + line);
        }
        else
        {
            m_FileSizes.Add(m_Path + "/" + parts[1], size);
        }
    }

    private void ProcessCommand(string line)
    {
        switch (line)
        {
            case "$ ls":
                break;
            case "$ cd /":
                m_Path = ":";
                break;
            case "$ cd ..":
                m_Path = Parent(m_Path);
                break;
            default:
            {
                if (!line.StartsWith("$ cd "))
                {
                    throw new Exception("Unexpected command " + line);
                }

                var subDir = line.Split(" ")[2];
                m_Path = m_Path + "/" + subDir;
                
                break;
            }
        }
    }

    private static string Parent(string path) => string.Join("/", path.Split("/").SkipLast(1));
}