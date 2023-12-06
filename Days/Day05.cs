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

        long max = long.MaxValue;
        foreach(var pair in srcPairs)
        {
            var seeds = LongRange(pair[0], pair[1]).ToArray();
            var resutl = CalculateMinSeedLocation(seeds);

            if(resutl < max)
                max = resutl;
        }

        Console.WriteLine(max);
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

