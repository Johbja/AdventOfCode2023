using AdventOfCode2023.Attributes;
using AdventOfCode2023.Intefaces;
using AdventOfCode2023.Services;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days;

[DayInfo("Trebuchet?!", "Day 1")]
public class Day01 : ISolution
{
    private readonly string[] _input;

    public Day01(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day01));
    }

    public void SolvePartOne()
    {
        int sum = 0;
        foreach (var row in _input)
        {
            var matches = Regex.Matches(row, @"\d");
            sum += int.Parse($"{matches[0].Value}{matches[^1].Value}");
        }

        Console.WriteLine(sum);
    }

    public void SolvePartTwo()
    {
        int sum = 0;

        for (int i = 0; i < _input.Length; i++)
        {
            string row = _input[i];

            row = row.Replace("one", "o1e")
                .Replace("two", "t2o")
                .Replace("three", "t3e")
                .Replace("four", "f4r")
                .Replace("five", "f5e")
                .Replace("six", "s6x")
                .Replace("seven", "s7n")
                .Replace("eight", "e8t")
                .Replace("nine", "n9e");

            var matches = Regex.Matches(row, @"\d");
            var fistMatch = matches[0];
            var lastMatch = matches[^1];

            sum += int.Parse($"{fistMatch}{lastMatch}");
        }

        Console.WriteLine(sum);
    }

}
