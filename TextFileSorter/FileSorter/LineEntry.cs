namespace FileSorter;

public class LineEntry
{
    public LineEntry(int number, string text)
    {
        Number = number;
        Text = text;
    }

    public int Number { get; }
    public string Text { get; }

    public override string ToString()
    {
        return $"{Number}. {Text}";
    }
}