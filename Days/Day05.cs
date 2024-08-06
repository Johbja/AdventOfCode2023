using AdventOfCode2023.Attributes;
using AdventOfCode2023.Intefaces;
using AdventOfCode2023.Services;
using System.Data;

namespace AdventOfCode2023.Days;

[DayInfo("If You Give A Seed A Fertilizer", "Day 5")]
public class Day05 : ISolution
{
    private readonly string _input;
    private readonly long[] seeds;
    private readonly Dictionary<string, long[][]> maps;

    public Day05(LoadInputService inputService)
    {
        _input = inputService.GetInput(nameof(Day05));
        string[] sections = _input.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);
        seeds = sections[0]
            .Split(":")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();

        maps = sections.Skip(1).Select(section => section.Split(":"))
            .Select
            (
                map =>
                (
                    mapnName: map[0],
                    values: map[1].Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
                        .Select
                        (
                            row => row.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                            .Select(long.Parse)
                            .ToArray()
                        )
                        .ToArray()
                 )
            )
            .ToDictionary(key => key.mapnName, value => value.values);
    }

    public void SolvePartOne()
    {
        long result = CalculateMinSeedLocation(seeds);
        Console.WriteLine(result);
    }

    private IEnumerable<long> LongRange(long start, long count)
    {
        long end = start + count;
        while (end > start)
        {
            yield return start++;
        }
    }

    public void SolvePartTwo()
    {
        var srcPairs = seeds.Chunk(2).ToList();
        var seedRanges = srcPairs.Select(pair => (start: pair[0], end: pair[0] + pair[1] - 1)).ToArray();
        var mapRanges = maps.ToDictionary(key => key.Key, value => value.Value.Select(row => (dest: (start: row[0], end: row[0] + row[2] - 1), src: (start: row[1], end: row[1] + row[2] - 1))).ToArray());

        Console.WriteLine(seedRanges.Select(x => x.end - x.start).Sum());

        long min = long.MaxValue;
        foreach (var (start, end) in seedRanges)
        {
            List<(long start, long end)> currentRanges = new()
            {
                (start, end)
            };

            foreach (var key in mapRanges.Keys)
            {
                foreach (var (dest, src) in mapRanges[key])
                {
                    int iterator = currentRanges.Count;
                    for (int i = 0; i < iterator; i++)
                    {
                        if (currentRanges[i].end > src.end)
                        {
                            var newRangeStart = Math.Abs(src.end - currentRanges[i].end);
                            currentRanges.Add((newRangeStart, currentRanges[i].end));
                            iterator++;
                            currentRanges[i] = (currentRanges[i].start, src.end);
                        }

                        if (currentRanges[i].start < src.start)
                        {
                            var newRangeEnd = Math.Abs(currentRanges[i].start - src.start);
                            currentRanges.Add((currentRanges[i].start, newRangeEnd));
                            iterator++;
                            currentRanges[i] = (src.start, currentRanges[i].end);
                        }

                        var rangeOffset = Math.Abs(src.start - currentRanges[i].start);
                        var mappedStartValue = dest.start + rangeOffset;

                        rangeOffset = Math.Abs(src.start - currentRanges[i].end);
                        var mappedEndValue = dest.start + rangeOffset;

                        currentRanges[i] = (mappedStartValue, mappedEndValue);
                    }
                }
            }

            var currentMin = currentRanges.Select(x => x.start < x.end ? x.start : x.end).Min();
            min = currentMin < min ? currentMin : min;
        }

        Console.WriteLine(min);
    }

    private long CalculateMinSeedLocation(long[] currentSrc)
    {
        foreach (var key in maps.Keys)
        {

            long len = currentSrc.Length;
            for (long i = 0; i < len; i++)
            {
                var validMaps = maps[key].Where(row => currentSrc[i] >= row[1] && currentSrc[i] < (row[1] + row[2])).ToList();

                if (!validMaps.Any())
                {
                    continue;
                }

                var validMap = validMaps.Single();

                var offset = Math.Abs((validMap[1] - currentSrc[i]));
                currentSrc[i] = validMap[0] + offset;

            }
        }

        var result = currentSrc.Min();
        return result;
    }
}

