namespace FileSorter;

public class ChunkMerger
{
    public static void Merge(IEnumerable<string> chunkFiles, string outputPath)
    {
        if (chunkFiles.Count() == 0)
        {
            return;
        }

        var readers = chunkFiles
            .Select(path => new StreamReader(path))
            .ToList();

        var comparer = new LineEntryComparer();
        var queue = new PriorityQueue<(LineEntry Entry, int Index), LineEntry?>(comparer);

        for (var i = 0; i < readers.Count; i++)
        {
            if (readers[i].EndOfStream) continue;

            var line = readers[i].ReadLine();
            if (line != null)
            {
                if (LineParser.TryParse(line, out var parsedLine))
                {
                    queue.Enqueue((parsedLine, i), parsedLine);
                }
            }
        }

        using var writer = new StreamWriter(outputPath);

        while (queue.Count > 0)
        {
            var (entry, index) = queue.Dequeue();
            writer.WriteLine(entry.ToString());

            if (!readers[index].EndOfStream)
            {
                var nextLine = readers[index].ReadLine();
                if (nextLine != null)
                {
                    if (LineParser.TryParse(nextLine, out var parsedLine))
                    {
                        queue.Enqueue((parsedLine, index), parsedLine);
                    }
                }
            }
        }

        foreach (var r in readers)
        {
            r.Dispose();
        }
    }
}