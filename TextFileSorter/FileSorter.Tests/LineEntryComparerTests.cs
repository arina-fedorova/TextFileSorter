using FluentAssertions;

namespace FileSorter.Tests;

public class LineEntryComparerTests
{
    private readonly LineEntryComparer _comparer = new();

    [Fact]
    public void ComparesByTextFirst()
    {
        var a = new LineEntry(10, "Apple");
        var b = new LineEntry(10, "Banana");

        _comparer.Compare(a, b).Should().BeLessThan(0);
    }

    [Fact]
    public void ComparesByNumberIfTextEqual()
    {
        var a = new LineEntry(5, "Apple");
        var b = new LineEntry(10, "Apple");

        _comparer.Compare(a, b).Should().BeLessThan(0);
    }
}