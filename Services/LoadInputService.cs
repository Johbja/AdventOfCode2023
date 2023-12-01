using System.Reflection;

namespace AdventOfCode2023.Services;

public class LoadInputService
{
    public string GetInput(string inputDay)
    {
        string assemblyLocation = Assembly.GetEntryAssembly().Location;
        string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
        string path = Path.Combine(assemblyDirectory, "Inputs", $"{inputDay}.txt");

        return File.ReadAllText(path);
    }

    public string[] GetInputAsLines(string inputDay)
    {
        string assemblyLocation = Assembly.GetEntryAssembly().Location;
        string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
        string path = Path.Combine(assemblyDirectory, "Inputs", $"{inputDay}.txt");

        return File.ReadAllLines(path);
    }
}
