using System.Text;

namespace FileGenerator;

public class Generator
{
    private const string FileName = "generated_file.txt";
    private const int OneMegabyte = 1024 * 1024;
    private readonly StringFactory _stringFactory;

    public Generator(StringFactory stringFactory)
    {
        _stringFactory = stringFactory;
    }

    public void Generate(string outputPath, long targetSizeBytes)
    {
        var fullPath = Path.Combine(outputPath, FileName);
        using var writer = new StreamWriter(fullPath, false, Encoding.UTF8, bufferSize: OneMegabyte);

        long currentSize = 0;
        var currentLineNumber = 1;
        while (currentSize < targetSizeBytes)
        {
            var text = _stringFactory.GetRandomString();
            var line = $"{currentLineNumber}. {text}";
            writer.WriteLine(line);

            currentSize += Encoding.UTF8.GetByteCount(line + Environment.NewLine);
            currentLineNumber++;
        }
    }
}