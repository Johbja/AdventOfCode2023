using AdventOfCode2023.Attributes;
using AdventOfCode2023.Intefaces;
using AdventOfCode2023.Services;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;

namespace AdventOfCode2023.Days;

[DayInfo("Camel Cards", "Day 7")]
internal class Day07 : ISolution
{
    private readonly string[] _input;
    private readonly List<(int[] hand, int bid)> hands;

    private readonly Dictionary<string, int> groupRanks = new()
    {
        {
            "5", 5
        },
        {
            "41", 4
        },
        {
            "32", 3
        },
        {
            "31", 3
        },
        {
            "22", 2
        },
        {
            "21", 1
        },
        {
            "1", 0
        }
    };

    public Day07(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day07));
        hands = _input.Select(line => ParseHand(line.Split(" ", StringSplitOptions.RemoveEmptyEntries))).ToList();

    }

    private (int[] hand, int bid) ParseHand(string[] inputLine)
        => (inputLine[0].Select(ConvertCard).ToArray(), int.Parse(inputLine[1]));

    private int ConvertCard(char card)
        => card switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => 11,
            'T' => 10,
            _ => int.Parse(card.ToString())
        };

    public void SolvePartOne()
    {
        var handRanks = hands.Select((hand, index) =>
        {
            var cardGroups = hand.hand
            .GroupBy(card => card)
            .GroupBy(g => g.Count()).Select(x => x.Key)
            .OrderByDescending(x => x)
            .Aggregate("", (a, b) => a + b);

            var rank = groupRanks[cardGroups];

            return (rank, hand);
        })
        .GroupBy(hand => hand.rank)
        .ToDictionary(hand => hand.Key, hand => hand.ToList());

        foreach (var item in handRanks)
        {
            item.Value.Sort(handComparer);
        }

        var result = handRanks.OrderBy(x => x.Key)
            .SelectMany(x => x.Value)
            .Select((value, index) => value.hand.bid * (index + 1))
            .Sum();

        Console.WriteLine(result);
    }

    private readonly Comparison<(int rank,(int[] cards, int bid) hand)> handComparer = (a, b) =>
    {
        for (int i = 0; i < a.hand.cards.Length; i++)
        {
            if (a.hand.cards[i] > b.hand.cards[i])
                return 1;
            if (a.hand.cards[i] < b.hand.cards[i])
                return -1;
        }

        return 0;
    };

    public void SolvePartTwo()
    {
        Console.WriteLine("");
    }


}
