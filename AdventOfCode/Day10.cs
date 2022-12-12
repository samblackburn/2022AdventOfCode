namespace AdventOfCode;

public class Day10
{
    private int m_Clock;
    private int m_Register;
    private int m_Result;

    [SetUp]
    public void Init()
    {
        m_Clock = 0;
        m_Register = 1;
        m_Result = 0;
    }
    
    [TestCase("Day10ExampleInput.txt", ExpectedResult = 13140)]
    [TestCase("Day10Input.txt", ExpectedResult = 14820)]
    public int Part1(string fileName)
    {
        var input = File.ReadAllLines(fileName);
        foreach (var line in input)
        {
            if (line == "noop")
            {
                Increment();
            }
            else
            {
                Increment();
                Increment();
                m_Register += int.Parse(line.Split(" ")[1]);
            }
        }

        return m_Result;
    }
    

    private void Increment()
    {
        m_Clock++;
        if (m_Clock % 40 == 20)
        {
            var strength = m_Clock * m_Register;
            Console.WriteLine($"{m_Clock} * {m_Register} = {strength}");
            m_Result += strength;
        }
    }
}