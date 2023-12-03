using AdventOfCode2023.Attributes;
using AdventOfCode2023.Intefaces;
using AdventOfCode2023.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days;

[DayInfo("Name", "Day 4")]
public class Day04 : ISolution
{
    private readonly string[] _input;

    public Day04(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day04));
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
