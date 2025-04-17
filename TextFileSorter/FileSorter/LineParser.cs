namespace FileSorter;

public class LineParser
{
    public static LineEntry Parse(string line)
    {
        var dotIndex = line.IndexOf('.');
        if (dotIndex == -1)
        {
            throw new FormatException($"Invalid format: {line}");
        }

        var number = int.Parse(line.Substring(0, dotIndex));
        var text = line.Substring(dotIndex + 1).Trim();

        return new LineEntry(number, text);
    }
}