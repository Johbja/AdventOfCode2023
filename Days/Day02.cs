using AdventOfCode2023.Attributes;
using AdventOfCode2023.Intefaces;
using AdventOfCode2023.Services;

namespace AdventOfCode2023.Days;

[DayInfo("Name", "Day 2")]
internal class Day02 : ISolution
{
    private readonly string[] _input;

    public Day02(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day02));
    }

    public void SolvePartOne()
    {
        Console.WriteLine("p1");
    }

    public void SolvePartTwo()
    {
        Console.WriteLine("p2");
    }

}
