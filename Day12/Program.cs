using Day12;

var file = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");

var map = File.ReadAllLines(file).ToList();
const char startLetter = 'S';
const char endLetter = 'E';

Main();
Main(true);

void Main(bool startFromAnyA = false)
{
    var starts = new List<Tile>();
    if (startFromAnyA)
    {
        for (var y = 0; y < map.Count; y++)
        {
            var xResults = Enumerable.Range(0, map[y].Length)
                .Where(i => map[y][i] == 'a')
                .ToList();
            starts.AddRange(xResults.Select(x => new Tile { Y = y, X = x }));
        }
    }

    var yStartLine = map.FindIndex(e => e.Contains(startLetter));
    var xStartLine = map[yStartLine].IndexOf(startLetter);
    starts.Add(new Tile
    {
        Y = yStartLine,
        X = xStartLine
    });


    var yFinishLine = map.FindIndex(e => e.Contains(endLetter));
    var finish = new Tile
    {
        Y = yFinishLine,
        X = map[yFinishLine].IndexOf(endLetter)
    };

    var results = new List<int>();

    Parallel.ForEach(starts, start =>
    {
        start.SetDistance(finish.X, finish.Y);
        results.Add(FindPath(start, finish));
    });
    Console.WriteLine(results.Min());
}

int FindPath(Tile start, Tile finish)
{
    var activeTiles = new List<Tile> { start };
    var visitedTiles = new List<Tile>();

    while (activeTiles.Any())
    {
        var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();
        if (checkTile.X == finish.X && checkTile.Y == finish.Y)
        {
            return checkTile.Cost;
        }

        visitedTiles.Add(checkTile);
        activeTiles.Remove(checkTile);

        var walkableTiles = GetWalkableTiles(map, checkTile, finish);

        foreach (var walkableTile in walkableTiles)
        {
            if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
            {
                continue;
            }

            if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
            {
                var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                if (existingTile.CostDistance <= checkTile.CostDistance)
                {
                    continue;
                }

                activeTiles.Remove(existingTile);
                activeTiles.Add(walkableTile);
            }
            else
            {
                activeTiles.Add(walkableTile);
            }
        }
    }

    return int.MaxValue;
}

int GetLetterIndex(char letter)
{
    var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToList();
    return letter == startLetter
        ? alphabet.IndexOf('a')
        : alphabet.IndexOf(letter == endLetter ? 'z' : letter);
}

IEnumerable<Tile> GetWalkableTiles(IReadOnlyList<string> map, Tile currentTile, Tile targetTile)
{
    var possibleTiles = new List<Tile>
    {
        new() { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
        new() { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
        new() { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
        new() { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
    };

    possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

    var maxX = map[0].Length - 1;
    var maxY = map.Count - 1;
    var t = possibleTiles
        .Where(tile => tile.X >= 0 && tile.X <= maxX)
        .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
        .Where(tile =>
            GetLetterIndex(map[tile.Y][tile.X]) == GetLetterIndex(map[currentTile.Y][currentTile.X]) + 1 ||
            GetLetterIndex(map[tile.Y][tile.X]) <= GetLetterIndex(map[currentTile.Y][currentTile.X]))
        .ToList();
    return t;
}