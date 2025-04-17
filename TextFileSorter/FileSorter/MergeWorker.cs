namespace FileSorter;

public class MergeWorker
{
    public static List<string> MergeGroups(List<string> chunkFiles, int groupSize, string tempDir)
    {
        var tasks = new List<Task<string>>();

        foreach (var (group, groupIndex) in chunkFiles.Chunk(groupSize).Select((g, i) => (g, i)))
        {
            var groupList = group.ToList();
            var intermediatePath = Path.Combine(tempDir, $"merge_{groupIndex:D4}.txt");

            var task = Task.Run(() =>
            {
                ChunkMerger.Merge(groupList, intermediatePath);
                return intermediatePath;
            });

            tasks.Add(task);
        }

        Task.WaitAll(tasks.ToArray());

        return tasks.Select(t => t.Result).ToList();
    }
}