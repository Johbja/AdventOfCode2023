using AdventOfCode2023.Attributes;
using AdventOfCode2023.Intefaces;
using AdventOfCode2023.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days;

[DayInfo("Scratchcards", "Day 6")]
public class Day06 : ISolution
{
    private readonly string[] _input;

    public Day06(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day06));
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
