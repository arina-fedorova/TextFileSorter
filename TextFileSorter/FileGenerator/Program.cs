using FileGenerator;

const long bytesInMegabyte = 1024L * 1024L;

Console.WriteLine("Enter the output directory path:");
var outputPath = Console.ReadLine();

if (string.IsNullOrWhiteSpace(outputPath) ||
    !Directory.Exists(outputPath))
{
    Console.WriteLine("Invalid directory path.");
    return;
}

Console.WriteLine("Enter the file size in MB:");
var sizeInMb = int.TryParse(Console.ReadLine(), out var size) ? size : 0;

if (sizeInMb == 0)
{
    Console.WriteLine("Invalid size.");
    return;
}

if (!Directory.Exists(outputPath))
{
    Directory.CreateDirectory(outputPath);
}

var targetSizeBytes = sizeInMb * bytesInMegabyte;

var generator = new Generator(new StringFactory());
generator.Generate(outputPath, targetSizeBytes);

Console.WriteLine($"File generated at {outputPath}");