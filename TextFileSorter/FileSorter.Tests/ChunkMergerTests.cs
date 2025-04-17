using FluentAssertions;

namespace FileSorter.Tests;

public class ChunkMergerTests
{
    [Fact]
    public void MergesSortedChunksCorrectly()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        var chunk1 = Path.Combine(tempDir, "chunk1.txt");
        var chunk2 = Path.Combine(tempDir, "chunk2.txt");
        var output = Path.Combine(tempDir, "output.txt");

        File.WriteAllLines(chunk1, [
            "1. Apple",
            "5. Banana"
        ]);

        File.WriteAllLines(chunk2, [
            "2. Apple",
            "10. Cherry"
        ]);

        var chunkFiles = new List<string> { chunk1, chunk2 };

        // Act
        ChunkMerger.Merge(chunkFiles, output);

        // Assert
        var result = File.ReadAllLines(output);
        result.Should().BeEquivalentTo([
            "1. Apple",
            "2. Apple",
            "5. Banana",
            "10. Cherry"
        ], options => options.WithStrictOrdering());

        // Cleanup
        File.Delete(chunk1);
        File.Delete(chunk2);
        File.Delete(output);
        Directory.Delete(tempDir);
    }
    
    [Fact]
    public void MergesEmptyChunksCorrectly()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        var chunk1 = Path.Combine(tempDir, "chunk1.txt");
        var chunk2 = Path.Combine(tempDir, "chunk2.txt");
        var output = Path.Combine(tempDir, "output.txt");

        File.WriteAllLines(chunk1, [
            "1. Apple",
            "5. Banana"
        ]);

        File.WriteAllLines(chunk2, []);

        var chunkFiles = new List<string> { chunk1, chunk2 };

        // Act
        ChunkMerger.Merge(chunkFiles, output);

        // Assert
        var result = File.ReadAllLines(output);
        result.Should().BeEquivalentTo([
            "1. Apple",
            "5. Banana"
        ], options => options.WithStrictOrdering());

        // Cleanup
        File.Delete(chunk1);
        File.Delete(chunk2);
        File.Delete(output);
        Directory.Delete(tempDir);
    }
    
    [Fact]
    public void MergesSingleChunkCorrectly()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        var chunk1 = Path.Combine(tempDir, "chunk1.txt");
        var output = Path.Combine(tempDir, "output.txt");

        File.WriteAllLines(chunk1, [
            "1. Apple",
            "5. Banana"
        ]);

        var chunkFiles = new List<string> { chunk1 };

        // Act
        ChunkMerger.Merge(chunkFiles, output);

        // Assert
        var result = File.ReadAllLines(output);
        result.Should().BeEquivalentTo([
            "1. Apple",
            "5. Banana"
        ], options => options.WithStrictOrdering());

        // Cleanup
        File.Delete(chunk1);
        File.Delete(output);
        Directory.Delete(tempDir);
    }
    
    [Fact]
    public void MergesNoChunksCorrectly()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        var output = Path.Combine(tempDir, "output.txt");

        var chunkFiles = new List<string>();

        // Act
        ChunkMerger.Merge(chunkFiles, output);

        // Assert
        File.Exists(output).Should().BeFalse("output file should not be created");

        // Cleanup
        Directory.Delete(tempDir);
    }
}