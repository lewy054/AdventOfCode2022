var file = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");

var allLines = File.ReadAllLines(file);
PartOne(allLines);
PartTwo(allLines);

void PartOne(string[] lines)
{
    var visibleTrees = 0;
    for (int rowIndex = 0; rowIndex < lines.Length; rowIndex++)
    {
        var line = lines[rowIndex];
        for (int columnIndex = 0; columnIndex < line.Length; columnIndex++)
        {
            var checkedTreeSize = int.Parse(line[columnIndex].ToString());
            Task<bool>[] taskArray =
            {
                CheckIfTreeVisibleFromOneSide(0, columnIndex, checkedTreeSize, line), //left
                CheckIfTreeVisibleFromOneSide(columnIndex + 1, line.Length, checkedTreeSize, line), //right
                CheckIfTreeVisibleFromTopOrBottom(0, rowIndex, columnIndex, checkedTreeSize, lines), //top
                CheckIfTreeVisibleFromTopOrBottom(rowIndex + 1, lines.Length,
                    columnIndex, checkedTreeSize, lines) //bottom
            };
            Task.WhenAll(taskArray);

            if (taskArray.Any(e => e.Result))
            {
                visibleTrees++;
            }
        }
    }

    Console.WriteLine(visibleTrees);
}

void PartTwo(string[] lines)
{
    var highestScenicScore = 0;
    for (int rowIndex = 0; rowIndex < lines.Length; rowIndex++)
    {
        var line = lines[rowIndex];
        for (int columnIndex = 0; columnIndex < line.Length; columnIndex++)
        {
            var checkedTreeSize = int.Parse(line[columnIndex].ToString());
            
            var treesAtLeft = line[..columnIndex].Reverse().ToList();
            var visibleTreesLeft = GetVisibleTrees(treesAtLeft, checkedTreeSize);
            var visibleTreesLeftCount = CountVisibleTrees(checkedTreeSize, treesAtLeft, visibleTreesLeft);
            
            var treesAtRight = line[(columnIndex + 1)..line.Length].ToList();
            var visibleTreesRight = GetVisibleTrees(treesAtRight, checkedTreeSize);
            var visibleTreesRightCount = CountVisibleTrees(checkedTreeSize, treesAtRight, visibleTreesRight);

            var treesAtTop = lines[..rowIndex].Select(e => e[columnIndex]).Reverse().ToList();
            var visibleTreesTop = GetVisibleTrees(treesAtTop, checkedTreeSize);
            var visibleTreesTopCount = CountVisibleTrees(checkedTreeSize, treesAtTop, visibleTreesTop);

            var treesAtBottom = lines[(rowIndex + 1)..lines.Length].Select(e => e[columnIndex]).ToList();
            var visibleTreesBottom = GetVisibleTrees(treesAtBottom, checkedTreeSize);
            var visibleTreesBottomCount = CountVisibleTrees(checkedTreeSize, treesAtBottom, visibleTreesBottom);

            var scenicScore = visibleTreesLeftCount * visibleTreesRightCount *
                              visibleTreesTopCount * visibleTreesBottomCount;
            if (scenicScore > highestScenicScore)
            {
                highestScenicScore = scenicScore;
            }
        }
    }

    Console.WriteLine(highestScenicScore);
}

Task<bool> CheckIfTreeVisibleFromOneSide(int start, int stop, int treeSize, string line)
{
    return Task.FromResult(line[start..stop].All(e => int.Parse(e.ToString()) < treeSize));
}

Task<bool> CheckIfTreeVisibleFromTopOrBottom(int start, int stop, int column, int treeSize, string[] allRows)
{
    var lines = allRows[start..stop];
    var bottom = lines.All(e => int.Parse(e[column].ToString()) < treeSize);
    return Task.FromResult(lines.Length == 0 || bottom);
}

int CountVisibleTrees(int treeSize, IReadOnlyList<char> treesAtLine, IReadOnlyCollection<char> visibleTreesAtLine)
{
    var visibleTrees = visibleTreesAtLine.Count;

    if (treesAtLine.Count >= visibleTrees + 1)
    {
        if (visibleTreesAtLine.Any())
        {
            if (int.Parse(visibleTreesAtLine.Last().ToString()) != treeSize ||
                treesAtLine[visibleTrees + 1] > visibleTreesAtLine.Last())
            {
                visibleTrees++;
            }
        }
        else
        {
            visibleTrees++;
        }
    }

    return visibleTrees;
}

List<char> GetVisibleTrees(IEnumerable<char> trees, int checkedTreeSize)
{
    return trees.TakeWhile(e => int.Parse(e.ToString()) < checkedTreeSize).ToList();
}