using AdventOfCode2023.Attributes;
using AdventOfCode2023.Intefaces;
using AdventOfCode2023.Services;

namespace AdventOfCode2023.Days;

[DayInfo("Name of problem", "Day 1")]
public class Day01 : ISolution
{
    private string _input = "";
    
    public Day01(LoadInputService inputService)
    {
        _input = inputService.GetInput(nameof(Day01));
    }

    public void SolvePartOne()
    {
        Console.WriteLine(_input);
    }

    public void SolvePartTwo()
    {
        Console.WriteLine("p2");
    }
}
