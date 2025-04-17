# TextFileSorter

**TextFileSorter** is a high-performance utility for generating and sorting large text files (up to 100GB and more) using external merge sort, multithreading, and efficient memory management in C#.

## 📄 File Format

Each line in the file has the format:

```
<Number>. <String>
```

Example:

```
415. Apple
416. Apple
417. Banana is yellow
```

## ✅ Sorting Rules

1. Alphabetical by the **string** part
2. If strings are equal, then by **number** (ascending)

## 🧩 Solution Overview

The solution consists of two console applications:

### 1. **FileGenerator**

Generates a large test file with the specified size.

```bash
dotnet run --project FileGenerator
```

### 2. **FileSorter**

Sorts the generated file using chunked in-memory sorting + external k-way merge.

```bash
dotnet run --project FileSorter
```

## ⚙️ Architecture Highlights

- External merge sort for huge files
- Chunked sorting with custom comparer
- Parallel merge groups for performance
- Streamed reading/writing with memory control
- Clean testable code with unit tests

## 🧪 Test Coverage

- Fully covered with `xUnit` tests
- Uses FluentAssertions for expressive assertions
- Includes real merge tests with temporary files

## 🛠 Technologies

- .NET 9
- C# 12
- xUnit + FluentAssertions
- PriorityQueue, StreamReader/Writer

## 📂 Folder Structure

```
TextFileSorter/
├── FileGenerator/
├── FileGenerator.Tests/
├── FileSorter/
├── FileSorter.Tests/

```

---

## 🧼 Clean-Up

Temporary chunk files (`chunk_*.txt`, `merge_*.txt`) are automatically removed after sorting.
