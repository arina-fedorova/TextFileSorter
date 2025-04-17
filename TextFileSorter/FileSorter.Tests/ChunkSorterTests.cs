using FluentAssertions;

namespace FileSorter.Tests;

public class ChunkSorterTests
{
    [Fact]
    public void SortsSmallFileCorrectly()
    {
        // Arrange
        var lines = new[]
        {
            "20. Banana",
            "10. Apple",
            "5. Banana"
        };

        var inputPath = Path.GetTempFileName();
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        File.WriteAllLines(inputPath, lines);

        // Act
        var sorter = new ChunkSorter(maxLinesPerChunk: 10);
        var chunks = sorter.ProcessChunks(inputPath, tempDir);

        // Assert
        var sortedLines = File.ReadAllLines(chunks[0]);
        sortedLines.Should().BeEquivalentTo([
            "10. Apple",
            "5. Banana",
            "20. Banana"
        ], options => options.WithStrictOrdering());

        // Сleanup
        File.Delete(inputPath);
        foreach (var file in chunks)
        {
            File.Delete(file);
        }
        Directory.Delete(tempDir);
    }

    [Fact]
    public void SortsSkipIncorrectLines()
    {
        // Arrange
        var lines = new[]
        {
            "20. Banana",
            "wrong string format",
            "5. Banana"
        };

        var inputPath = Path.GetTempFileName();
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        File.WriteAllLines(inputPath, lines);

        // Act
        var sorter = new ChunkSorter(maxLinesPerChunk: 10);
        var chunks = sorter.ProcessChunks(inputPath, tempDir);

        // Assert
        var sortedLines = File.ReadAllLines(chunks[0]);
        sortedLines.Should().BeEquivalentTo([
            "5. Banana",
            "20. Banana"
        ], options => options.WithStrictOrdering());

        // Сleanup
        File.Delete(inputPath);
        foreach (var file in chunks)
        {
            File.Delete(file);
        }
        Directory.Delete(tempDir);
    }
}