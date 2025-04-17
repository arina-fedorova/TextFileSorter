namespace FileSorter;

public class ChunkMerger
{
    public static void Merge(IEnumerable<string> chunkFiles, string outputPath)
    {
        var readers = chunkFiles
            .Select(path => new StreamReader(path))
            .ToList();

        var comparer = new LineEntryComparer();
        var queue = new PriorityQueue<(LineEntry Entry, int Index), LineEntry>(comparer);

        for (var i = 0; i < readers.Count; i++)
        {
            if (readers[i].EndOfStream)
            {
                continue;
            }

            var line = readers[i].ReadLine();
            if (line != null)
            {
                var parsedLine = LineParser.Parse(line);
                queue.Enqueue((parsedLine, i), parsedLine);
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
                        queue.Enqueue((LineParser.Parse(nextLine), index), LineParser.Parse(nextLine));
                    }
                }
            }

            foreach (var r in readers)
            {
                r.Dispose();
            }
        }
    }
}