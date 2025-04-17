namespace FileSorter;

public class ChunkSorter
{
    private readonly LineEntryComparer _comparer = new();
    private readonly int _maxLinesPerChunk;

    public ChunkSorter(int maxLinesPerChunk = 1_000_000)
    {
        _maxLinesPerChunk = maxLinesPerChunk;
    }

    public List<string> ProcessChunks(string inputPath, string tempDirectory)
    {
        var chunkPaths = new List<string>();

        using var reader = new StreamReader(inputPath);
        var chunkIndex = 0;

        while (!reader.EndOfStream)
        {
            var entries = new List<LineEntry>(_maxLinesPerChunk);
            for (var i = 0; i < _maxLinesPerChunk && !reader.EndOfStream; i++)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                entries.Add(LineParser.Parse(line));
            }

            entries.Sort(_comparer);

            var chunkPath = Path.Combine(tempDirectory, $"chunk_{chunkIndex:D4}.txt");
            using var writer = new StreamWriter(chunkPath);
            foreach (var entry in entries)
            {
                writer.WriteLine(entry.ToString());
            }

            chunkPaths.Add(chunkPath);
            chunkIndex++;
        }

        return chunkPaths;
    }
}