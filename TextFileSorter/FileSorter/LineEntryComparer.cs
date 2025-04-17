namespace FileSorter;

public class LineEntryComparer : IComparer<LineEntry>
{
    public int Compare(LineEntry? x, LineEntry? y)
    {
        if (x == null || y == null)
        {
            return 0;
        }

        var textCompare = string.Compare(x.Text, y.Text, StringComparison.Ordinal);
        if (textCompare != 0)
        {
            return textCompare;
        }

        return x.Number.CompareTo(y.Number);
    }
}