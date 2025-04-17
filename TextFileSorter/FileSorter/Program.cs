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

var sorter = new ChunkSorter();

var chunkFiles = StopwatchHelper.Measure("Sorting chunks", () =>
    sorter.ProcessChunks(inputPath, tempDir)
);

var intermediateFiles = StopwatchHelper.Measure("Parallel merging", () =>
    MergeWorker.MergeGroups(chunkFiles, groupSize: 4, tempDir)
);

StopwatchHelper.Measure("Final merge", () =>
    ChunkMerger.Merge(intermediateFiles, outputPath)
);

StopwatchHelper.Measure("Cleanup", () =>
    Directory.Delete(tempDir, recursive: true)
);