using AdventOfCode2023.Attributes;
using AdventOfCode2023.Intefaces;
using AdventOfCode2023.Services;
using System.Reflection;

namespace AdventOfCode2023;

public static class Program
{
    private static Dictionary<int, Type> _solutionRepository = new();

    static void Main(string[] args)
    {
        ConstructSolutionRepository();

        try
        {
            if(int.TryParse(args[0], out int day))
            {
                var currentDayToSolve = CreateInstanceOfSolution(day);
                Solve(currentDayToSolve);
            }
            else
            {
                Console.WriteLine("Could not parse input argument");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void ConstructSolutionRepository()
    {
        Assembly currentAssembly = Assembly.GetExecutingAssembly();

        Type interfaceType = typeof(ISolution);

        _solutionRepository = currentAssembly
            .GetTypes()
            .Where(type => type != null && interfaceType.IsAssignableFrom(type) && type != interfaceType)
            .OrderBy(type => type.Name)
            .ToDictionary(type => int.Parse(type.Name.Where(x => char.IsDigit(x)).Aggregate("", (a,b) => a + b.ToString())), type => type);
    }
    
    private static ISolution CreateInstanceOfSolution(int day)
    {
        if (Activator.CreateInstance(_solutionRepository[day], new LoadInputService()) is not ISolution instance)
            throw new ArgumentNullException(nameof(instance), $"instance of day {day} could not be created");

        return instance;
    }

    private static void Solve(ISolution solution)
    {
        DayInfo? dayInfo = solution.GetType().GetCustomAttribute<DayInfo>();
        string displayName = "";
        
        if (dayInfo is not null)
        {
            displayName = $"{dayInfo.Day}: {dayInfo.SolutionName}";
            Console.WriteLine(displayName);
            PrintSeparetor(displayName.Length);
        }

        solution.SolvePartOne();
        PrintSeparetor(displayName.Length);

        solution.SolvePartTwo();
        PrintSeparetor(displayName.Length);
    }

    private static void PrintSeparetor(int length)
    {
        Console.WriteLine(new string('-', length));
    }
}
