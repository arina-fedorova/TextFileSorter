using FileSorter;
using FluentAssertions;

namespace FileGenerator.Tests;

public class GeneratorTests
{
    [Fact]
    public void GenerateCreatesOutputFileWithExpectedSizeAndFormat()
    {
        // Arrange
        const long targetSizeBytes = 100 * 1024; // 100KB
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        var fakeStrings = new[] { "Apple", "Banana", "Cherry" };
        var factory = new StringFactoryStub(fakeStrings);
        var generator = new Generator(factory);

        // Act
        generator.Generate(tempDir, targetSizeBytes);

        // Assert
        var filePath = Path.Combine(tempDir, "generated_file.txt");

        File.Exists(filePath).Should().BeTrue("file should be created");
        new FileInfo(filePath).Length.Should().BeGreaterThanOrEqualTo(targetSizeBytes);

        var lines = File.ReadAllLines(filePath);
        lines.Should().NotBeEmpty();

        foreach (var line in lines)
        {
            var result = LineParser.TryParse(line, out var entry);
            
            result.Should().BeTrue();
            entry.Should().NotBeNull();
            entry.Text.Should().BeOneOf(fakeStrings);
        }

        // Cleanup
        File.Delete(filePath);
        Directory.Delete(tempDir);
    }
}