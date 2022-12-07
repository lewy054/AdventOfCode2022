var file = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");

PartOne();
PartTwo();

void PartOne()
{
    const int mostTotalSize = 100000;
    var directoryTree = CreateDirectoryTree(file);
    var sum = 0;
    foreach (var directory in directoryTree)
    {
        var inner = directoryTree.Where(e => e.Key.StartsWith(directory.Key)).ToList();
        var totalSize = inner.Sum(e => e.Value);
        if (totalSize <= mostTotalSize)
        {
            sum += totalSize;
        }
    }

    Console.WriteLine(sum);
}

void PartTwo()
{
    const int totalSpace = 70000000;
    const int updatedSize = 30000000;
    var directoryTree = CreateDirectoryTree(file);
    var usedSpaces = directoryTree.Sum(e => e.Value);
    var spaceAvailable = totalSpace - usedSpaces;
    var neededExtraSpace = updatedSize - spaceAvailable;
    var directoriesToDelete = new List<int>();
    foreach (var directory in directoryTree)
    {
        var directorySize = directoryTree.Where(e => e.Key.StartsWith(directory.Key))
            .ToList()
            .Sum(e => e.Value);
        if (directorySize >= neededExtraSpace)
        {
            directoriesToDelete.Add(directorySize);
        }
    }

    Console.WriteLine(directoriesToDelete.Min());
}

Dictionary<string, int> CreateDirectoryTree(string filePath)
{
    var directoryTree = new Dictionary<string, int>();
    var rootPath = "/";
    directoryTree.Add("/", 0);
    foreach (var line in File.ReadLines(filePath).Skip(1))
    {
        if (line.StartsWith("$ cd"))
        {
            var destination = line.Split(" ")[2];
            if (destination == "..")
            {
                var t = rootPath.Split("/");
                var x = string.Join("/", t.Take(t.Length - 1));
                rootPath = x;
            }
            else
            {
                rootPath += '/' + destination;
                directoryTree.TryAdd(rootPath, 0);
            }
        }
        else if (line.StartsWith("$ ls"))
        {
            continue;
        }
        else
        {
            if (line.StartsWith("dir"))
            {
                continue;
            }

            var pathSize = directoryTree.GetValueOrDefault(rootPath);
            directoryTree[rootPath] = pathSize + int.Parse(line.Split(" ")[0]);
        }
    }

    return directoryTree;
}