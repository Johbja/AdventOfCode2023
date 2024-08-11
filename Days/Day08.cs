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
    private List<Node> _nodeList = new();

    public Day08(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day08));
        _instructions = _input[0];



        foreach (var textNode in _input.Skip(2))
        {
            var nodes = textNode.Select((node, index) => (node, index))
                .Where(x => char.IsAsciiLetter(x.node))
                .OrderBy(x => x.index)
                .Select((x, i) => (firstIndex: x, secondIndex: i))
                .GroupBy(x => x.firstIndex.index - x.secondIndex)
                .ToDictionary(x => x.Key, x => x.ToList());

            var nodeName = new string(nodes[0].Select(x => x.firstIndex.node).ToArray());
            var nextLeftNode = new string(nodes[4].Select(x => x.firstIndex.node).ToArray());
            var nextRightNode = new string(nodes[6].Select(x => x.firstIndex.node).ToArray());

            var newNode = new Node(nodeName, nextLeftNode, nextRightNode);
            _nodeList.Add(newNode);
        }

        foreach (var node in _nodeList)
        {

            var left = node.NextNodeLeftName;
            var right = node.NextNodeRightName;

            node.NextNodeLeft = _nodeList.Find(x => x.NodeName == left);
            node.NextNodeRight = _nodeList.Find(x => x.NodeName == right);
        }

    }

    public void SolvePartOne()
    {
        var currentNode = _nodeList.Find(x => x.NodeName == "AAA");

        int stepCounter = 0;
        while (currentNode.NodeName != "ZZZ")
        {
            var currentInstuction = _instructions[stepCounter % _instructions.Length];

            if(currentInstuction == 'L')
            {
                currentNode = currentNode.NextNodeLeft;
            }
            else
            {
                currentNode = currentNode.NextNodeRight;
            }

            stepCounter++;
        }

        Console.WriteLine(stepCounter);
    }

    public void SolvePartTwo()
    {

        var currentNodes = _nodeList.Where(x => x.NodeName[2] == 'A').ToArray();

        int stepCounter = 0;
        while (currentNodes.Any(x => x.NodeName[2] != 'Z'))
        {
            var currentInstuction = _instructions[stepCounter % _instructions.Length];


            for(int i = 0; i < currentNodes.Length; i++)
            {
                if (currentInstuction == 'L')
                {
                    currentNodes[i] = currentNodes[i].NextNodeLeft;
                }
                else
                {
                    currentNodes[i] = currentNodes[i].NextNodeRight;
                }
            }

            stepCounter++;
        }

        Console.WriteLine(stepCounter);
    }
}

public class Node
{
    public Node NextNodeLeft { get; set; }
    public Node NextNodeRight { get; set; }

    public string NodeName { get; private set; }
    public string NextNodeLeftName { get; private set; }
    public string NextNodeRightName { get; private set; }

    public Node(string nodeName, string leftNodeName, string rightNodeName)
    {
        NodeName = nodeName;
        NextNodeLeftName = leftNodeName;
        NextNodeRightName = rightNodeName;
    }
}


