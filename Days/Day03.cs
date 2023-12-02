using AdventOfCode2023.Attributes;
using AdventOfCode2023.Intefaces;
using AdventOfCode2023.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days;

[DayInfo("Name", "Day 3")]
public class Day03 : ISolution
{
    private readonly string[] _input;

    public Day03(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day02));
    }

    public void SolvePartOne()
    {
        Console.WriteLine("p1");
    }

    public void SolvePartTwo()
    {
        Console.WriteLine("p1");
    }
}
