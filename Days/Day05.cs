using AdventOfCode2023.Attributes;
using AdventOfCode2023.Intefaces;
using AdventOfCode2023.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days;

[DayInfo("Name", "Day 5")]
public class Day05 : ISolution
{
    private readonly string[] _input;

    public Day05(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day05));
    }

    public void SolvePartOne()
    {
        Console.WriteLine("");
    }

    public void SolvePartTwo()
    {
        Console.WriteLine("");
    }
}
