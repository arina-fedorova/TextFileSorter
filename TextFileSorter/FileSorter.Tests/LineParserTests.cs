using FluentAssertions;

namespace FileSorter.Tests;

public class LineParserTests
{
    [Theory]
    [InlineData("42. Apple", 42, "Apple")]
    [InlineData("1. Banana is yellow", 1, "Banana is yellow")]
    public void ParsesCorrectly(string input, int expectedNumber, string expectedText)
    {
        var result = LineParser.TryParse(input, out var entry);

        entry.Should().NotBeNull();
        entry.Number.Should().Be(expectedNumber);
        entry.Text.Should().Be(expectedText);
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("Wrong format")]
    [InlineData("1 Banana")]
    public void ParsesIncorrectly(string input)
    {
        var result = LineParser.TryParse(input, out var entry);

        entry.Should().BeNull();
        result.Should().BeFalse();
    }


    [Fact]
    public void ThrowsOnInvalidFormat()
    {
        var act = () => LineParser.Parse("NoDotSeparator");

        act.Should().Throw<FormatException>();
    }

    [Fact]
    public void ThrowsOnEmptyString()
    {
        var act = () => LineParser.Parse("");

        act.Should().Throw<FormatException>();
    }
}