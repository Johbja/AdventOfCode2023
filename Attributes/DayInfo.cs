using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Attributes;
public class DayInfo : Attribute
{
    public string SolutionName { get; set; }
    public string Day { get; set; }

    public DayInfo(string solutionName, string day) 
    {
        SolutionName = solutionName;
        Day = day;
    }

}
