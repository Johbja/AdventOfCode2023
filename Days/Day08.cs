using AdventOfCode2023.Attributes;
using AdventOfCode2023.Intefaces;
using AdventOfCode2023.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode2023.Days;

[DayInfo("Haunted Wasteland", "Day 8")]
public class Day08 : ISolution
{
    private readonly string[] _input;
    private readonly string _instructions;

    Dictionary<string, (string left, string rigth)> _positionLookup = new();

    public Day08(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day08));
        _instructions = _input[0];

        foreach (var textNode in _input.Skip(2))
        {

            var keyValues = textNode.Replace("(", "").Replace(")", "").Split("=", StringSplitOptions.RemoveEmptyEntries);
            var values = keyValues[1].Split(",", StringSplitOptions.RemoveEmptyEntries);
            _positionLookup.Add(keyValues[0].Replace(" ", ""), (values[0].Replace(" ", ""), values[1].Replace(" ","")));
        }
    }

    private int GDC(int valueA, int valueB)
        => valueB == 0 ? valueA : GDC(valueB, valueA % valueB);
    

    private int LCM(int valueA, int valueB)
    {
        return valueA * valueB / GDC(valueA, valueB);
    }

    public void SolvePartOne()
    {
        string currentPosition = "AAA";
        int step = 0;

        while (currentPosition != "ZZZ")
        {
            var currentInstuction = _instructions[step % _instructions.Length];
            
            if (currentInstuction == 'L')
                currentPosition = _positionLookup[currentPosition].left;
            else
                currentPosition = _positionLookup[currentPosition].rigth;

            step++;
        }

        Console.WriteLine(step);
    }

    public void SolvePartTwo()
    {
        var currentPositions = _positionLookup
            .Where(entry => entry.Key.EndsWith('A'))
            .Select(entry => (entry.Key, steps:0))
            .ToArray();

        for (int i = 0; i < currentPositions.Length; i++)
        {
            string currentPosition = currentPositions[i].Key;
            int step = 0;

            while (!currentPosition.EndsWith('Z'))
            {
                var currentInstuction = _instructions[step % _instructions.Length];

                if (currentInstuction == 'L')
                    currentPosition = _positionLookup[currentPosition].left;
                else
                    currentPosition = _positionLookup[currentPosition].rigth;

                step++;
            }

            currentPositions[i] = (currentPositions[i].Key, step);
        }

        int result = currentPositions.Aggregate(1, (acc, steps) => LCM(acc, steps.steps));
        Console.WriteLine(result);
    }
}


