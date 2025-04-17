using System.Diagnostics;
using FileSorter;

const string outputFileName = "sorted_file.txt";

Console.WriteLine("Enter the input file path:");
var inputPath = Console.ReadLine();

if (string.IsNullOrWhiteSpace(inputPath) ||
    !File.Exists(inputPath))
{
    Console.WriteLine("Invalid input file path.");
    return;
}

var outputPath = $"{Directory.GetParent(inputPath)!.FullName}/{outputFileName}";
var tempDir = Path.Combine(Path.GetDirectoryName(outputPath) ?? ".", "temp_chunks");

Directory.CreateDirectory(tempDir);

var stopwatch = Stopwatch.StartNew();

Console.WriteLine("Sorting chunks...");
var sorter = new ChunkSorter();
var chunkFiles = sorter.ProcessChunks(inputPath, tempDir);

Console.WriteLine($"{chunkFiles.Count} sorted chunk(s) created.");

Console.WriteLine("Parallel merging...");
var intermediateFiles = MergeWorker.MergeGroups(chunkFiles, groupSize: 4, tempDir);

Console.WriteLine("Final merge...");
ChunkMerger.Merge(intermediateFiles, outputPath);

stopwatch.Stop();
Console.WriteLine($"Done! Sorted file: {outputPath}");
Console.WriteLine($"Total time: {stopwatch.Elapsed}");

Directory.Delete(tempDir, recursive: true);