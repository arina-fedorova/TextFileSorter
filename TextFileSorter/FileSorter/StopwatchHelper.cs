using System.Diagnostics;

namespace FileSorter;

public static class StopwatchHelper
{
    public static T Measure<T>(string label, Func<T> action)
    {
        var sw = Stopwatch.StartNew();
        var result = action();
        sw.Stop();
        Console.WriteLine($"{label}: {sw.Elapsed}");
        return result;
    }

    public static void Measure(string label, Action action)
    {
        var sw = Stopwatch.StartNew();
        action();
        sw.Stop();
        Console.WriteLine($"{label}: {sw.Elapsed}");
    }
}